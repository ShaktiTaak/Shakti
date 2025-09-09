using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Net;
using Xamarin.Essentials;

namespace hhh
{	
	public partial class Ration_Card_Detail_Page : ContentPage
	{
        String ekyc_statuss = "";
        public Ration_Card_Detail_Page ()
		{
			InitializeComponent ();
            Application.Current.Properties["NAME"] = "";
            Application.Current.Properties["DOB"] = "";
            Application.Current.Properties["GENDER"] = "";
            Application.Current.Properties["AADHAAR"] = "";

            Application.Current.Properties["RATION_CARD_NUMBER"] = "";
            Application.Current.Properties["CARD_TYPE"] = "";
            Application.Current.Properties["RELATION_NAME"] = "";
            Application.Current.Properties["RELIGION"] = "";
            Application.Current.Properties["CASTE_CATEGORY"] = "";
            Application.Current.Properties["ADDRESS"] = "";
            Application.Current.Properties["MOBILE_NUMBER"] = "";
            Application.Current.Properties["FPS_ID"] = "";
            Application.Current.Properties["FPS_NAME"] = "";
            Application.Current.Properties["MARTIAL_STATUS"] = "";
            Application.Current.Properties["OCCUPATION"] = "";
            Application.Current.Properties["DISTRICTID"] = "";
            Application.Current.Properties["DISTRICT"] = "";
            Application.Current.Properties["VILLAGE_NAME"] = "";
            Application.Current.Properties["BLOCKID"] = "";
            Application.Current.Properties["BLOCK"] = "";
            Application.Current.Properties["PACHAYATID"] = "";
            Application.Current.Properties["PANCHAYAT_NAME"] = "";
            Application.Current.Properties["VILLAGEID"] = "";
            Application.Current.Properties["EKYC_STATUS"] = "";
            Application.Current.Properties["BLOCK"] = "";
            Application.Current.Properties["BLOCK"] = "";
            Application.Current.Properties["BLOCK"] = "";
            Application.Current.Properties["BLOCK"] = "";
            fetch_list();

        }

       

        async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; 
            }
            var contact = (rc_detail)e.SelectedItem;

            if (contact.ekyc_status.Equals("eKYC not verified"))
            {
                await DisplayAlert("Your eKYC is not verified", "Kindly proceed to perform your eKYC", "Ok");
                Application.Current.Properties["NAME"] = contact.MEMBER_NAME;
                Application.Current.Properties["DOB"] = contact.DATE_OF_BIRTH;
                Application.Current.Properties["GENDER"] = contact.Gender;
                Application.Current.Properties["AADHAAR"] = contact.AADHAAR_NUMBER;
                Application.Current.Properties["RATION_CARD_NUMBER"] = contact.RATION_CARD_NUMBER;
                Application.Current.Properties["CARD_TYPE"] = contact.card_type;
                Application.Current.Properties["RELATION_NAME"] = contact.RELATION_NAME;
                Application.Current.Properties["RELIGION"] = contact.Religion;
                Application.Current.Properties["CASTE_CATEGORY"] = contact.caste_category;
                Application.Current.Properties["ADDRESS"] = contact.address;
                Application.Current.Properties["MOBILE_NUMBER"] = contact.MOBILE_NUMBER;
                Application.Current.Properties["FPS_ID"] = contact.fps_id;
                Application.Current.Properties["FPS_NAME"] = contact.FPS_NAME;
                Application.Current.Properties["MARTIAL_STATUS"] = contact.MaritalStatus;
                Application.Current.Properties["OCCUPATION"] = contact.Occupation;
                Application.Current.Properties["DISTRICTID"] = contact.DistrictID;
                Application.Current.Properties["DISTRICT"] = contact.DISTRICT;
                Application.Current.Properties["VILLAGE_NAME"] = contact.VILLAGE_NAME;
                Application.Current.Properties["BLOCKID"] = contact.BlockID;
                Application.Current.Properties["BLOCK"] = contact.BLOCK;
                Application.Current.Properties["PACHAYATID"] = contact.PanchayatID;
                Application.Current.Properties["PANCHAYAT_NAME"] = contact.PANCHAYAT_NAME;
                Application.Current.Properties["VILLAGEID"] = contact.VillageID;
                Application.Current.Properties["EKYC_STATUS"] = contact.ekyc_status;

                await Navigation.PushAsync(new Consent());

            }
            else
            {

                await DisplayAlert("eKYC Status", contact.MEMBER_NAME.ToString() + " " + "is" + " " + contact.ekyc_status.ToString() + " ", "Ok");

            }

            //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
        }

        public async void ekyc__(Object sender, EventArgs e)
        {



        }


        public async void fetch_list()
        {

       

            var rc_number = Application.Current.Properties["rc_number"].ToString();

            var HttpClient = new HttpClient();

            String url = "http://frgdgfgg.hp.gov.in:8098/test_rc_data_api/rest/rc_details/" + "Enter_rc_or_aadhar=" + rc_number;


            var response = await HttpClient.GetAsync(url);
            var resp = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<rc_detail>>(resp);

            rc_list.ItemsSource = result;

            
        }

    }
}