using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS_System.Class.Forms
{
    class clsClose_Other_Forms
    {
        public static bool IMS_IsFormOpen(String name)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == name) { return true; }
            }
            return false;
        }

        public static void Dispose_other_forms(String name)
        {
            try
            {
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                {
                    if (frm.Name == name || frm.Name == "frmMainPage" || frm.Name == "frmSplash")
                    { }
                    else
                    {
                       // MessageBox.Show(frm.Name);
                       frm.Dispose();
                    }
                }
            }
            catch { }
           // catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
