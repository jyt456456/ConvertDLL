using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IniClass
{
    public class IniC
    {
        private string m_strFilename;
        public IniC(string strFilename)
        {
            m_strFilename = strFilename;
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public void SetIni(string _Section, string _Key, string _Value)
        {
            WritePrivateProfileString(_Section, _Key, _Value, System.IO.Directory.GetCurrentDirectory() + "\\" + m_strFilename);
        }
        public string GetIni(string _Section, string _key)
        {
            StringBuilder STBD = new StringBuilder(1000);
            GetPrivateProfileString(_Section, _key, null, STBD, 5000, System.IO.Directory.GetCurrentDirectory() + "\\" + m_strFilename);
            return STBD.ToString().Trim();
        }
    }
}
