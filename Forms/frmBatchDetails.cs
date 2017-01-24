using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMS_System.Class.Forms;
using IMS_System.Class.Database;
using IMS_System.Forms;

namespace IMS_System.Forms
{
    
    public partial class frmBatchDetails : Form
    {
        private String Query = "";
        frmMainPage MainScreen;
        private String selectedIndex = "0";
        public frmBatchDetails(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            Query = "select BatchId,BatchName,BatchSatus from tblBatch order by BatchSatus desc,BatchId desc";
            Add_Details_to_Datagridview();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Add_Details_to_Datagridview()
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
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString()
                            );
                        if(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString().Equals("True"))
                        { dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Black;}
                        else { dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Red; }
                    }
                    dgvCart.ClearSelection();
                }
                else
                {
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvCart.SelectedRows.Count>=1)
                {
                    if (button1.Text.Equals("Delete"))
                    {
                        selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                        DeleteBatchDetails();
                        Add_Details_to_Datagridview();
                    }
                    else
                    {
                        button1.Text = "Delete";
                        button4.Text = "+Add";
                        txtValue.Text = "";
                        txtValue.Focus();
                    }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Select Batch to Delete!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonEditClicked();
        }

        private void buttonEditClicked()
        {
            try
            {
                if (dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[2].Value.ToString().Equals("True"))
                {
                    if (dgvCart.SelectedRows.Count >= 1)
                    {
                        txtValue.Text = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[1].Value.ToString();
                        selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                        button1.Text = "Cancel";
                        button4.Text = "Update";
                        txtValue.SelectAll();
                        txtValue.Focus();
                    }
                    else
                    {
                        new frmMessageBox("error", "Empty", "Please Select Batch to Edit!", false, MainScreen)
                            .ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            submitButtonClicked();
        }

        private void submitButtonClicked()
        {
            try
            {
                if (!txtValue.Text.Trim().Equals(""))
                {
                    CheckAndAddValues(txtValue.Text.Trim());
                    
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Enter Batch Name!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void AddBatchDetails(String BatchName)
        {
            try
            {

                    if (clsDatabase_Connection.ExecuteQuery("Insert into tblBatch values ('" + BatchName + "','True','" + IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE())") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Added", "INFO"); }
                    else
                    { new frmMessageBox("error", "Insert", "You can not add this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                    Add_Details_to_Datagridview();
                    txtValue.Text = "";

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void CheckAndAddValues(string BatchName)
        {
            try
            {

                if (button4.Text.Equals("+Add")) // Add
                {
                    clsDatabase_Connection.Get_Table("select BatchId,BatchSatus from tblBatch where BatchName='" + BatchName + "' order by BatchSatus desc");
                }
                else // Edit
                {
                    clsDatabase_Connection.Get_Table("select BatchId,BatchSatus from tblBatch where BatchName='" + BatchName + "' and BatchId!='"+ dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString()+"' order by BatchSatus desc");
                }
                
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString().Equals("True"))
                    {
                        new frmMessageBox("error", "Duplicate", "You already has this Batch name in Active Mode.Duplicate Batches not allowed!", false, MainScreen).ShowDialog();
                        txtValue.SelectAll();
                    }
                    else
                    {
                        addeditSelection(BatchName);
                    }
                }
                else
                {
                    addeditSelection(BatchName);
                }
            }
            catch (Exception)
            { }
        }

        private void addeditSelection(string BatchName)
        {
            if (button4.Text.Equals("+Add")) // Add
            {
                AddBatchDetails(BatchName);
            }
            else // Edit
            {
                EditBatchDetails(BatchName);
            }
            txtValue.SelectAll();
            txtValue.Focus();
        }

        private void EditBatchDetails(String BatchName)
        {
            try
            {
                    if (clsDatabase_Connection.ExecuteQuery("update tblBatch set BatchName='" + BatchName +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where BatchId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Edited", "INFO"); }
                    else
                    { new frmMessageBox("error", "Edit", "You can not Edit this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                    Add_Details_to_Datagridview();
                    button1.Text = "Delete";
                    button4.Text = "+Add";
                    txtValue.Text = "";
                    txtValue.Focus();  
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void DeleteBatchDetails()
        {
            try
            {
                if(clsDatabase_Connection.ExecuteQuery("Delete from tblBatch where BatchId ='" + selectedIndex + "'")== true)
                { MainScreen.SucessMessageShow("Sucessfully Deleted","INFO"); }
                else
                { new frmMessageBox("error", "Delete", "You can not delete this. Some Records are depend on this Data!", false, MainScreen).ShowDialog(); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void dgvCart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                button5.Enabled = true;
                selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                if (dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[2].Value.Equals("True"))
                {
                    button5.Text = "Deactivate";
                }
                else
                {
                    button5.Text = "Activate";
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCart.SelectedRows.Count >= 1)
                {
                    selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                    if (button5.Text.Equals("Activate"))
                    {
                        clsDatabase_Connection.Get_Table("select BatchId,BatchSatus from tblBatch where BatchName = '" + dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[1].Value.ToString() + "' and BatchId!='" + selectedIndex + "'");
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                        {
                            new frmMessageBox("error", "Duplicate",
                                "You already has this Batch name in Active Mode.Duplicate Subjects not allowed!",
                                false, MainScreen).ShowDialog();
                            txtValue.SelectAll();
                        }
                        else { UpdateActivation("True"); }
                    }
                    else
                    { UpdateActivation("False"); }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Select Batch!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void UpdateActivation(String Value)
        {
            try
            {
                if (Value.Equals("True"))
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblBatch set BatchSatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where BatchId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Activated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Activate", "You can not Activate this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                }
                else
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblBatch set BatchSatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where BatchId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Deactivated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Deactivate", "You can not Deactivate this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                   
                }
                Add_Details_to_Datagridview();
                button5.Enabled = false;
                txtValue.Focus();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void dgvCart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            buttonEditClicked();
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                { submitButtonClicked(); }
            }
            catch  { }
        }

        private void frmBatchDetails_Load(object sender, EventArgs e)
        {

        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void btnFun1_Click(object sender, EventArgs e)
        {
            Query = "select BatchId,BatchName,BatchSatus from tblBatch order by BatchSatus desc,BatchId desc";
            Add_Details_to_Datagridview();
        }
    }
}
