using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    class ArduinoHandler: Configuration
    {
        static TextBox textBox1;
        static TextBox textBox2;
        static string command1, command2;
        
        public static string serialMessage1 { get;private set; }
        public static string serialMessage2 { get;private set; }
        public static void OpenPort1(TextBox textbox)
        {
            if(textBox1 == null)
            {
                textBox1 = textbox;
            }
            if (!SERIALPORT1.serial.IsOpen)
            {
                try
                {
                    
                    SERIALPORT1.serial.Open();
                    renderTextLog(textBox1, "Serial Port 1 Open");
                    
                    SERIALPORT1.serial.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                }
                catch (Exception err)
                {
                    renderTextLog(textBox1, err.Message.ToString());
                }
            }
            else
            {
                renderTextLog(textbox, "PORT 1 ALREADY OPEN");

            }
        }

        public static void OpenPort2(TextBox textbox)
        {
            if (textBox1 == null)
            {
                textBox2= textbox;
            }
            if (!SERIALPORT2.serial.IsOpen)
            {
                try
                {
                    SERIALPORT2.serial.Open();
                    renderTextLog(textbox, "Serial Port 2 Open");

                    SERIALPORT2.serial.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceived);
                }
                catch (Exception err)
                {
                    renderTextLog(textbox, err.Message.ToString());


                }
            }
            else
            {
                renderTextLog(textbox, "PORT 1 ALREADY OPEN");

            }
        }

        public static void write(SerialPort serialPort,string cmd, TextBox textbox, int port, bool newLine = true)
        {
            try
            {
                if(port == 1)
                {
                    command1 = cmd;
                } else
                {
                    command2 = cmd;
                }
                
                if (newLine)
                {
                    serialPort.WriteLine(cmd);
                }
                else
                {
                    serialPort.Write(cmd);
                }
                
            }catch(Exception err)
            {
                renderTextLog(textbox, err.Message);
            }
        }

        public static void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string s = SERIALPORT1.serial.ReadLine().Trim();
            if(serialMessage1!= s)
            {
                Debug.WriteLine("Contains" + s.Contains("data:"));
                serialMessage1 = s;
                invokeRenderTextLog(textBox1, s);

                // FROM UI;
                if(command1.Contains("doRead"))
                {
                    if (s.Contains("data:"))
                    {
                        // DO FUNCTION
                        string data = s.Replace("data: ", "").Replace(" ", "");
                        MessageBox.Show(Helper.ConverterHex(data));
                    }
                }


                // FROM CARD;
                if (s.Contains("masuk;data:"))
                {
                    // process masuk 
                    Debug.Write("proses masuk");
                    string data = s.Replace("masuk;data: ", "").Replace(" ", "");
                    prosesParkir(Helper.ConverterHex(data), "masuk");
                    
                    MessageBox.Show(Helper.ConverterHex(data));
                }
                else if(s.Contains("keluar;data:"))
                {
                    Debug.Write("proses keluar");
                    string data = s.Replace("keluar;data: ", "").Replace(" ", "");
                    prosesParkir(Helper.ConverterHex(data), "keluar");
                }
            }
        }

        private static void prosesParkir(string ni, string jenis)
        {
            Debug.WriteLine("NO:" + ni);
            string nomor_induk = ni.Trim();
            string path = "Apimobile/proses_parkir";
            RestRequest request = Configuration.getHttpConfig(path);
           
            request.AddJsonBody(
                new
                {
                    nomor_induk = Convert.ToInt32(nomor_induk),
                    jenis = jenis
                }
            );
            IRestResponse<Model.ResponseParkir> response2 = Configuration.CLIENT.Execute<Model.ResponseParkir>(request);
            if (response2 != null)
            {


                if (response2.Data.data == null)
                {

                    MessageBox.Show(response2.Data.message, "Error Status " + response2.Data.status);
                    
                }
                else
                {
                    if(response2.Data.data[0].status == 400)
                    {
                        MessageBox.Show(response2.Data.data[0].message, "Error Status " + response2.Data.data[0].status);
                    }
                    else
                    {
                        Debug.WriteLine(response2.Data.data[0].status);
                    }
                    
                    
                }
            }
            else
            {
                MessageBox.Show("Network/ Server Error!");
                Application.Exit();
            }
        }

        public static void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string s = SERIALPORT2.serial.ReadLine().Trim();
            if(serialMessage2!= s)
            {
                serialMessage2 = s;
                invokeRenderTextLog(textBox2, s);
            }
        }


    }
}
