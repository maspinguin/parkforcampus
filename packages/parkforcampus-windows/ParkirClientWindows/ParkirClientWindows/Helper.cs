﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkirClientWindows
{
    class Helper
    {
        public static string ConverterHex(string hexString)
        {
            try
            {
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length - 4; i += 2)
                {
                    string hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    ulong decval = Convert.ToUInt64(hs, 16);
                    long deccc = Convert.ToInt64(hs, 16);
                    char character = Convert.ToChar(deccc);
                    ascii += character;

                }
                //Debug.WriteLine("convert: " + ascii);
                return ascii;
            }
            catch (Exception ex) {
               //Debug.WriteLine("convert: " + ex.Message);
                return string.Empty;

            }

            //return string.Empty;
        }

        public static string ASCIItoHex(string Value)
        {
            /*
            if(Value.Length < 16)
            {
                string addZero = "";
                for(int i= 0; i< 16-Value.Length; i++)
                {
                    addZero += "";
                }
                Value += addZero;
               
            }
            */
            
            StringBuilder sb = new StringBuilder();

            byte[] inputByte = Encoding.UTF8.GetBytes(Value);

            foreach (byte b in inputByte)
            {
                sb.Append(string.Format("{0:x2}", b));
            }

            string newString = sb.ToString();

            if (newString.Length < 40)
            {
                string addZero = "";
                for (int i = 0; i <  40 - newString.Length; i++)
                {
                    addZero += "0";
                }
                newString += addZero;

            }
            Debug.WriteLine("new string: "+ newString);
            return newString;
        }
    }
}
