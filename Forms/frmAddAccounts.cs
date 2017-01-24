using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using IMS_System.Class.Database;
using System.Globalization;

namespace IMS_System.Forms
{
    public partial class frmAddAccounts : Form
    {
        frmMainPage MainScreen;
        private List<int> subject_id;
        private List<int> batch_id;
        private String SelectedClassId;
        public frmAddAccounts(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            subject_id = new List<int>();
            batch_id = new List<int>();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            txtAmount.Focus();
        }

        public void Edit_AddDatatoCombo(string currentClassId, string des, string mtd, string mode, string amount )
        {
            this.SelectedClassId = currentClassId;
            comboBox1.SelectedIndex =int.Parse(mtd);
            comboBox2.SelectedIndex = int.Parse(mode);
         
            txtDescription.Text = des;
            txtAmount.Text = amount;
           
            button4.Text = "Update";
            comboBox1.Focus();
        }
     

 
        private void frmAddBatchTime_Load(object sender, EventArgs e)
        {
            label2.Text = IMS_System.Properties.Settings.Default.Currency;
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            
        }

    
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            submitButtonClicked();
        }

        private void submitButtonClicked()
        {
            try
            {
                if (!txtDescription.Text.Trim().Equals("") && !txtAmount.Text.Trim().Equals(""))
                {

                            if (button4.Text.Equals("Add")) // Add
                            {
                                AddTimeDetails(txtDescription.Text, comboBox1.SelectedIndex.ToString(),comboBox2.SelectedIndex.ToString(), txtAmount.Text);
                            }
                            else // Edit
                            {
                                EditTimeDetails(txtDescription.Text, comboBox1.SelectedIndex.ToString(),comboBox2.SelectedIndex.ToString(), txtAmount.Text);
                            }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Enter Description or Amount!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void AddTimeDetails(string des, string mtd, string mode, string amount)
        {
            try
            {
                    saveAddDetails(des, mtd, mode, amount);
                    this.Dispose();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void saveAddDetails(string des, string mtd, string mode, string amount)
        {
            try
            {
                if (clsDatabase_Connection.ExecuteQuery("Insert into tblAccounts values ('" + des + "','" + mtd + "','" + amount + "','" + mode + "',GETDATE(),'"+ IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'True')") == true)
                { MainScreen.SucessMessageShow("Sucessfully Added", "INFO"); }
                else
                { new frmMessageBox("error", "Insert", "You can not add this. Some errors occurred!", false, MainScreen).ShowDialog(); }
            }
            catch (Exception ex)
            { MessageBox.Show("1"+ex.Message); }
        }


        private void EditTimeDetails(string des, string mtd, string mode, string amount)
        {
            try
            {
                    saveEditDetails(des, mtd, mode, amount);
                this.Dispose();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void saveEditDetails(string des, string mtd, string mode, string amount)
        {
            try
            {
                if (clsDatabase_Connection.ExecuteQuery("update tblAccounts set AccountDescription='" + des +
                      "',PaymentMtd='" + mtd + "',Amount='" + amount + "',Income='" + mode + 
                      "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                      "',CreatedDate=GETDATE(),PaymentDate=GETDATE() where AccountId='" + SelectedClassId + "'") == true)
                {
                    MainScreen.SucessMessageShow("Sucessfully Edited", "INFO");
                }
                else
                {
                    new frmMessageBox("error", "Edit", "You can not Edit this. Some errors occurred!", false, MainScreen).ShowDialog();
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void txtEnd_Leave(object sender, EventArgs e)
        {
       
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar);
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
