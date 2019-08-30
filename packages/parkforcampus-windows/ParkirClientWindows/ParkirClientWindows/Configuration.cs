using RestSharp;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
        public static string SERIALPORT1_NAME, SERIALPORT2_NAME;
        public static int SERIALPORT1_BAUDRATE, SERIALPORT2_BAUDRATE;
        public static SerialPort SERIALPORT1, SERIALPORT2;
        public static string LOGINNAMA, LOGINNIP;
        public static int LOGINIDTYPE;
       
        public static void getConfig()
        {
            configClass = new config(Application.StartupPath + @"\config.ini");
            TOKEN = configClass.IniReadValue("ENDPOINT", "TOKEN");
            ENDPOINT = configClass.IniReadValue("ENDPOINT", "endpoint");
            CLIENTSERVICE = configClass.IniReadValue("ENDPOINT", "clientService");
            AUTHKEY = configClass.IniReadValue("ENDPOINT", "authKey");
            CONTENT_TYPE = configClass.IniReadValue("ENDPOINT", "contentType");
            SERIALPORT1_NAME = configClass.IniReadValue("SERIALPORT1", "PORTNAME");
            SERIALPORT1_BAUDRATE = Convert.ToInt32(configClass.IniReadValue("SERIALPORT1", "BAUDRATE"));
            SERIALPORT2_NAME = configClass.IniReadValue("SERIALPORT2", "PORTNAME");
            SERIALPORT2_BAUDRATE = Convert.ToInt32(configClass.IniReadValue("SERIALPORT2", "BAUDRATE"));


            CLIENT = new RestClient(ENDPOINT);
        }

        public static RestRequest getHttpConfig(string path = "", bool checkToken = false, string tipe = "POST")
        {
            var request = new RestRequest(path, Method.POST);
            if (tipe == "GET")
            {
                request = new RestRequest(path, Method.GET);
            }

            if (tipe == "PUT")
            {
                request = new RestRequest(path, Method.PUT);
            }

            if (tipe == "DELETE")
            {
                request = new RestRequest(path, Method.DELETE);
            }


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

        public static void setSerialPortSetting()
        {
            SERIALPORT1 = new SerialPort();
            SERIALPORT2 = new SerialPort();
            SERIALPORT1.BaudRate = SERIALPORT1_BAUDRATE;
            SERIALPORT1.PortName = SERIALPORT1_NAME;

            SERIALPORT2.BaudRate = SERIALPORT2_BAUDRATE;
            SERIALPORT2.PortName = SERIALPORT2_NAME;
        }

       

        public static void renderTextLog(TextBox textbox, string newMessage)
        {
            textbox.Text +=  Environment.NewLine + DateTime.Now + " : " + newMessage;
            textbox.SelectionStart = textbox.Text.Length;
            textbox.ScrollToCaret();
        }
    }
}
