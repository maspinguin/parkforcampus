using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ParkirClientWindows
{
    class config
    {
        StreamReader reader;
        StreamWriter writer;
        public string PathFile;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);


        public config(string PathFile)
        {

            this.PathFile = PathFile;

        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.PathFile);
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.PathFile);
            return temp.ToString();

        }



        //save config by keyword
        public void SaveLinebyWord(string section, string keyword, string data)
        {
            IniWriteValue(section, keyword, data + "");
        }



        //load a config from config.ini with keyword
        public string LoadConfigByKeyword(string keyword)
        {
            reader = new StreamReader(PathFile);
            string tmp = "";

            do
            {
                tmp = reader.ReadLine();

                if (tmp != null)
                    if (tmp.IndexOf(keyword) > -1)
                    {
                        reader.Close();
                        reader.Dispose();
                        return tmp.Split('=')[1];
                    };
            }
            while (!reader.EndOfStream);


            reader.Close();
            reader.Dispose();

            return tmp;
        }
    }
}
