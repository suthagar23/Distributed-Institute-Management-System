using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS_System.Class.Codes
{
    class clsAccounts
    {
        private static string[] ArrayPaymentMtds = { "By Cash","By Cheque","By Account Deposit" };

        public static string returnPaymentMtds(string i)
        {
            if (ArrayPaymentMtds.Length > int.Parse(i))
            {
                return ArrayPaymentMtds[int.Parse(i)];
            }
            else
            {
                return "0";
            }
        }
    }
}
