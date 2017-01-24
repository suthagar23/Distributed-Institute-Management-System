using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_System.Class.GUI;
using IMS_System.Class.Forms;
using IMS_System.Class.Database;
using System.Data.SqlClient;
using System.Xml.Serialization;
using IMS_System.Class.Codes;

namespace IMS_System.Forms
{
    public partial class frmAccounts : Form
    {
        frmMainPage MainScreen;
       // private String Query = "";
        private List<int> batch_id;
        private string DgvQuery;
        public frmAccounts(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            batch_id = new List<int>();

            DgvQuery= "select A.AccountId,A.AccountDescription,A.PaymentMtd,A.Amount,A.Income,A.PaymentDate,A.Status,SUBSTRING(C.StafFirstName, 1, 1)+'.'+C.StafLastName from tblAccounts A inner join tblUser B on A.CreatedBy=B.UserId inner join tblStaffDetails C on C.StaffId=B.StaffId order by A.Status desc,A.AccountId desc";
            Add_Details_to_Datagridview(DgvQuery);
            
        }

        private void AddIncomeExpense()
        {
            try
            {
                float[] accounts=new float[2];
                string IncomeQry = "select IIF(sum(A.Amount)!='', sum(A.Amount), 0) from tblAccounts A where A.Income = '1' ";
                string ExpenseQry = "select IIF(sum(A.Amount)!='', sum(A.Amount), 0) from tblAccounts A where A.Income = '0' ";

                if (checkBox2.Checked == true)
                { IncomeQry = IncomeQry + " and A.Status='True' "; ExpenseQry = ExpenseQry + " and A.Status='True' "; }
                if (!txtValue.Text.Trim().Equals(""))
                { IncomeQry = IncomeQry + " and A.AccountDescription like '" + txtValue.Text + "%'"; ExpenseQry = ExpenseQry + " and A.AccountDescription like '" + txtValue.Text + "%'"; }
                if (comboBox2.SelectedIndex == 1)
                { IncomeQry = IncomeQry + " and A.Income='1' "; ExpenseQry = ExpenseQry + " and A.Income='0' "; }
                else if (comboBox2.SelectedIndex == 2)
                { IncomeQry = IncomeQry + " and A.Income='1' "; ExpenseQry = ExpenseQry + " and A.Income='0' "; }

                Clipboard.SetText(ExpenseQry);
                clsDatabase_Connection.Get_Table(IncomeQry +";"+ ExpenseQry);
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    label1.Text = @"Income : " + IMS_System.Properties.Settings.Default.Currency +
                                  clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString();
                    accounts[0] = float.Parse(clsDatabase_Connection.objDataSet.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    label1.Text = @"Income : " + IMS_System.Properties.Settings.Default.Currency + "00.00";
                    accounts[0] = 0;
                }
                if (clsDatabase_Connection.objDataSet.Tables[1].Rows.Count > 0)
                {
                    label2.Text = @"Expense : " + IMS_System.Properties.Settings.Default.Currency +
                                  clsDatabase_Connection.objDataSet.Tables[1].Rows[0][0].ToString();
                    accounts[1] = float.Parse(clsDatabase_Connection.objDataSet.Tables[1].Rows[0][0].ToString());
                }
                else
                {
                    label2.Text = @"Income : " + IMS_System.Properties.Settings.Default.Currency + "00.00";
                    accounts[1] = 0;
                }
                label4.Text = @"PROFIT : " + IMS_System.Properties.Settings.Default.Currency + (accounts[0] - accounts[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmBatches_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            clsLines.Draw_Horizontal_lines(e, Color.Black, label6);
        }

        public void Add_Details_to_Datagridview(String Query)
        {
            try
            {
                dgvCart.Rows.Clear();
                clsDatabase_Connection.Get_Table(Query);
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    dgvCart.Visible = true;
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        dgvCart.Rows.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString(),
                            clsAccounts.returnPaymentMtds(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString()),
                            IMS_System.Properties.Settings.Default.Currency+clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][6].ToString(),
                             clsDatabase_Connection.objDataSet.Tables[0].Rows[i][7].ToString(),
                             clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString()
                            );
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][6].ToString().Equals("True"))
                        { dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Black; }
                        else { dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Red; }

                        dgvCart.ClearSelection();
                      
                    }
                   
                }
                else
                {
                    //dgvCart.Visible = false;
                    // label2.Text = "There are no Details";
                    //  label2.ForeColor = Color.Red;
                }
                AddIncomeExpense();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_DockChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                setDgvQuery();
                Add_Details_to_Datagridview(DgvQuery);
                //if (comboBox1.SelectedIndex==0)
                //{
                //    Add_Details_to_Datagridview(DgvQuery);
                //}
                //else
                //{
                //    DgvQuery= DgvQuery+ " and A.BatchId=" + batch_id[comboBox1.SelectedIndex];
                //    Add_Details_to_Datagridview(DgvQuery); 
                //}
              }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void setDgvQuery()
        {
            DgvQuery = "select A.AccountId,A.AccountDescription,A.PaymentMtd,A.Amount,A.Income,A.PaymentDate,A.Status,SUBSTRING(C.StafFirstName, 1, 1)+'.'+C.StafLastName from tblAccounts A inner join tblUser B on A.CreatedBy=B.UserId inner join tblStaffDetails C on C.StaffId=B.StaffId where B.UserStatus='True'";

            if (checkBox2.Checked == true)
            { DgvQuery = DgvQuery + " and A.Status='True' "; }
            if (!txtValue.Text.Trim().Equals(""))
            { DgvQuery = DgvQuery + " and A.AccountDescription like '" + txtValue.Text + "%'"; }
            if (comboBox2.SelectedIndex==1)
            { DgvQuery = DgvQuery + " and A.Income='1' "; }
            else if (comboBox2.SelectedIndex == 2)
            { DgvQuery = DgvQuery + " and A.Income='0' "; }
             
            if (checkBox1.Checked==true)
            { DgvQuery = DgvQuery + " order by A.PaymentDate asc,A.AccountId desc"; }
            else { DgvQuery = DgvQuery + " order by A.Status desc,A.AccountId desc"; }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clsClose_Other_Forms.IMS_IsFormOpen("frmAddAccounts") == false)
            {
                frmAddAccounts AddAccounts = new frmAddAccounts(MainScreen);
                AddAccounts.ShowDialog();
                AddAccounts.BringToFront();
                Add_Details_to_Datagridview(DgvQuery);

            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            editSelectedData();
        }

        private void editSelectedData()
        {
            try
            {
                if (clsClose_Other_Forms.IMS_IsFormOpen("frmAddAccounts") == false)
                {
                    frmAddAccounts  AddAccounts = new frmAddAccounts(MainScreen);
                    AddAccounts.Edit_AddDatatoCombo(dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[1].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[8].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[4].Value.ToString(),
                    dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[3].Value.ToString().Replace(IMS_System.Properties.Settings.Default.Currency,""));

                    AddAccounts.ShowDialog();
                    AddAccounts.BringToFront();

                    Add_Details_to_Datagridview(DgvQuery);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }



        private void dgvCart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnEdit.Enabled == true) { editSelectedData(); }

        }

        private void dgvCart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                button1.Enabled = true;
               // selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                if (dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[6].Value.Equals("True"))
                {
                    button1.Text = @"Deactivate";
                    btnEdit.Enabled = true;
                }
                else
                {
                    button1.Text = @"Activate";
                    btnEdit.Enabled = false;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCart.SelectedRows.Count >= 1)
                {
                   // selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                    if (button1.Text.Equals("Activate"))
                    { UpdateActivation("True", dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString()); }
                    else
                    { UpdateActivation("False", dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString()); }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Select Exam Details!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateActivation(String Value,string selectedIndex)
        {
            try
            {
                if (Value.Equals("True"))
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblAccounts set Status='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where AccountId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Activated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Activate", "You can not Activate this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                }
                else
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblAccounts set Status='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where AccountId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Deactivated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Deactivate", "You can not Deactivate this. Some errors occurred!", false, MainScreen).ShowDialog(); }

                }
                Add_Details_to_Datagridview(DgvQuery);
                button1.Enabled = false;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            setDgvQuery();
            Add_Details_to_Datagridview(DgvQuery);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            setDgvQuery();
            Add_Details_to_Datagridview(DgvQuery);
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void comboBox2_DropDownClosed_1(object sender, EventArgs e)
        {
            try
            {
                setDgvQuery();
                Add_Details_to_Datagridview(DgvQuery);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            setDgvQuery();
            Add_Details_to_Datagridview(DgvQuery);
        }
    }
}
