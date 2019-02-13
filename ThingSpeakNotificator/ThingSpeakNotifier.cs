using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ThingSpeakNotificator
{
    public class ThingSpeakNotifier
    {
        public static async Task<string> UpdateAsync(string writeKey, string field1, string status)
        {
            string strUpdateBase = "http://api.thingspeak.com/update";
            string strUpdateURI = strUpdateBase + "?key=" + writeKey;

            strUpdateURI += "&field1=" + field1;
            strUpdateURI += "&status=" + status;

            string responseText = await Task.Run(() =>
            {
                try
                {
                    HttpWebRequest request = WebRequest.Create(strUpdateURI) as HttpWebRequest;
                    request.Timeout = 7000;
                    request.ReadWriteTimeout = 14000;
                    WebResponse response = request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    return new StreamReader(responseStream).ReadToEnd();
                }
                catch (Exception)
                {
                    return null;
                }
            });

            return responseText;
        }
    }
}
