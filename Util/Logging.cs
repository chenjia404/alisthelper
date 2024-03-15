using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alisthelper.Util
{
    internal class Logging
    {
        public static bool Log(Exception ex)
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\log"))
                    Directory.CreateDirectory(Application.StartupPath + @"\log");
                string filename = Application.StartupPath + @"\log\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                using (StreamWriter sw = new StreamWriter(@filename, true))//覆盖模式写入
                {
                    sw.WriteLine(DateTime.Now.ToLongTimeString() + ex.Source + ":" + " :" + ex.Message + ex.StackTrace);
                    sw.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public static bool Log(string msg)
        {
            try
            {
                if (!Directory.Exists(Application.StartupPath + @"\log"))
                    Directory.CreateDirectory(Application.StartupPath + @"\log");
                string filename = Application.StartupPath + @"\log\" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                using (StreamWriter sw = new StreamWriter(@filename, true))//覆盖模式写入
                {
                    sw.WriteLine(DateTime.Now.ToLongTimeString() + " :" + msg);
                    sw.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
