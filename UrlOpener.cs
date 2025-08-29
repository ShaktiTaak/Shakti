using Foundation;
using UIKit;
using Xamarin.Forms;
using hhh.iOS;
using hhh;
using static CoreFoundation.DispatchSource;
using System;
using System.Globalization;
using System.Text;
using System.Security.Cryptography;

[assembly: Dependency(typeof(UrlOpener))]
namespace hhh.iOS
{
    public class UrlOpener : IUrlOpener
    {
        private const string ENVIRONMENT_TAG = "PP"; // or "Staging"
        private const string LANGUAGE = "en";
        private const string ENABLE_AUTO_CAPTURE = "true";
;
       
                  public  void OpenUrl1()
        {

           Console.WriteLine( WadhGenerator.GenerateWadh());

            var txnId = createTxn1(); // You should define this
             var requestXml = CreatePidOptions("HPeKYC:" + txnId, "auth");
             string escapedRequest = Uri.EscapeDataString(requestXml);
            string encodedInput = Uri.EscapeDataString(txnId);
         
            string base64PidOptions = Convert.ToBase64String(Encoding.UTF8.GetBytes(requestXml));
            string callbackUrl = $"FackikSHP://requestCode=123";
            string escapedRequest1 = Uri.EscapeDataString(callbackUrl);
            var urlScheme = new NSUrl($"Facjhg://in.com.uidai.hgjgj.face.CAPTURE?request={escapedRequest}");

            try
            {

                if (UIApplication.SharedApplication.CanOpenUrl(urlScheme))
                {
                    UIApplication.SharedApplication.OpenUrl(urlScheme);
                    NSUserDefaults.StandardUserDefaults.SetString(encodedInput, "ReceivedTransactionID");
                    NSUserDefaults.StandardUserDefaults.Synchronize();
                }
                else
                {

                    var alert = UIAlertController.Create("Not Supported",
                     "Face RD capture not available.",
                     UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                    var window = UIApplication.SharedApplication.KeyWindow;
                    var vc = window.RootViewController;

                    while (vc.PresentedViewController != null)
                        vc = vc.PresentedViewController;

                    vc.PresentViewController(alert, true, null);



                }
            }
            catch(Exception ex)
            {
                var alert = UIAlertController.Create("Not Supported",
                     ex.Message.ToString(),
                     UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;

                while (vc.PresentedViewController != null)
                    vc = vc.PresentedViewController;

                vc.PresentViewController(alert, true, null);

            }

          
        }

        public static string CreatePidOptions(string txnId, string purpose)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            sb.Append("<PidOptions ver=\"2.0\" env=\"").Append(ENVIRONMENT_TAG).Append("\">\n");
            sb.Append("   <Opts fCount=\"\" fType=\"\" iCount=\"\" iType=\"\" pCount=\"\" pType=\"\" format=\"\"  timeout=\"\" otp=\"\" wadh=\"")
              .Append(GenerateWadh()).Append("\" posh=\"UNKNOWN\" />\n");
            sb.Append("   <CustOpts>\n");
            sb.Append("      <Param name=\"txnId\" value=\"").Append(txnId).Append("\"/>\n");

            sb.Append("      <Param name=\"callback\" value=\"").Append("FacekhjhkP").Append("\"/>\n");
            sb.Append("      <Param name=\"purpose\" value=\"").Append("auth").Append("\"/>\n");
            sb.Append("      <Param name=\"language\" value=\"").Append(LANGUAGE).Append("\"/>\n");
            sb.Append("      <Param name=\"enableAutoCapture\" value=\"").Append(ENABLE_AUTO_CAPTURE).Append("\"/>\n");
            sb.Append("   </CustOpts>\n");
            sb.Append("</PidOptions>");

            return sb.ToString();
        }

     /*   private static string GenerateWadh()
        {
            // Implement your logic to generate the WADH hash
            return "RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=";
        }*/


       
        public static string GenerateWadh()
        {
            string wadh = "2.5kjhkjkj";
            byte[] hash;

            // Generate SHA-256 hash
            using (SHA256 sha256 = SHA256.Create())
            {
                hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(wadh));
            }

            // Convert hash to Base64 string
            string outStr = Convert.ToBase64String(hash);
            Console.WriteLine("Wadh vaklue is --{0}", outStr);

            return outStr;
        }

        public string createTxn1()
        {
            string timestamp = DateTime.Now.ToString("yyMMddHHmmss");
            int randomNumber = GetRandomNumber();
            return $"{timestamp}{randomNumber}D";
        }

        private int GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(1000, 9999); 
        }

    }
}
