using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    class ArduinoHandler: Configuration
    {
        public static void OpenPort1(TextBox textbox)
        {
            if (!SERIALPORT2.IsOpen)
            {
                try
                {
                    SERIALPORT1.Open();
                    renderTextLog(textbox, "Serial Port 1 Open");
                    SERIALPORT1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
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

        public static void OpenPort2(TextBox textbox)
        {
            if (!SERIALPORT2.IsOpen)
            {
                try
                {
                    SERIALPORT2.Open();
                    renderTextLog(textbox, "Serial Port 2 Open");

                    SERIALPORT2.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceived);
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

        public static void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        public static void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }
    }
}
