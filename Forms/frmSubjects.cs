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
    
    public partial class frmSubjects : Form
    {
        private String Query = "";
        frmMainPage MainScreen;
        private String selectedIndex = "0";
        public frmSubjects(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;
            Query = "select SubjectId,subjectName,SubjectStatus from tblSubjects order by SubjectStatus desc,SubjectId desc";
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
                        DeleteSubjectDetails();
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
                    new frmMessageBox("error", "Empty", "Please Select Subject to Delete!", false, MainScreen).ShowDialog();
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
                        new frmMessageBox("error", "Empty", "Please Select Subject to Edit!", false, MainScreen)
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
                    if (char.IsLetter(txtValue.Text.Trim()[0]))
                    {
                        CheckAndAddValues(txtValue.Text.Trim());
                    }
                    else
                    {
                        new frmMessageBox("error", "Empty", "Please Enter valid Subject Name!", false, MainScreen).ShowDialog();
                    }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Enter Subject Name!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void AddSubjectDetails(String SubjectName)
        {
            try
            {

                    if (clsDatabase_Connection.ExecuteQuery("Insert into tblSubjects values ('" + SubjectName + "','True','" + IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE())") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Added", "INFO"); }
                    else
                    { new frmMessageBox("error", "Insert", "You can not add this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                    Add_Details_to_Datagridview();
                    txtValue.Text = "";
  
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void CheckAndAddValues(string subjectName)
        {
            try
            {
                if (button4.Text.Equals("+Add")) // Add
                {
                    clsDatabase_Connection.Get_Table("select SubjectId,SubjectStatus from tblSubjects where subjectName = '" + subjectName + "' order by SubjectStatus desc");
                }
                else // Edit
                {
                    clsDatabase_Connection.Get_Table("select SubjectId,SubjectStatus from tblSubjects where subjectName = '" + subjectName + "' and SubjectId!='"+ dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[0].Value.ToString()+"' order by SubjectStatus desc");
                }
               
                if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                {
                    if (clsDatabase_Connection.objDataSet.Tables[0].Rows[0][1].ToString().Equals("True"))
                    {
                        new frmMessageBox("error", "Duplicate", "You already has this Subject name in Active Mode.Duplicate Subjects not allowed!", false, MainScreen).ShowDialog();
                        txtValue.SelectAll();
                    }
                    else
                    {
                        addEditSelection(subjectName);
                    }
                }
                else
                {
                    addEditSelection(subjectName);
                }
            }
            catch (Exception)
            {  }
        }

        private void addEditSelection(string subjectName)
        {
            if (button4.Text.Equals("+Add")) // Add
            {
                AddSubjectDetails(subjectName);
            }
            else // Edit
            {
                EditSubjectDetails(subjectName);
            }
            txtValue.SelectAll();
            txtValue.Focus();
        }
        private void EditSubjectDetails(String SubjectName)
        {
            try
            {
                    if (clsDatabase_Connection.ExecuteQuery("update tblSubjects set subjectName='" + SubjectName +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where SubjectId='" + selectedIndex + "'") == true)
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

        private void DeleteSubjectDetails()
        {
            try
            {
                if(clsDatabase_Connection.ExecuteQuery("Delete from tblSubjects where SubjectId ='" + selectedIndex + "'")== true)
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
                        clsDatabase_Connection.Get_Table("select SubjectId,SubjectStatus from tblSubjects where subjectName = '" + dgvCart.Rows[dgvCart.CurrentRow.Index].Cells[1].Value.ToString() + "' and SubjectId!='"+selectedIndex+"'");
                        if (clsDatabase_Connection.objDataSet.Tables[0].Rows.Count > 0)
                        {
                                new frmMessageBox("error", "Duplicate",
                                    "You already has this Subject name in Active Mode.Duplicate Subjects not allowed!",
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
                    new frmMessageBox("error", "Empty", "Please Select Subject!", false, MainScreen).ShowDialog();
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
                    if (clsDatabase_Connection.ExecuteQuery("update tblSubjects set SubjectStatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where SubjectId='" + selectedIndex + "'") == true)
                    { MainScreen.SucessMessageShow("Sucessfully Activated", "INFO"); }
                    else
                    { new frmMessageBox("error", "Activate", "You can not Activate this. Some errors occurred!", false, MainScreen).ShowDialog(); }
                }
                else
                {
                    if (clsDatabase_Connection.ExecuteQuery("update tblSubjects set SubjectStatus='" + Value +
                        "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                        "',CreatedDate=GETDATE() where SubjectId='" + selectedIndex + "'") == true)
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

        private void frmSubjects_Load(object sender, EventArgs e)
        {

        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
           e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) ;
        }

        private void btnFun1_Click(object sender, EventArgs e)
        {
            Query = "select SubjectId,subjectName,SubjectStatus from tblSubjects order by SubjectStatus desc,SubjectId desc";
            Add_Details_to_Datagridview();
        }
    }
}
