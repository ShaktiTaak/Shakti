using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;

namespace hhh
{	
	public partial class Final_Page : ContentPage
	{	
		public Final_Page (String Photo)
		{
			InitializeComponent ();
            SetBase64Image(Photo);

        }
        public void SetBase64Image(string base64String)
        {
          
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Convert to ImageSource
            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));

            // Assign to the Image control
            AadhaarImage.Source = imageSource;


            name.Text = Application.Current.Properties["NAME"].ToString();
            dob.Text = Application.Current.Properties["DOB"].ToString();
            gender.Text = Application.Current.Properties["GENDER"].ToString();


           name_a.Text= Application.Current.Properties["name_a"].ToString() ;
         dob_a.Text=   Application.Current.Properties["dob_a"].ToString() ;
          gender_a.Text = Application.Current.Properties["gender_a"].ToString();
            //aadhaar.Text = Application.Current.Properties["AADHAAR"].ToString();


        }

    }
}

