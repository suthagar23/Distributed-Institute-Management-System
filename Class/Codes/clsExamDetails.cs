using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace IMS_System.Class.Codes
{
    class clsExamDetails
    {
        private static string[] ArrayExamPart= new string[]{ "Part I","Part II","Model Paper","",""};
        private static string[] ArrayExamStatus = { "Under Paper Setting", "Paper Prepared",
            "Ready for Exams", "Exam Finished", "Marks Announced" };

        public static string returnExamParts(string i)
        {
            if (ArrayExamPart.Length > int.Parse(i))
            {
                return ArrayExamPart[int.Parse(i)];
            }
            else
            {
                return "0";
            }
        }

        public static int returnExamPartIndex(string var)
        {
            int results = Array.FindIndex(ArrayExamPart, s => s.Equals(var));
            return results;
        }

        public static string returnExamStatus(string i)
        {
            if (ArrayExamStatus.Length > int.Parse(i))
            {
                return ArrayExamStatus[int.Parse(i)];
            }
            else
            {
                return "0";
            }
        }

        public static int returnExamStatusIndex(string var)
        {
            int results = Array.FindIndex(ArrayExamStatus, s => s.Equals(var));
            return results;
        }

    }
}
