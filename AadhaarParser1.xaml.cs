using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Xml;
using Xamarin.Essentials;

namespace hhh
{	
	public partial class AadhaarParser1 : ContentPage
	{
        String Photo = "";

        public AadhaarParser1 ()
		{
			InitializeComponent ();
		}


        public  void ParseXmlResponseData(string xmlData)
        {

            Application.Current.Properties["name_a"] = "";
            Application.Current.Properties["dob_a"] = "";
            Application.Current.Properties["gender_a"] = "";
            string message = "";
            string aadhaar_ref_id = "";
            string err = "";
            string rr = "";

            int status = -1;
            string dob = "", gender = "", name_a = "", nameh = "";

            try
            {
                XmlDocument doc = new XmlDocument();
                Console.WriteLine("Post executed XML Data-->{0}", xmlData);
                doc.LoadXml(xmlData);
                XmlNode kycResNode = doc.GetElementsByTagName("KycRes")[0];

                message = kycResNode.Attributes["ret"]?.Value;
                aadhaar_ref_id = kycResNode.Attributes["aadhaarReferenceNumber"]?.Value;
                err = kycResNode.Attributes["err"]?.Value;
                rr = err;

                if (err == "K-100")
                {
                    status = 0;
                    ShowFailed("Resident authentication failed with K-100 error.", "Try again");
                    //return "Failed";
                }

                if (message == "N")
                {
                    status = 2;
                    ShowFailed("Resident authentication.", "Try again");
                    //  return "Failed";
                }

                if (message == "Y")
                {
                    status = 1;
                    XmlNodeList uidDataList = doc.GetElementsByTagName("UidData");

                    foreach (XmlNode uidNode in uidDataList)
                    {
                        XmlAttributeCollection poiAttrs = uidNode["Poi"]?.Attributes;
                        XmlAttributeCollection poaAttrs = uidNode["Poa"]?.Attributes;
                        XmlNode phtNode = uidNode["Pht"];
                        XmlAttributeCollection lDataAttrs = uidNode["LData"]?.Attributes;

                        dob = poiAttrs?["dob"]?.Value;
                        gender = poiAttrs?["gender"]?.Value;
                        name_a = poiAttrs?["name"]?.Value;

                        Application.Current.Properties["name_a"] = name_a;
                        Application.Current.Properties["dob_a"] = dob;
                        Application.Current.Properties["gender_a"] = gender;
                        string co = poaAttrs?["co"]?.Value ?? "";
                        string country = poaAttrs?["country"]?.Value ?? "";
                        string dist = poaAttrs?["dist"]?.Value ?? "";
                        string state = poaAttrs?["state"]?.Value ?? "";
                        string loc = poaAttrs?["loc"]?.Value ?? "";
                        string street = poaAttrs?["street"]?.Value ?? "";
                        string house = poaAttrs?["house"]?.Value ?? "";
                        string lm = poaAttrs?["lm"]?.Value ?? "";
                        string po = poaAttrs?["po"]?.Value ?? "";
                        string subdist = poaAttrs?["subdist"]?.Value ?? "";
                        string pc = poaAttrs?["pc"]?.Value ?? "";
                        string vtc = poaAttrs?["vtc"]?.Value ?? "";

                        // Hindi attributes
                        string coh = lDataAttrs?["co"]?.Value ?? "";
                        string countryh = lDataAttrs?["country"]?.Value ?? "";
                        string disth = lDataAttrs?["dist"]?.Value ?? "";
                        string stateh = lDataAttrs?["state"]?.Value ?? "";
                        nameh = lDataAttrs?["name"]?.Value ?? "";
                        string loch = lDataAttrs?["loc"]?.Value ?? "";
                        string streeth = lDataAttrs?["street"]?.Value ?? "";
                        string househ = lDataAttrs?["house"]?.Value ?? "";
                        string lmh = lDataAttrs?["lm"]?.Value ?? "";
                        string poh = lDataAttrs?["po"]?.Value ?? "";
                        string subdisth = lDataAttrs?["subdist"]?.Value ?? "";
                        string pch = lDataAttrs?["pc"]?.Value ?? "";
                        string vtch = lDataAttrs?["vtc"]?.Value ?? "";
                         Photo = phtNode?.InnerText;
                        Console.WriteLine("Photo base64: " + phtNode?.InnerText);
                        Console.WriteLine("Hindi Name: " + nameh);
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new Final_Page(phtNode?.InnerText));
                        });


                    }
                }
            }
            catch (Exception ex)
            {
              			string logPath = "error_log.txt"; 
    string message = DateTime.Now + " - Exception while posting KYC request: " + ex.Message + Environment.NewLine;

    System.IO.File.AppendAllText(logPath, message);

			  Console.WriteLine("Exception parsing XML: ");
            }
           
            // return "Success";
        }

        private void ShowFailed(string title, string message)
        {
            Console.WriteLine($"{title} - {message}");
        }
    }
}

