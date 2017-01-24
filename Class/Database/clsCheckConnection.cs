using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_System.Class.Database
{
    class clsCheckConnection
    {
        public static Boolean Check_Connection(string Server_Address)
        {
            try
            {
                if (Server_Address != "")
                {
                    System.Net.IPHostEntry objIPHost = new System.Net.IPHostEntry();
                    objIPHost = System.Net.Dns.Resolve(Server_Address);
                    System.Net.IPAddress objAddress = default(System.Net.IPAddress);
                    objAddress = objIPHost.AddressList[0];
                    System.Net.Sockets.TcpClient objTCP = new System.Net.Sockets.TcpClient();
                    objTCP.Connect(objAddress, 1433);
                    objTCP.Close();
                    objTCP = null;
                    objAddress = null;
                    objIPHost = null;
                    return true;
                }
                else
                { return false; }
            }
            catch
            { return false; }

        }
    }
}
