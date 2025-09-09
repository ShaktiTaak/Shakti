using System;
using System.Xml;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using System.Net;
using System.IO;


namespace hhh
{

   
    public class PidDataProcessor1
    {
        
        private INavigation _navigation;
        string responseText = "";
        // Fields to store parsed values
        public string EncryptedPidXmlStr { get; private set; }
        public string EncryptedHmacStr { get; private set; }
        public string EncryptedSkey { get; private set; }

        public string DpId { get; private set; }
        public string Mc { get; private set; }
        public string Mi { get; private set; }
        public string RdsId { get; private set; }
        public string RdsVer { get; private set; }
        public string Dc { get; private set; }
        public string Ci { get; private set; }
        public string Type { get; private set; }
        public string param = "";

        public async Task ParseXmlData(string xmlData)
        {
            
            await new PidDataProcessor1().ExecuteAsync(xmlData);
        }

        public async Task<string> SendAuthentication2(string xmldat)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmldat);
            doc.Normalize();
            try
            {

                XmlNodeList pidDataList = doc.GetElementsByTagName("PidData");

                foreach (XmlNode node in pidDataList)
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XmlElement eElement = (XmlElement)node;

                        XmlElement respElement = (XmlElement)eElement.GetElementsByTagName("Resp")[0];
                        string errorCode = respElement?.GetAttribute("errCode");
                        string errorInfo = respElement?.GetAttribute("errInfo");

                        if (string.Equals(errorCode, "0", StringComparison.OrdinalIgnoreCase))
                        {
                            EncryptedPidXmlStr = eElement.GetElementsByTagName("Data")[0]?.InnerText.Replace(" ", "");
                            Console.WriteLine("Data: " + EncryptedPidXmlStr);

                            EncryptedHmacStr = eElement.GetElementsByTagName("Hmac")[0]?.InnerText.Replace(" ", "");
                            Console.WriteLine("Hmac: " + EncryptedHmacStr);

                            EncryptedSkey = eElement.GetElementsByTagName("Skey")[0]?.InnerText.Replace(" ", "");
                            Console.WriteLine("Skey: " + EncryptedSkey);

                            XmlElement deviceInfo = (XmlElement)eElement.GetElementsByTagName("DeviceInfo")[0];
                            DpId = deviceInfo?.GetAttribute("dpId");
                            Mc = deviceInfo?.GetAttribute("mc").Replace(" ", "");
                            Mi = deviceInfo?.GetAttribute("mi");
                            RdsId = deviceInfo?.GetAttribute("rdsId");
                            RdsVer = deviceInfo?.GetAttribute("rdsVer");
                            Dc = deviceInfo?.GetAttribute("dc");

                            Console.WriteLine($"dpId: {DpId}, mc: {Mc}, mi: {Mi}, rdsId: {RdsId}, rdsVer: {RdsVer}, dc: {Dc}");

                            XmlElement skeyElement = (XmlElement)eElement.GetElementsByTagName("Skey")[0];
                            Ci = skeyElement?.GetAttribute("ci").Replace(" ", ""); ;
                            Console.WriteLine("ci: " + Ci);

                            XmlElement dataElement = (XmlElement)eElement.GetElementsByTagName("Data")[0] ;
                            Type = dataElement?.GetAttribute("type").Replace(" ", ""); ;
                            Console.WriteLine("type: " + Type);  
	
                        }
                    }
                }


                        }
            catch (Exception ex)
            {
							string logPath = "error_log.txt"; 
    string message = DateTime.Now + " - Exception while posting KYC request: " + ex.Message + Environment.NewLine;

    System.IO.File.AppendAllText(logPath, message);
                Console.WriteLine("Error While parsing XML ");
            }

            try
            {

                var inputAuth = new Dictionary<string, string>
        {

            { "xmlns", "http://www.jhjdskj.gov.in/authentication/tgfgfgf/2.0" },
           { "uid", Application.Current.Properties["AADHAAR"].ToString() },
    
            { "rc", "Y" },
            { "tid", "registered" },
            { "sa", "STGfk87988080" },
            { "appId", "KKKpds" },
            { "ac", "STG68bkjhhkh" },
            { "ver", "2.5" },
            { "txn", "UKC:" + createTxn1() },
            { "lk", "MA1y0L4l3iFRveybNghgdsg876876jhjh9897p6AVP2S1yxa980mn4c" }
  
        };

                var inputUses = new Dictionary<string, string>
        {
            { "pi", "n" },
            { "pa", "n" },
            { "bio", "y" },
            { "bt", "DID" },
            { "otp", "n" },
            { "pin", "n" }
        };

                var inputMeta = new Dictionary<string, string>
        {
               { "rdsId", RdsId },
            { "rdsVer", RdsVer },
            { "dpId", DpId },
            { "dc", Dc },
            { "mc", Mc }
        };

                var inputSkey = new Dictionary<string, string>
        {
            { "ci", Ci }
        };

                var inputPid = new Dictionary<string, string>
        {
            { "type", Type }
        };


                // You must define the `GetAuthRequestV25` method elsewhere to handle request formatting
                return await GetAuthRequestV25(
                     inputAuth,
                     inputUses,
                     inputMeta,
                     inputSkey,
                     EncryptedSkey,
                     inputPid,
                     EncryptedPidXmlStr,
                     EncryptedHmacStr
                 );


                
            }
            catch (Exception ex)
            {
							string logPath = "error_log.txt"; 
    string message = DateTime.Now + " - Exception while posting KYC request: " + ex.Message + Environment.NewLine;

    System.IO.File.AppendAllText(logPath, message);
				
                Console.WriteLine("Error While generating request");
                //  return string.Empty;
            }

            //Console.WriteLine("Auth Server Response: " + result);
            return "";
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
            return random.Next(1000, 9999); // Generates a number between 1000 and 9998
        }



        public async Task<string> GetAuthRequestV25(
        Dictionary<string, string> authMap,
        Dictionary<string, string> usesMap,
        Dictionary<string, string> metaMap,
        Dictionary<string, string> skeyMap,
        string skeyValue,
        Dictionary<string, string> dataMap,
        string dataValue,
        string hmacValue)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");

                // Start Auth tag
                sb.Append("<Auth ");
                foreach (var kvp in authMap)
                {
                    sb.Append($"{kvp.Key}=\"{kvp.Value}\" ");
                }
                sb.Append(">");

                // <Uses />
                sb.Append("<Uses ");
                foreach (var kvp in usesMap)
                {
                    sb.Append($"{kvp.Key}=\"{kvp.Value}\" ");
                }
                sb.Append("/>");

                // <Meta />
                sb.Append("<Meta ");
                foreach (var kvp in metaMap)
                {
                    sb.Append($"{kvp.Key}=\"{kvp.Value}\" ");
                }
                sb.Append("/>");

                // <Skey>...</Skey>
                sb.Append("<Skey ");
                foreach (var kvp in skeyMap)
                {
                    sb.Append($"{kvp.Key}=\"{kvp.Value}\" ");
                }
                sb.Append(">");
                sb.Append(skeyValue);
                sb.Append("</Skey>");

                // <Data>...</Data>
                sb.Append("<Data ");
                foreach (var kvp in dataMap)
                {
                    sb.Append($"{kvp.Key}=\"{kvp.Value}\" ");
                }
                sb.Append(">");
                sb.Append(dataValue);
                sb.Append("</Data>");

                // <Hmac>...</Hmac>
                sb.Append("<Hmac>");
                sb.Append(hmacValue);
                sb.Append("</Hmac>");

                // Optional <Demo> element
                sb.Append("<Demo lang=\"06\" ></Demo>");

                // End Auth
                sb.Append("</Auth>");

                // Debug print (like Log.e)
                Console.WriteLine("Auth XML: " + sb.ToString());
            }
            catch (Exception ex)
            {
							string logPath = "error_log.txt"; 
    string message = DateTime.Now + " - Exception while posting KYC request: " + ex.Message + Environment.NewLine;

    System.IO.File.AppendAllText(logPath, message);
				
				
                Console.WriteLine("Error while generating Auth XML");
            }

            return await getKycRequest(sb.ToString());
        }




        public async Task<string> getKycRequest(string authXml)
        {
            try
            {
                Console.WriteLine("getKycRequest: " + authXml);


                var sb = new StringBuilder();
                sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                sb.Append("<Kyc ");
                sb.Append("ver=\"2.5\" ");
                sb.Append("ra=\"P\" "); // Resident Authentication
                sb.Append("rc=\"Y\" "); // Resident Consent
                sb.Append("lr=\"Y\" "); // Local Language Request
                sb.Append("de=\"N\" "); // Digital Enablement (N=off)
                sb.Append("pfr=\"N\" "); // Partial Fetch Request
                sb.Append("appId=\"HPjjgjS\" ");
                sb.Append(">");

                // Base64 encode the Auth XML
                string base64Auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(authXml));
                sb.Append("<Rad>");
                sb.Append(base64Auth);
                sb.Append("</Rad>");
                sb.Append("</Kyc>");

                string kycXml = sb.ToString();


             

                Console.WriteLine("KYC XML is ------>{0}", kycXml);

               return await PostRequestKycAsync(sb.ToString(), "http://99.764.107.77:8080/auac25/hghghgj/authenticateKyc", "BIO");
     
            }
            catch (Exception ex)
            {
				
							string logPath = "error_log.txt"; 
    string message = DateTime.Now + " - Exception while posting KYC request: " + ex.Message + Environment.NewLine;

    System.IO.File.AppendAllText(logPath, message);
				
				
                Console.WriteLine("Error while building KYC request ");
                return string.Empty;
            }
        }

        public async Task<string> PostRequestKycAsync(string requestXml, string url, string authType)
        {
            try
            {
            
                Uri obj = new Uri(url);
                HttpWebRequest con = (HttpWebRequest)WebRequest.Create(obj);
                con.Method = "POST";
                con.ContentType = "text/plain; charset=ISO-8859-1";
                con.Timeout = 300000;
                con.ReadWriteTimeout = 300000;
                con.AllowWriteStreamBuffering = true;
string safeInputrequestXml = System.Security.SecurityElement.Escape(userInput);
                using (Stream wr = con.GetRequestStream())
                {
                    byte[] bytes = Encoding.ASCII.GetBytes(safeInputrequestXml);
                    wr.Write(bytes, 0, bytes.Length);
                }
               
                HttpWebResponse response = (HttpWebResponse) await con.GetResponseAsync();

                int responseCode = (int)response.StatusCode;

                using (Stream responseStream=response.GetResponseStream())
                {
                    if(responseStream!=null)
                    {
                        using (StreamReader reader=new StreamReader(responseStream,Encoding.UTF8))
                        {
                             responseText = await reader.ReadToEndAsync();
                            Console.WriteLine("Kyc Response is -->{0}", responseText);
                           

                            await OnPostExecute(responseText);
                        }
                    }
                }
                
          
            }
            catch (Exception ex)
            {
               
				
				string logPath = "error_log.txt"; 
    string message = DateTime.Now + " - Exception while posting KYC request: " + ex.Message + Environment.NewLine;

    System.IO.File.AppendAllText(logPath, message);
				 Console.WriteLine("Exception while posting KYC request,");
				
				
                return null;
            }
            return null;
        }
		
		
        private void ShowToast(string message, bool isError, bool isShort)
        {
          
            Console.WriteLine($"Toast: {message}");
        }


        //public class rrr
        //{
        private readonly IProgress<string> _progress;

        public PidDataProcessor1(IProgress<string> progress = null)
        {
            _progress = progress;
        }

        public async Task<string> ExecuteAsync(string xmlData)
        {
            // Equivalent to onPreExecute
            await OnPreExecute();

            string result = await Task.Run(() => DoInBackground(xmlData));

            // Equivalent to onPostExecute
        
           return result;
        }

        private async Task OnPreExecute()
        {
            // UI preparation logic (e.g., show loading)
            Console.WriteLine("Task starting...");
           // return "Task starting...";
        }

        private string DoInBackground(string xmlData)
        {
            // Simulate background work
            for (int i = 1; i <= 3; i++)
            {
                Thread.Sleep(1000); // simulate work
                _progress?.Report($"Progress: {i * 33}%"); // Equivalent to publishProgress
            }
            PidDataProcessor1 p1 = new PidDataProcessor1();

            return $"Processed: {p1.SendAuthentication2(xmlData)}";
        }

        private async Task OnPostExecute(String resptxt)
        {
            // Update UI after task completion
            Console.WriteLine("Post Exceution started");
            //Navigation.PushAsync(new Final_Page(Photo));
            AadhaarParser1 ap1 = new AadhaarParser1();
            ap1.ParseXmlResponseData(responseText);
            // return "";
        }

      

  
    }



}