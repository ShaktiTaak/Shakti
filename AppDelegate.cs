using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using System.Net;

using System.IO;
using Xamarin.Essentials;

namespace hhh.iOS
{

    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
    
        UIView loadingOverlay;
        UIActivityIndicatorView activitySpinner;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
           // Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
        public byte[] LoadUIDAICertificate()
        {
            // Look for the .cer file in the bundle
            var path = NSBundle.MainBundle.PathForResource("uidai_auth_preprod", "cer");

            return File.ReadAllBytes(path);
        }

        public void ShowLoading()
        {
            if (loadingOverlay != null) return; // prevent duplicates

            var bounds = UIScreen.MainScreen.Bounds;

            // Semi-transparent full-screen overlay
            loadingOverlay = new UIView(bounds)
            {
                BackgroundColor = UIColor.Black.ColorWithAlpha(0.4f)
            };

            // Spinner setup
            activitySpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Large)
            {
                Color = UIColor.White
            };

            // Label setup
            var loadingLabel = new UILabel
            {
                Text = "Please wait...",
                TextColor = UIColor.White,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize(18),
                Lines = 1
            };

            // Stack the spinner and label vertically
            var stack = new UIStackView(new UIView[] { activitySpinner, loadingLabel })
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Alignment = UIStackViewAlignment.Center,
                Distribution = UIStackViewDistribution.EqualSpacing,
                Spacing = 20,
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            loadingOverlay.AddSubview(stack);
            UIApplication.SharedApplication.KeyWindow.AddSubview(loadingOverlay);

            // Center the stack view
            stack.CenterXAnchor.ConstraintEqualTo(loadingOverlay.CenterXAnchor).Active = true;
            stack.CenterYAnchor.ConstraintEqualTo(loadingOverlay.CenterYAnchor).Active = true;

            activitySpinner.StartAnimating();
        }

        public async Task<bool> ReceiveCallbackFromAadhaarFaceRD(NSUrl url, byte[] certBytes)
        {

       
            var uri = new Uri(url.AbsoluteString);
            String urrr = uri.ToString().Replace("facerdjkjk://hjg.yiuyiu.iuyyiioih/?request=", "");
        
           // Console.WriteLine("URL value---->{0}",urrr);

            //Console.WriteLine("URL value11111111---->{0}", urrr);
            QueryParser q1 = new QueryParser();

            PidDataProcessor1 pd1 = new PidDataProcessor1();
           await pd1.ParseXmlData( q1.mm(urrr));

            return true;
     
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            ShowLoading();

           _ = HandleFaceCaptureCallbackAsync(url);
            return true; // Return true to indicate the URL was handled
        }

        private async Task HandleFaceCaptureCallbackAsync(NSUrl url)
        {
            try
            {
                var cert = LoadUIDAICertificate(); // assuming this is synchronous
                await ReceiveCallbackFromAadhaarFaceRD(url, cert);
            }
            catch (Exception ex)
            {
                // Log or handle error
                Console.WriteLine($"Error handling face capture callback: {ex.Message}");
            }
            finally
            {
              
                HideLoading(); // Custom method to stop the spinner
            }
        }

        public void HideLoading()
        {
            if (activitySpinner != null)
            {
                activitySpinner.StopAnimating();
                activitySpinner.RemoveFromSuperview();
                activitySpinner.Dispose();
                activitySpinner = null;
            }

            if (loadingOverlay != null)
            {
                loadingOverlay.RemoveFromSuperview();
                loadingOverlay.Dispose();
                loadingOverlay = null;
            }
        }

        public static Dictionary<string, string> ParseQueryString(string query)
        {
            var result = new Dictionary<string, string>();

            // Remove leading '?', if present
            if (query.StartsWith("?"))
            {
                query = query.Substring(1);
            }

            // Split query into key-value pairs
            var pairs = query.Split('&', StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                var parts = pair.Split('=', 2);
                var key = WebUtility.UrlDecode(parts[0]);
                var value = parts.Length > 1 ? WebUtility.UrlDecode(parts[1]) : "";
                result[key] = value;
            }

            return result;
        }

        private Dictionary<string, string> ParseQuery(string query)
        {
            return query.TrimStart('?')
                        .Split('&')
                        .Select(part => part.Split('='))
                        .Where(part => part.Length == 2)
                        .ToDictionary(kv => WebUtility.UrlDecode(kv[0]), kv => WebUtility.UrlDecode(kv[1]));
        }

    }





}

