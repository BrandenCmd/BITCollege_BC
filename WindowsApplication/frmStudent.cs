using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BITCollege_BC.Models;
using Utility;

namespace WindowsApplication
{
    public partial class frmStudent : Form
    {
        ///Given: Student and Registration data will be retrieved
        ///in this form and passed throughout application
        ///These variables will be used to store the current
        ///Student and selected Registration
        ConstructorData constructorData = new ConstructorData();

        BITCollege_BC.Models.BITCollege_BCContext db = new BITCollege_BC.Models.BITCollege_BCContext();

        public frmStudent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given:  This constructor will be used when returning to frmStudent
        /// from another form.  This constructor will pass back
        /// specific information about the student and registration
        /// based on activites taking place in another form
        /// </summary>
        /// <param name="constructorData">Student data passed among forms</param>
        public frmStudent(ConstructorData constructorData)
        {
            InitializeComponent();
            //further code to be added
        }

        /// <summary>
        /// given: open grading form passing constructor data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //instance of frmTransaction passing constructor data
            frmGrading frmGrading = new frmGrading(constructorData);
            //open in frame
            frmGrading.MdiParent = this.MdiParent;
            //show form
            frmGrading.Show();
            this.Close();
        }

        /// <summary>
        /// given: open history form passing data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //instance of frmHistory passing constructor data
            frmHistory frmHistory = new frmHistory(constructorData);
            //open in frame
            frmHistory.MdiParent = this.MdiParent;
            //show form
            frmHistory.Show();
            this.Close();
        }

        /// <summary>
        /// given:  opens form in top right of frame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmStudent_Load(object sender, EventArgs e)
        {
            //keeps location of form static when opened and closed
            this.Location = new Point(0, 0);
        }

        private void registrationNumberComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void creditHoursLabel1_Click(object sender, EventArgs e)
        {

        }

        private void studentNumberMaskedTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                //Ensure the user has completed the requirements for the Mask
                //long studentNumber = long.Parse(this.studentNumberMaskedTextBox.Text);

                //Set the DataSource Property of the BindingSource object representing the Student controls to a
                //LINQ-to-SQL Server query selecting data from the Students Entity Class whose StudentNumber
                //matches the value in the MaskedTextBox

                /*Student student =
                   (from x in db.Students
                    where x.StudentNumber == studentNumber
                    select x).SingleOrDefault();*/

                if (student == null)
                {
                    MessageBox.Show("Student Number does not exist", "Invalid Student Number", MessageBoxButtons.OK);

                    this.lnkUpdate.Enabled = false;
                    this.lnkDetails.Enabled = false;

                    this.studentNumberMaskedTextBox.Focus();

                    //Clear each BindingSource object on the form

                }
                else
                {
                    //Set the DataSource property of the BindingSource object representing the Registration controls
                    //to a LINQ-to-SQL Server query selecting all Registrations whose StudentId corresponds to the
                    //record represented by the StudentNumber value in the MaskedTextBox


                    if (this.constructorData.Registration == null)
                    {
                        this.lnkUpdate.Enabled = false;
                        this.lnkDetails.Enabled = false;

                        //Clear each BindingSource object on the form
                    }
                    else
                    {
                        this.lnkUpdate.Enabled = true;
                        this.lnkDetails.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);

                this.lnkUpdate.Enabled = false;
                this.lnkDetails.Enabled = false;
            }
        }
    }
}
