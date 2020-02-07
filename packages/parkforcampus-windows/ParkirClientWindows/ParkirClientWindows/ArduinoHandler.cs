using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    class ArduinoHandler: Configuration
    {
        static TextBox textBox1;
        static TextBox textBox2;
        TextBox textBoxLogApplication;
        private MainForm form;
        static string command1 = "", command2="";
        
        public static string serialMessage1 { get;private set; }
        public static string serialMessage2 { get;private set; }

        public ArduinoHandler(MainForm form1, TextBox textBoxLogApplication)
        {
            this.form = form1;
            this.textBoxLogApplication = textBoxLogApplication;
        }
       
        public void OpenPort1(TextBox textbox)
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

        public void OpenPort2(TextBox textbox)
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

        public static void write(SerialPort serialPort,string cmd, TextBox textbox = null, int port = 1, bool newLine = true)
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
                if(textbox != null)
                {
                    renderTextLog(textbox, err.Message);
                }
                
            }
        }

        public void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string s = SERIALPORT1.serial.ReadLine().Trim();
            if(serialMessage1!= s)
            {
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
                    data = Helper.ConverterHex(data);
                    prosesParkir(data, "masuk");
                    
                   // MessageBox.Show(Helper.ConverterHex(data));
                }
                else if(s.Contains("keluar;data:"))
                {
                    Debug.Write("proses keluar");
                    
                    string data = s.Replace("keluar;data: ", "").Replace(" ", "");
                    data = Helper.ConverterHex(data);
                    prosesParkir(data, "keluar");
                }
            }
        }

        private void prosesParkir(string ni, string jenis)
        {
            
            //Debug.WriteLine("NO:" + ni);
            string no_kartu = ni.Trim();
            string path = "Apimobile/proses_parkir";
            RestRequest request = Configuration.getHttpConfig(path);

            //nomor_induk = Convert.ToString(ni);
            int id = 0;
            int.TryParse(no_kartu, out id);
           

            request.AddJsonBody(
                new
                {
                   no_kartu = id,
                    jenis = jenis
                }
            );
            IRestResponse<Model.ResponseParkir> response2 = Configuration.CLIENT.Execute<Model.ResponseParkir>(request);
            if (response2 != null)
            {


                if (response2.Data.data == null)
                {

                    invokeRenderTextLog(textBoxLogApplication, "Error Status " + response2.Data.status + " : " + response2.Data.message + "("+id+")");
                    
                }
                else
                {
                    if(response2.Data.data[0].status == 400)
                    {
                        invokeRenderTextLog(textBoxLogApplication, "Error Status " + response2.Data.data[0].status + " : " + response2.Data.data[0].message + "("+id+")");

                        //MessageBox.Show(response2.Data.data[0].message, "Error Status " + response2.Data.data[0].status);
                    }
                    else
                    {
                        this.form.getListParkir();
                        invokeRenderTextLog(textBoxLogApplication, "Success:" + response2.Data.data[0].status + " : " + response2.Data.data[0].message + "(" + id + ")");
                        write(SERIALPORT1.serial, "openPortal;");
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

        public void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
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
