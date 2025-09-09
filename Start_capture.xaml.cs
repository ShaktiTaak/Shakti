using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
//using Foundation;
//using UIKit;
using System.Net;

using Xamarin.Forms;

namespace hhh
{


    public partial class Start_capture : ContentPage
    {	    
        private const string ENVIRONMENT_TAG = "PP"; // or "Staging"
        private const string LANGUAGE = "en";
        private const string ENABLE_AUTO_CAPTURE = "true";

        public static string WADH_KEY = "RZhhkhkhScZ74b3EibKYy1WyGw=";
        public Start_capture ()
		{
			InitializeComponent ();

            name.Text = Application.Current.Properties["NAME"].ToString();

            aadhaar.Text = Application.Current.Properties["AADHAAR"].ToString();

         
        }
        public void capt(Object sender, EventArgs e)
        {
             //Navigation.PushAsync(new Final_Page(""));
            DependencyService.Get<IUrlOpener>()?.OpenUrl1();

        }
       
    }

    
}

