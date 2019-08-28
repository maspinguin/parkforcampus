using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    class Configuration
    {
        public static config configClass;
        public static string ENDPOINT, CLIENTSERVICE, AUTHKEY, CONTENT_TYPE;
        public static string TOKEN;
        public static RestClient CLIENT;

       
        public static void getConfig()
        {
            configClass = new config(Application.StartupPath + @"\config.ini");
            TOKEN = configClass.IniReadValue("ENDPOINT", "TOKEN");
            ENDPOINT = configClass.IniReadValue("ENDPOINT", "endpoint");
            CLIENTSERVICE = configClass.IniReadValue("ENDPOINT", "clientService");
            AUTHKEY = configClass.IniReadValue("ENDPOINT", "authKey");
            CONTENT_TYPE = configClass.IniReadValue("ENDPOINT", "contentType");
            

            CLIENT = new RestClient(ENDPOINT);
        }

        public static RestRequest getHttpConfig(string path = "", bool checkToken = false)
        {
            var request = new RestRequest(path, Method.POST);

            request.RequestFormat = DataFormat.Json;

            if(checkToken)
            request.AddJsonBody(new { token = TOKEN }); // uses JsonSerializer

            request.AddHeader("Client-Service", CLIENTSERVICE);
            request.AddHeader("Auth-Key", AUTHKEY);
            request.AddHeader("Content-Type", CONTENT_TYPE);

            if (!checkToken)
                request.AddHeader("Authorization", TOKEN);
            return request;
        }
    }
}
