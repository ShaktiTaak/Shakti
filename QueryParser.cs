using System;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Xml.Linq;
using hhh.iOS;
using System.Threading.Tasks;
namespace hhh
{
    public class QueryParser
    {
        //public  String  mm(String url, byte[] certBytes)
                  public  String  mm(String url)
        {
  
            //Uri uri = new Uri(url);
            String xmlString = "";
             //NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);

          xmlString = url.ToString().Replace("%0A", "");
          //  xmlString = HttpUtility.UrlDecode(queryParams.ToString()).Replace("request=", "");

            String xmlString1 = Uri.UnescapeDataString(xmlString);

            string closingTag = "</PidData>";
            int endIndex = xmlString1.IndexOf(closingTag);
           

            if (endIndex != -1)
            {
                xmlString = xmlString1.Substring(0, endIndex + closingTag.Length);
            }
            else
            {
                // Handle error: closing tag not found
                Console.WriteLine("Error: </PidData> tag not found in response.");
            }

            Console.WriteLine("PID DATA: " + xmlString);

         

            return xmlString;

           


   
        public static NameValueCollection ParseQueryString1(string query)
        {
            var nvc = new NameValueCollection();
            if (query.StartsWith("?"))
                query = query.Substring(1);

            foreach (string vp in query.Split('&'))
            {
                string[] singlePair = vp.Split('=');
                if (singlePair.Length == 2)
                {
                    nvc.Add(Uri.UnescapeDataString(singlePair[0]), Uri.UnescapeDataString(singlePair[1]));
                }
            }
            return nvc;
        }





    }







}
