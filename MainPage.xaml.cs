using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace hhh
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        [Obsolete]
        public async void ad(Object sender, EventArgs e)
        {


            await Navigation.PushAsync(new Home_Page());

            //new NavigationPage(new Home_Page());

        }

    }
}

