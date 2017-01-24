using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_System.Class
{
    class SystemLogFile
    {

        public static void WriteSystemLog(string Data,string Source)
        {
            try
            {
                File.WriteAllText(System.Environment.CurrentDirectory + "/Log.txt",
                    DateTime.Now.TimeOfDay.ToString() + " - " + Source+ " - "+ Data);
            }
            catch { }
        }
    }
}
