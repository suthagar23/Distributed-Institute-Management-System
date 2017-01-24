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
    
    public partial class frmSubjectFees : Form
    {
        private String Query = "";
        frmMainPage MainScreen;
        private String selectedIndex = "0";
        private List<int> subject_id;
        private List<int> batch_id;

        public frmSubjectFees(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            subject_id = new List<int>();
            batch_id = new List<int>();
            AddDataToComboBox("select BatchId,BatchName from tblBatch where BatchSatus='true'", comboBox1, batch_id);
            AddDataToComboBox("select SubjectId,SubjectName from tblSubjects where SubjectStatus='true'", comboBox2, subject_id);
            Query = "select A.SubjectPaymentId,B.SubjectName,C.BatchName,A.Amount,A.AmountStatus,A.SubjectId,A.BatchId from tblSubjectAmount A inner join tblSubjects B on A.SubjectId=B.SubjectId inner join tblBatch C on A.BatchId=C.BatchId where B.SubjectStatus='True' and C.BatchSatus='True' order by A.AmountStatus desc,A.SubjectPaymentId desc";
            Add_Details_to_Datagridview();
        }

        private void AddDataToComboBox(String Query, ComboBox cmbBox, List<int> idList)
        {
            try
            {
                idList.Clear();
                cmbBox.Items.Clear();
                clsDatabase_Connection.Get_Table(Query);
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < clsDatabase_Connection.objDataSet.Tables[0].Rows.Count; i++)
                    {
                        idList.Add((int)clsDatabase_Connection.objDataSet.Tables[0].Rows[i][0]);
                        cmbBox.Items.Add(clsDatabase_Connection.objDataSet.Tables[0].Rows[i][1].ToString());
                    }
                    cmbBox.SelectedIndex = 0;
                }
                else
                { MessageBox.Show("Please add values before using"); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            buttonEditClicked();
        }

        private void buttonEditClicked()
        {
            try
            {
                if (dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[4].Value.ToString().Equals("True"))
                {
                    if (dgvCart.SelectedRows.Count >= 1)
                    {
                        MessageBox.Show((dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[5].Value.ToString()));

                        txtValue.Text = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[3].Value.ToString();
                        selectedIndex = dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString();
                        comboBox1.SelectedIndex = subject_id [int.Parse(dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[5].Value.ToString())];
                        comboBox2.SelectedIndex = batch_id[ int.Parse(dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[6].Value.ToString())];
                        button1.Text = @"Cancel";
                        button4.Text = @"Update";
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
                    CheckAndAddValues(comboBox1.SelectedIndex,comboBox2.SelectedIndex,txtValue.Text.Trim());

                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Enter Subject amount!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void CheckAndAddValues(int subjectid,int batchid,string amount)
        {
            try
            {

                if (button4.Text.Equals("+Add")) // Add
                {
                    clsDatabase_Connection.Get_Table("select A.SubjectPaymentId,A.AmountStatus from tblSubjectAmount A where A.SubjectId='" + subjectid + "' and A.BatchId='"+ batchid + "' and A.Amount='"+ amount + "' order by A.AmountStatus desc");
                }
                else // Edit
                {
                    clsDatabase_Connection.Get_Table("select A.SubjectPaymentId,A.AmountStatus from tblSubjectAmount A where A.SubjectId='" + subjectid + "' and A.BatchId='" + batchid + "' and A.Amount='" + amount + "' and A.SubjectPaymentId!='" + dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString() + "' order by A.AmountStatus desc");
                }

                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString().Equals("True"))
                    {
                        new frmMessageBox("error", "Duplicate", "You already has this Details in Active Mode.Duplicate Amounts are not allowed!", false, MainScreen).ShowDialog();
                        txtValue.SelectAll();
                    }
                    else
                    {
                        addeditSelection(subjectid, batchid, amount);
                    }
                }
                else
                {
                    addeditSelection(subjectid,batchid,amount);
                }
            }
            catch (Exception)
            { }
        }

        private void addeditSelection(int subjectid, int batchid, string amount)
        {
            if (button4.Text.Equals("+Add")) // Add
            {
                AddBatchDetails(subjectid, batchid, amount);
            }
            else // Edit
            {
                EditBatchDetails(subjectid, batchid, amount);
            }
            txtValue.SelectAll();
            txtValue.Focus();
        }

        private void AddBatchDetails(int subjectid, int batchid, string amount)
        {
            try
            {
                Clipboard.SetText(("Insert into tblSubjectAmount values ('" + subject_id[subjectid] + "','" + batch_id[batchid] + "','" + amount + "','True','" + IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'" + checkBox2.CheckState + "')"));
                if (clsDatabase_Connection.ExecuteQuery("Insert into tblSubjectAmount values ('" + subject_id[subjectid] +"','"+batch_id[batchid]+"','"+amount+ "','True','" + IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'"+checkBox2.CheckState+"')") == true)
                { MainScreen.SucessMessageShow("Sucessfully Added", "INFO"); }
                else
                { new frmMessageBox("error", "Insert", "You can not add this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                Add_Details_to_Datagridview();
                txtValue.Text = "";

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void EditBatchDetails(int subjectid, int batchid, string amount)
        {
            try
            {
                if (clsDatabase_Connection.ExecuteQuery("update tblSubjectAmount set SubjectId='" + subjectid +
                    "',BatchId='"+batchid+"','Amount='"+amount+ "',AllowToAllBatch='"+checkBox2.CheckState+
                    "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                    "',CreatedDate=GETDATE() where SubjectPaymentId='" + selectedIndex + "'") == true)
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



        private void dgvCart_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonEditClicked();
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
                        clsDatabase_Connection.Get_Table("select A.SubjectPaymentId,A.AmountStatus from tblSubjectAmount A where A.SubjectId='" + dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[5].Value.ToString() + "' and A.BatchId='" + dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[6].Value.ToString() + "' and A.Amount='" + dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[3].Value.ToString() + "' and A.SubjectPaymentId!='" + dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString() + "' order by A.AmountStatus desc");
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                        {
                            new frmMessageBox("error", "Duplicate",
                                "You already has this details in Active Mode.Duplicate amount is not allowed!",
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
                    if (clsDatabase_Connection.ExecuteQuery("update tblSubjectAmount set AmountStatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where SubjectPaymentId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Activated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Activate", "You can not Activate this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                }
                else
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblSubjectAmount set AmountStatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where SubjectPaymentId='" + selectedIndex + "'") == true)
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
          
        }


        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
        
        }

        private void frmBatchDetails_Load(object sender, EventArgs e)
        {

        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar);
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
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][2].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][3].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][5].ToString(),
                            clsDatabase_Connection.objDataSet.Tables[0].Rows[i][6].ToString()
                            );
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows[i][4].ToString().Equals("True"))
                        { dgvCart.Rows[i].DefaultCellStyle.ForeColor = Color.Black; }
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
    }
}
