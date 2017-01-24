using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_System.Class.Codes
{
    class clsDayConvert
    {

        public static String getWeeDay_Number(string day)
        {
            if (day.Equals("Monday")) { return "0"; }
            else if (day.Equals("Tuesday")) { return "1"; }
            else if (day.Equals("Wednesday")) { return "2"; }
            else if (day.Equals("Thursday")) { return "3"; }
            else if (day.Equals("Friday")) { return "4"; }
            else if (day.Equals("Saturday")) { return "5"; }
            else { return "6"; }

        }
    }
}
