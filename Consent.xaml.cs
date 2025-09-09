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
using System.Collections.Specialized;
using System.Web;

namespace hhh
{	
	public partial class Consent : ContentPage
	{

        public string BingStringValue { get; set; }


        void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // Perform required operation after examining e.Value
        }
        public  async void rd_auth(Object sender, EventArgs e)
        {
            if (checkBox2.IsChecked == false)
            {
                await DisplayAlert("Alert", "Kindly Check the Consent " , "Ok");
                }
            else
            {
                await Navigation.PushAsync(new Start_capture());
            }


        }


        public Consent ()
		{
			InitializeComponent ();

            name.Text=  Application.Current.Properties["NAME"].ToString() ;
         dob.Text=   Application.Current.Properties["DOB"].ToString() ;
          gender.Text     =   Application.Current.Properties["GENDER"].ToString() ;
           aadhaar.Text= Application.Current.Properties["AADHAAR"].ToString() ;
            List<string> conse = new List<string> { "1. I understand that my Aadhaar number, photograph and demographic information, as understood under the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and services) Act, 2016 (18 of 2016) and regulations framed there under, is being collected by the Government of India for the following Purposes:"
,"i. Authenticating my identity by way of the Aadhaar authentication system:","ii. Registering on the Portal (Name of the Portal), after authentication, for availing subsidies, benefits & services under Section 7 of the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and Services) Act, 2016 (18 of 2016);"
            };
            //consentt.ItemsSource = conse;
            BingStringValue = "1. I understand that my Aadhaar number, photograph and demographic information, as understood under the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and services) Act, 2016 (18 of 2016) and regulations framed there under, is being collected by the Government of India for the following Purposes:\n " +
                "i. Authenticating my identity by way of the Aadhaar authentication system: \n" +
                "ii. Registering on the Portal (Name of the Portal), after authentication, for availing subsidies, benefits & services under Section 7 of the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and Services) Act, 2016 (18 of 2016);\n" +
                "iii. Sharing of my Aadhaar number and demographic information and photograph, for verifying my identity for the purpose of determining my eligibility across Government welfare programmes, which are in existence and for future programmes, run by the Central Government and State Governments under Section 7 of the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and Services) Act, 2016 (18 of 2016);\n                \n2. I understand that the Government of India shall create an Aadhaar-seeded database containing my Aadhaar number, photograph and demographic information for all or any of the purposes enlisted in paragraphs 1(i)-(iii) of this form, and that the Government of India shall ensure that requisite mechanisms have been put in place to ensure safety, security and privacy of such information in accordance with applicable laws, rules and regulations.\n3. I have no objection to provide my Aadhaar Number, photograph and demographic information for Aadhaar based authentication for the purposes enlisted in paragraphs 1(i)-(iii) of this form and further for creation of an Aadhaar-seeded database as described Paragraph 2 of this form,\n4. I also understand that my ‘no-objection’ accorded in this form is revocable and I have the right to withdraw the same at any time in future, through a communication of opting out.\n            \n1. मैं समझता हूँ कि मेरे द्वारा दी गयी जानकारी जैसे मेरा आधार नंबर, फोटोग्राफ, जनसांख्यिकीय जानकारी जिसमें मेरा (नाम, पता, जन्म तिथि, लिंग) आदि आते हैं। इन्हें वित्तीय और अन्य सब्सिडी का लक्षित वितरण, लाभ और सेवाएं अधिनियम, 2016 और उसके तहत बनाए गए नियमों के अंतर्गत एकत्र किया  गया है। इनका प्रयोग भारत सरकार द्वारा निम्नलिखित उद्देश्य के लिए किया जा रहा है:\n      i. आधार प्रमाणीकरण प्रणाली के माध्यम से मेरी पहचान को प्रमाणित करना।\n      ii. प्रमाणित होने के बाद पोर्टल (पोर्टल का नाम) पर पंजीकरण और लाभ उठाने के लिए आधार की धारा 7 के तहत सब्सिडी, लाभ और सेवाएं लक्षित डिलीवरी वित्तीय और अन्य सब्सिडी, लाभ और सेवाएं अधिनियम, 2016 के तहत प्रयोग के लिए।\n     iii. मैं अपना आधार और उससे जुड़ी जानकारियां आधार की धारा 7 के तहत सरकारी कल्याण कार्यक्रम, जो अस्तित्व में हैं और भविष्य के कार्यक्रमों के लिए हैं और केंद्र सरकार और राज्य की वित्तीय और अन्य सब्सिडी, लाभ और सेवाओं के वितरण जोकि अधिनियम, 2016 के अंतर्गत आते हैं उनमें अपनी पात्रता जाँचने के लिए साझा कर रहा हूँ।\n                \n2. मैं समझता हूँ कि प्रदेश सरकार आधार युक्त डेटाबेस तैयार करेगी जिसमें मेरा आधार नंबर, फोटोग्राफ और सभी या इनमें से किसी के लिए जिसमें जनसांख्यिकीय जानकारी शामिल है जो इस प्रपत्र के पैराग्राफ  1(i)-(iii) में सूचीबद्ध है साथ ही इस उद्देश्य से मेरे द्वारा दी गयी जानकारी की  सुरक्षा और         गोपनीयता सुनिश्चित करने के लिए आवश्यक तंत्र स्थापित किए गए हैं और भारत सरकार लागू कानूनों, नियमों और विनियमों के अनुसार ये सुनिश्चित करें।\n3. पैराग्राफ  1(i)-(iii) में सूचीबद्ध उद्देश्यों के लिए, आधार आधारित प्रमाणीकरण के लिए और इस फॉर्म के पैराग्राफ 2 में वर्णित आधार युक्त डेटाबेस के निर्माण के लिए मुझे अपना आधार नंबर, फोटोग्राफ और जनसांख्यिकीय जानकारी प्रदान करने में कोई आपत्ति नहीं है।\n4. मैं ये समझता हूँ कि मेरे द्वारा दी गयी अनापत्ति से मैं भविष्य में पत्राचार के माध्यम से बाहर निकलने और इसे वापस लेने का अधिकार रखता हूँ।";

            consett.Text= "1. I understand that my Aadhaar number, photograph and demographic information, as understood under the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and services) Act, 2016 (18 of 2016) and regulations framed there under, is being collected by the Government of India for the following Purposes:\n " +
                "i. Authenticating my identity by way of the Aadhaar authentication system: \n" +
                "ii. Registering on the Portal (Name of the Portal), after authentication, for availing subsidies, benefits & services under Section 7 of the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and Services) Act, 2016 (18 of 2016);\n" +
                "iii. Sharing of my Aadhaar number and demographic information and photograph, for verifying my identity for the purpose of determining my eligibility across Government welfare programmes, which are in existence and for future programmes, run by the Central Government and State Governments under Section 7 of the Aadhaar (Targeted Delivery of Financial and Other Subsidies, Benefits and Services) Act, 2016 (18 of 2016);\n                \n2. I understand that the Government of India shall create an Aadhaar-seeded database containing my Aadhaar number, photograph and demographic information for all or any of the purposes enlisted in paragraphs 1(i)-(iii) of this form, and that the Government of India shall ensure that requisite mechanisms have been put in place to ensure safety, security and privacy of such information in accordance with applicable laws, rules and regulations.\n3. I have no objection to provide my Aadhaar Number, photograph and demographic information for Aadhaar based authentication for the purposes enlisted in paragraphs 1(i)-(iii) of this form and further for creation of an Aadhaar-seeded database as described Paragraph 2 of this form,\n4. I also understand that my ‘no-objection’ accorded in this form is revocable and I have the right to withdraw the same at any time in future, through a communication of opting out.\n            \n1. मैं समझता हूँ कि मेरे द्वारा दी गयी जानकारी जैसे मेरा आधार नंबर, फोटोग्राफ, जनसांख्यिकीय जानकारी जिसमें मेरा (नाम, पता, जन्म तिथि, लिंग) आदि आते हैं। इन्हें वित्तीय और अन्य सब्सिडी का लक्षित वितरण, लाभ और सेवाएं अधिनियम, 2016 और उसके तहत बनाए गए नियमों के अंतर्गत एकत्र किया  गया है। इनका प्रयोग भारत सरकार द्वारा निम्नलिखित उद्देश्य के लिए किया जा रहा है:\n      i. आधार प्रमाणीकरण प्रणाली के माध्यम से मेरी पहचान को प्रमाणित करना।\n      ii. प्रमाणित होने के बाद पोर्टल (पोर्टल का नाम) पर पंजीकरण और लाभ उठाने के लिए आधार की धारा 7 के तहत सब्सिडी, लाभ और सेवाएं लक्षित डिलीवरी वित्तीय और अन्य सब्सिडी, लाभ और सेवाएं अधिनियम, 2016 के तहत प्रयोग के लिए।\n     iii. मैं अपना आधार और उससे जुड़ी जानकारियां आधार की धारा 7 के तहत सरकारी कल्याण कार्यक्रम, जो अस्तित्व में हैं और भविष्य के कार्यक्रमों के लिए हैं और केंद्र सरकार और राज्य की वित्तीय और अन्य सब्सिडी, लाभ और सेवाओं के वितरण जोकि अधिनियम, 2016 के अंतर्गत आते हैं उनमें अपनी पात्रता जाँचने के लिए साझा कर रहा हूँ।\n                \n2. मैं समझता हूँ कि प्रदेश सरकार आधार युक्त डेटाबेस तैयार करेगी जिसमें मेरा आधार नंबर, फोटोग्राफ और सभी या इनमें से किसी के लिए जिसमें जनसांख्यिकीय जानकारी शामिल है जो इस प्रपत्र के पैराग्राफ  1(i)-(iii) में सूचीबद्ध है साथ ही इस उद्देश्य से मेरे द्वारा दी गयी जानकारी की  सुरक्षा और         गोपनीयता सुनिश्चित करने के लिए आवश्यक तंत्र स्थापित किए गए हैं और भारत सरकार लागू कानूनों, नियमों और विनियमों के अनुसार ये सुनिश्चित करें।\n3. पैराग्राफ  1(i)-(iii) में सूचीबद्ध उद्देश्यों के लिए, आधार आधारित प्रमाणीकरण के लिए और इस फॉर्म के पैराग्राफ 2 में वर्णित आधार युक्त डेटाबेस के निर्माण के लिए मुझे अपना आधार नंबर, फोटोग्राफ और जनसांख्यिकीय जानकारी प्रदान करने में कोई आपत्ति नहीं है।\n4. मैं ये समझता हूँ कि मेरे द्वारा दी गयी अनापत्ति से मैं भविष्य में पत्राचार के माध्यम से बाहर निकलने और इसे वापस लेने का अधिकार रखता हूँ।";

            objt.Text = "I have no objection in providing eKYC data and fully understand that information provided by me shall be used for my eKYC authentication through Aadhaar Authentication System for the PDS-Parivar Register ad no other purpose.";



        }
    }
}

