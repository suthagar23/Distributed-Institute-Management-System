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
    public partial class frmAddBatchTime : Form
    {
        frmMainPage MainScreen;
        private List<int> subject_id;
        private List<int> batch_id;
        private String SelectedClassId;
        public frmAddBatchTime(frmMainPage MainPage)
        {
            InitializeComponent();
            MainScreen = MainPage;

           // if(SelectedClassId>-1) { button4.Text = "Update"; }
           // else { button4.Text = "Add"; }

            subject_id = new List<int>();
            batch_id = new List<int>();
            AddDataToComboBox("select SubjectId,SubjectName from tblSubjects where SubjectStatus='true'", comboBox1, subject_id);
            AddDataToComboBox("select BatchId,BatchName from tblBatch where BatchSatus='true'", comboBox2, batch_id);
            addWeeksDays(comboBox3);
            txtEnd.Focus();
        }

        public void Edit_AddDatatoCombo(String currentClassId, String sid, String bid, String did, string stime, string etime, String aGen, String atten)
        {
            this.SelectedClassId = currentClassId;
            comboBox1.SelectedIndex = subject_id.FindIndex(x => x == int.Parse(sid)); //int.Parse(sid);
            comboBox2.SelectedIndex = batch_id.FindIndex(x => x == int.Parse(bid)); //int.Parse(bid);
            comboBox3.SelectedIndex = int.Parse(did);
            txtStart.Text = stime;
            txtEnd.Text = etime;
            if (aGen.Equals("True")) { checkBox1.CheckState = CheckState.Checked; }
            else { checkBox1.CheckState = CheckState.Unchecked; }
            if (atten.Equals("True")) { checkBox2.CheckState = CheckState.Checked; }
            else { checkBox2.CheckState = CheckState.Unchecked; }
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
            txtStart.Text=txtStart.Text.Replace(".", ":");
            if(checkTime(txtStart.Text)== false && !txtStart.Text.Trim().Equals(""))
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
                    if (checkTime(txtEnd.Text) == true && checkTime(txtStart.Text) == true)
                    {
                        if ((Convert.ToDateTime(txtEnd.Text).Subtract(Convert.ToDateTime(txtStart.Text)).TotalMinutes)>0)
                        {
                            if (button4.Text.Equals("Add")) // Add
                            {
                                AddTimeDetails(subject_id[comboBox1.SelectedIndex].ToString(), batch_id[comboBox2.SelectedIndex].ToString(), comboBox3.SelectedIndex.ToString()
                                    , txtStart.Text, txtEnd.Text, checkBox1.Checked.ToString(), checkBox2.Checked.ToString());
                            }
                            else // Edit
                            {
                                EditTimeDetails(subject_id[comboBox1.SelectedIndex].ToString(), batch_id[comboBox2.SelectedIndex].ToString(), comboBox3.SelectedIndex.ToString()
                                    , txtStart.Text, txtEnd.Text, checkBox1.Checked.ToString(), checkBox2.Checked.ToString());
                            }
                        }
                        else
                        {
                            new frmMessageBox("error", "Class Duration", "Please check the End Time. Time Duration is not valid time. ", false, MainScreen).ShowDialog();
                        }
                    }
                    else
                    {
                        new frmMessageBox("error", "Wrong Format", "Please Enter Correct Start TIme or End Time." + System.Environment.NewLine + "Time Stamp : ##:## in 24 hour format", false, MainScreen).ShowDialog();
                    }
                }
                else
                {
                    new frmMessageBox("error", "Empty", "Please Enter Start Time or end Time!", false, MainScreen).ShowDialog();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void AddTimeDetails(String sid, String bid, String did, string stime, string etime, String aGen, String atten)
        {
            try
            {
                if (clsDatabase_Connection.CheckforDublicate("select count(ClassId) from tblClassTimes where BatchId='" + bid + "' and SubjectId='" + sid + "' and ClassDay='" + did + "' and StartTime='" + stime + "' and EndTime='" + etime + "' and ClassId!='" + SelectedClassId + "'") == true)
                {
                    saveAddDetails(sid, bid, did, stime, etime, aGen, atten);
                    this.Dispose();
                }
                else
                {
                    new frmMessageBox("question", "Duplicate", "You already has this Time Details. Would you like you proceed?", true, MainScreen).ShowDialog();
                    if (IMS_System.Properties.Settings.Default.MessageBoxResults.Equals("Yes"))
                    {
                        saveAddDetails(sid, bid, did, stime, etime, aGen, atten);
                        this.Dispose();
                    }
                    else { txtStart.SelectAll(); }
                    txtStart.SelectAll();
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void saveAddDetails(String sid, String bid, String did, string stime, string etime, String aGen, String atten)
        {
            try
            {
                if (clsDatabase_Connection.ExecuteQuery("Insert into tblClassTimes values ('" + sid + "','" + bid + "','" + did + "','" + stime + "','" + etime + "','"+ getTimeDuration() + "','" + aGen + "','" + atten + "','True','" + IMS_System.Properties.Settings.Default.current_staff_id + "',GETDATE())") == true)
                { MainScreen.SucessMessageShow("Sucessfully Added", "INFO"); }
                else
                { new frmMessageBox("error", "Insert", "You can not add this. Some errors occurred!", false, MainScreen).ShowDialog(); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private string getTimeDuration()
        {
            return (Convert.ToDateTime(txtEnd.Text).Subtract(Convert.ToDateTime(txtStart.Text)).TotalHours).ToString();
        }
        private void EditTimeDetails(String sid, String bid, String did, string stime, string etime, String aGen, String atten)
        {
            try
            {
                if (clsDatabase_Connection.CheckforDublicate("select count(ClassId) from tblClassTimes where BatchId='" + bid + "' and SubjectId='" + sid + "' and ClassDay='" + did + "' and StartTime='" + stime + "' and EndTime='" + etime + "' and ClassId!='"+SelectedClassId+"'") == true)
                {
                    saveEditDetails(sid, bid, did, stime, etime, aGen, atten);
                    this.Dispose();
                }
                else
                {
                    new frmMessageBox("question", "Duplicate", "You already has this Batch name. Would you like you proceed?", true, MainScreen).ShowDialog();
                    if (IMS_System.Properties.Settings.Default.MessageBoxResults.Equals("Yes"))
                    {
                        saveEditDetails(sid, bid, did, stime, etime, aGen, atten);
                        this.Dispose();
                    }
                    else { txtStart.SelectAll(); }
                    txtStart.SelectAll();
                }

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void saveEditDetails(String sid, String bid, String did, string stime, string etime, String aGen, String atten)
        {
            try
            {
                if (clsDatabase_Connection.ExecuteQuery("update tblClassTimes set BatchId='" + bid +
                      "',SubjectId='" + sid + "',ClassDay='" + did + "',StartTime='" + stime + "',EndTime='" + etime +
                      "',Duration='"+ getTimeDuration ()+ "',AutoGenerate='" + aGen + "',AutoAttendance='" + atten +
                      "',CreatedBy='" + IMS_System.Properties.Settings.Default.current_staff_id +
                      "',CreatedDate=GETDATE() where ClassId='" + SelectedClassId + "'") == true)
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
            if (checkTime(txtEnd.Text) == false && !txtEnd.Text.Trim().Equals(""))
            {
                new frmMessageBox("error", "Wrong Format", "Please Enter Correct End Time." + System.Environment.NewLine + "Time Stamp : ##:## in 24 hour format", false, MainScreen).ShowDialog();
                txtEnd.SelectAll();
            }
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
