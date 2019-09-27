using System.IO.Ports;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    class SerialPortHandler: Configuration
    {
        public SerialPort serial { get; set; }
        
        public SerialPortHandler()
        {
            this.serial = new SerialPort();
           
        }
        
    }
}
