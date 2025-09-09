using System;
using System.Xml.Linq;
namespace hhh.iOS
{
    public class FaceRdXmlParser
    {
        public void ParsePidData(string xmlString)
        {
            try
            {
                XDocument doc = XDocument.Parse(xmlString.Trim());
                XElement pidData = doc.Element("PidData");

                if (pidData != null)
                {
                    
                    var resp = pidData.Element("Resp");
                    var errCode = resp?.Attribute("errCode")?.Value;
                    var errInfo = resp?.Attribute("errInfo")?.Value;

                    var skey = pidData.Element("Skey")?.Value;
                    var skeyCi = pidData.Element("Skey")?.Attribute("ci")?.Value;
                    var hmac = pidData.Element("Hmac")?.Value;
                    var data = pidData.Element("Data")?.Value;
                    var dataType = pidData.Element("Data")?.Attribute("type")?.Value;
                    Console.WriteLine($"Resp: {errCode} - {errInfo}");
                    Console.WriteLine($"Skey (ci={skeyCi}): {skey}");
                    Console.WriteLine($"HMAC: {hmac}");
                    Console.WriteLine($"Data (type={dataType}): {data}");
                }
                else
                {
                    Console.WriteLine("PidData element not found.");
                }
            }
            catch (Exception ex)
            {
							string logPath = "error_log.txt"; 
    string message = DateTime.Now + " - Exception while posting KYC request: " + ex.Message + Environment.NewLine;

    System.IO.File.AppendAllText(logPath, message);
				
                Console.WriteLine($"Error While parsing XML: ");
            }
        }
    }
}

