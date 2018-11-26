using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Collections;

namespace E_Social_Auto_Qualificar.controller.licenca
{
    class HardDrive
    {
        private string model = null;
        private string type = null;
        private string serialNo = null;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }
    }

    class Licenca
    {    
        const string _keya = "Cec@";
        const string _keyb = "130310";
        const string _keym = "Cecilia13032010";

        public static string keymaster { get { return _keym; } }
              
        public static string getHdds(int _i)
        {
            ArrayList hdCollection = new ArrayList();

            ManagementObjectSearcher searcher = new
             ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.Model = wmi_HD["Model"].ToString();
                hd.Type = wmi_HD["InterfaceType"].ToString();

                hdCollection.Add(hd);
            }

            searcher = new
             ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                if (hdCollection.Count > i)
                {
                    HardDrive hd = (HardDrive)hdCollection[i];

                    // get the hardware serial no.
                    if (wmi_HD["SerialNumber"] == null)
                        hd.SerialNo = "None";
                    else
                        hd.SerialNo = wmi_HD["SerialNumber"].ToString();
                }

                ++i;
            }

            if(hdCollection.Count > 0){
                var h = (HardDrive)hdCollection[_i];
                return h.SerialNo;
            }

            return "";
        }

        /// <summary>
        /// Chave para envio
        /// </summary>
        /// <param name="hd"></param>
        /// <returns></returns>
        public static string getKey1(string hd)
        {            
            if (hd == "")
                return "";

            return EncryptMd5(_keya + hd + _keyb);
        }

        /// <summary>
        /// Valida Serial
        /// </summary>
        /// <param name="_key1"></param>
        /// <param name="_serial"></param>
        /// <returns></returns>
        public static bool getValida(string _key1, string _serial)
        {
            string key = EncryptMd5(_key1.Trim() + _keym);
            return (key == _serial.Trim());
        }

        /// <summary>
        /// Confirma se o sistema está licenciado;
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public static bool IsLicenciado(string _key)
        {
            if(System.IO.File.Exists("licence.lic")){
                var a = System.IO.File.ReadAllLines("licence.lic");

                foreach (string i in a)
                {
                    if (getValida(_key, i.Trim()))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Criptografia
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptMd5(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
            System.Text.StringBuilder sbString = new System.Text.StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));
            return sbString.ToString();
        }


    }
}
