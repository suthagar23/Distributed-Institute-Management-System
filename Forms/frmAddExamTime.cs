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
    public partial class frmAddExamTime : Form
    {
        frmMainPage MainScreen;
        private List<int> subject_id;
        private List<int> batch_id;
        private String SelectedClassId;
        public frmAddExamTime(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;

           // if(SelectedClassId>-1) { button4.Text = "Update"; }
           // else { button4.Text = "Add"; }

            subject_id = new List<int>();
            batch_id = new List<int>();
            AddDataToComboBox("select SubjectId,SubjectName from tblSubjects where SubjectStatus='true'", comboBox1, subject_id);
            AddDataToComboBox("select BatchId,BatchName from tblBatch where BatchSatus='true'", comboBox2, batch_id);
            //addWeeksDays(comboBox3);
            txtEnd.Focus();
        }

        public void Edit_AddDatatoCombo(String currentClassId, String sid, String bid, String did, string stime, string etime, int partId, int statusId)
        {
            this.SelectedClassId = currentClassId;
            comboBox1.SelectedIndex = subject_id.FindIndex(x => x == int.Parse(sid)); //int.Parse(sid);
            comboBox2.SelectedIndex = batch_id.FindIndex(x => x == int.Parse(bid)); //int.Parse(bid);
           // comboBox3.SelectedIndex = int.Parse(did);
            try
            {
                // MessageBox.Show(did);
              //  dateTimePicker1.Value = DateTime.Parse(did);
                dateTimePicker1.Value= DateTime.ParseExact(did, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            { MessageBox.Show("11 " + ex.Message); }

            txtStart.Text = stime;
            txtEnd.Text = etime;
            comboBox4.SelectedIndex = partId;
            comboBox5.SelectedIndex = statusId;
            button4.Text = "Update";
            comboBox1.Focus();
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

        private void addWeeksDays(ComboBox cmbBox)
        {
            cmbBox.Items.Add("Sunday");
            cmbBox.Items.Add("Monday");
            cmbBox.Items.Add("Tuesday");
            cmbBox.Items.Add("Wednesday");
            cmbBox.Items.Add("Thursday");
            cmbBox.Items.Add("Friday");
            cmbBox.Items.Add("Saturday");
            cmbBox.SelectedIndex = 0;
        }
        private void frmAddBatchTime_Load(object sender, EventArgs e)
        {

        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            txtStart.Text= txtStart.Text.Replace(".", ":");
            if (checkTime(txtStart.Text)== false && !txtStart.Text.Trim().Equals(""))
            {
                new frmMessageBox("error", "Wrong Format", "Please Enter Correct Start or End Time!"+System.Environment.NewLine+"Time Stamp : ##:## in 24 hour format", false, MainScreen).ShowDialog();
                txtStart.SelectAll();
            }
        }

        private Boolean checkTime(string textTime)
        {
            try { 
                DateTime time = new DateTime(); // Passed result if succeed 
                if (DateTime.TryParseExact(textTime, "HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out time))
                {
                    if (int.Parse(textTime.Substring(0, 2)) <= 24)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
        catch {return false; }
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
                if (!txtStart.Text.Trim().Equals("") && !txtEnd.Text.Trim().Equals(""))
                {
                    if (checkTime(txtStart.Text) == true && int.Parse(txtEnd.Text.Substring(0, 1)) <= 3)
                    {
                            if (button4.Text.Equals("Add")) // Add
                            {
                                AddTimeDetails(subject_id[comboBox1.SelectedIndex].ToString(), batch_id[comboBox2.SelectedIndex].ToString(), dateTimePicker1.Value.ToString("dd/MM/yyyy")
                                    , txtStart.Text, txtEnd.Text, comboBox4.SelectedIndex,comboBox5.SelectedIndex);
                            }
                            else // Edit
                            {
                                EditTimeDetails(subject_id[comboBox1.SelectedIndex].ToString(), batch_id[comboBox2.SelectedIndex].ToString(), dateTimePicker1.Value.ToString("dd/MM/yyyy")
                                    , txtStart.Text, txtEnd.Text, comboBox4.SelectedIndex, comboBox5.SelectedIndex);
                            }

                    }
                    else
                    {
                        new frmMessageBox("error", "Wrong Format", "Please Enter Correct Start TIme or Duration." + System.Environment.NewLine + "Time Stamp : ##:## in 24 hour format", false, MainScreen).ShowDialog();
                    }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Enter Start Time or Duration!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show("1 "+ex.Message); }
        }

        private void AddTimeDetails(String sid, String bid, String did, string stime, string etime, int partId, int statusId)
        {
            try
            {
               // MessageBox.Show(did);
                if (clsDatabase_Connection.CheckforDublicate("select count(ExamId) from tblExamDetails where BatchId='" + bid + "' and SubjectId='" + sid + "' and ExamDay='" + did + "' and StartTime='" + stime + "' and Duration='" + etime + "' and ExamPart='"+partId+"' and ExamId!='" + SelectedClassId + "'") == true)
                {
                    saveAddDetails(sid, bid, did, stime, etime, partId, statusId);
                    this.Dispose();
                }
                else
                {
                    new frmMessageBox("question", "Duplicate", "You already has this Exam Details. Would you like you proceed?", true, MainScreen).ShowDialog();
                    if (IMS_System.Properties.Settings.Default.MessageBoxResults.Equals("Yes"))
                    {
                        saveAddDetails(sid, bid, did, stime, etime, partId, statusId);
                        this.Dispose();
                    }
                    else { txtStart.SelectAll(); }
                    txtStart.SelectAll();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void saveAddDetails(String sid, String bid, String did, string stime, string etime, int partId, int statusId)
        {
            try
            {
                if (clsDatabase_Connection.ExecuteQuery("Insert into tblExamDetails values ('" + sid + "','" + partId + "','" + bid + "','" + did + "','" + stime + "','" + etime + "','" + statusId + "','" + IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE(),'True')") == true)
                { MainScreen.SucessMessageShow("Sucessfully Added", "INFO"); }
                else
                { new frmMessageBox("error", "Insert", "You can not add this. Some errors occurred!", false, MainScreen).ShowDialog(); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

       
        private void EditTimeDetails(String sid, String bid, String did, string stime, string etime, int partId, int statusId)
        {
            try
            {
                if (clsDatabase_Connection.CheckforDublicate("select count(ExamId) from tblExamDetails where BatchId='" + bid + "' and SubjectId='" + sid + "' and ExamDay='" + did + "' and StartTime='" + stime + "' and Duration='" + etime + "' and ExamPart='" + partId + "' and ExamId!='" + SelectedClassId + "'") == true)
                {
                    saveEditDetails(sid, bid, did, stime, etime, partId, statusId);
                    this.Dispose();
                }
                else
                {
                    new frmMessageBox("question", "Duplicate", "You already has this Exam Details. Would you like you proceed?", true, MainScreen).ShowDialog();
                    if (IMS_System.Properties.Settings.Default.MessageBoxResults.Equals("Yes"))
                    {
                        saveEditDetails(sid, bid, did, stime, etime, partId, statusId);
                        this.Dispose();
                    }
                    else { txtStart.SelectAll(); }
                    txtStart.SelectAll();
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void saveEditDetails(String sid, String bid, String did, string stime, string etime, int partId, int statusId)
        {
            try
            {
                if (clsDatabase_Connection.ExecuteQuery("update tblExamDetails set BatchId='" + bid +
                      "',SubjectId='" + sid + "',ExamDay='" + did + "',StartTime='" + stime + "',Duration='" + etime +
                      "',ExamPart='" + partId + "',ExamStatus='" + statusId +
                      "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                      "',CreatedDate=GETDATE() where ExamId='" + SelectedClassId + "'") == true)
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
            txtEnd.Text=txtEnd.Text.Replace(".", ":");
        }

        private void txtStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar!='.';
        }

        private void txtEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '.';
        }
    }
}
