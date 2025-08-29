using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace hhh
{	
	public partial class QrCode : ContentPage
	{
        public QrCode()
        {
            InitializeComponent();
            RR();
        }


        public void RR()
        {

            //var rc_number = Application.Current.Properties["rc_number"].ToString();
            rc_no.Text = "https://play.google.com/store/apps/details?id=com.hp.face_ekyc__face";
            //var rc_number1 = "HP-20145-219693";





        }
    }
}

