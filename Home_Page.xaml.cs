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
using static Xamarin.Forms.Internals.GIFBitmap;

namespace hhh
{	
	public partial class Home_Page : ContentPage
	{	
		public Home_Page ()
		{

            InitializeComponent ();
            Application.Current.Properties["rc_number"] = "";
        }

        [Obsolete]
        public async void rc(Object sender, EventArgs e)
        {
           
            string url = ""; ;
           
            if (rc_number.Text == "")
            {
                await DisplayAlert("Alert", "Please Enter Ration Card Number or Aadhar Number ", "Try Again");
            }

      
            string rc_number1 = "";
            string ac_number1 = "";
            string Ration_Card_Number = "";
            string Aadhaar_Number = "";
            string rc3 = "";
        
            if (rc_number.Text != "")
            {

                rc_number1 = rc_number.Text;
                url = "http://dfsapi1.hp.gov.in:8082/test_rc_data_api/rest/rc_details/?Username=UnUzSUxnVXJqZTJsK3R6L2NBMWVNZz09&Password=V2QwK3lDMDJ4dUkwc0QrL1JNeVpjZz09&" + "Enter_rc_or_aadhar=" + rc_number1;
             
                try
                {
                    var apiService = new ApiService();
                    string rest = await apiService.GetApiResponseAsString(url);
                    Console.WriteLine("Output of API-->{0}",rest);
                  
                        var HttpClient = new HttpClient();

                        var response = await HttpClient.GetAsync(url);
                        var resp = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Output of API--------------->{0}", resp);
                    if (resp.Equals("Error in Connection"))
                    {
                        await DisplayAlert("Wrong Details", "Kindly Enter Correct RationCard Number/Aadhaar Number", "Ok");

                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<List<rc_detail>>(resp);

                        for (int i = 0; i < result.Count; i++)
                        {
                            if (result[i].RATION_CARD_NUMBER != "")
                            {
                                Ration_Card_Number = result[i].RATION_CARD_NUMBER;
                                Application.Current.Properties["rc_number"] = Ration_Card_Number;
                                await Application.Current.SavePropertiesAsync();
                        
                            }

                        }
                        await Navigation.PushAsync(new Ration_Card_Detail_Page());
                    }
                        if (Ration_Card_Number == "")
                        {
                            await DisplayAlert("Alert", "Invalid Login Details", "Try Again");
                        }
                    
                }

                catch(Exception ex)
                {
                    await DisplayAlert("Invalid Details", "Kindly Enter Correct RationCard Number/Aadhaar Number" , "Ok");
                    rc_number.Text = "";
                }

            }
            
        }
        
        [Obsolete]
        public async void ac(Object sender, EventArgs e)
        {
            string url = ""; ;
           // var a = aadhar_no.Text.Trim();
            string rc_number1 = "";
            string ac_number1 = "";
            string Ration_Card_Number = "";
            string Aadhaar_Number = "";
            string rc3 = "";
            if (aadhar_no.Text=="")
            {
                await DisplayAlert("Alert", "Please Enter Ration Card Number or Aadhar Number ", "Try Again");
            }

            if (aadhar_no.Text!="")
            {
                ac_number1 = aadhar_no.Text;
                url = "http://dfsapi1.hp.gov.in:8082/test_rc_data_api/rest/rc_details/?Username=UnUzSUxnVXJqZTJsK3R6L2NBMWVNZz09&Password=V2QwK3lDMDJ4dUkwc0QrL1JNeVpjZz09&" + "Enter_rc_or_aadhar=" + ac_number1;
                try
                {
                    var apiService = new ApiService();
                    string rest = await apiService.GetApiResponseAsString(url);
                    Console.WriteLine("Output of API-->{0}", rest);

                    var HttpClient = new HttpClient();

                    var response = await HttpClient.GetAsync(url);
                    var resp = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Output of API--------------->{0}", resp);
                    if (resp.Equals("Error in Connection"))
                    {
                        await DisplayAlert("Wrong Details", "Kindly Enter Correct RationCard Number/Aadhaar Number", "Ok");

                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<List<rc_detail>>(resp);
                        for (int i = 0; i < result.Count; i++)
                        {
                            if (result[i].AADHAAR_NUMBER != "")
                            {
                                Aadhaar_Number = result[i].AADHAAR_NUMBER;
                                Application.Current.Properties["rc_number"] = Aadhaar_Number;
                                await Application.Current.SavePropertiesAsync();
                           
                            }


                        }
                        await Navigation.PushAsync(new Ac_detail_Page());
                    }

                    if (Aadhaar_Number == "")
                    {
                        await DisplayAlert("Alert", "Invalid Login Details", "Try Again");
                    }

                }

                catch (Exception ex)
                {
                    await DisplayAlert("Invalid Details", "Kindly Enter Correct RationCard Number/Aadhaar Number", "Ok");
                    aadhar_no.Text = "";
                }

            }

            

        }


    }



}




