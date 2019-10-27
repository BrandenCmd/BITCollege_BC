using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BITCollege_BC.Models;

namespace BITCollegeSite
{
    public partial class wfDrop : System.Web.UI.Page
    {
        BITCollege_BC.Models.BITCollege_BCContext db = new BITCollege_BC.Models.BITCollege_BCContext();
        ServiceReference1.CollegeRegistrationClient service = new ServiceReference1.CollegeRegistrationClient();

        /// <summary>
        /// On load collects the selected course from the index at wfStudent.
        /// Displays registration and course information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        QueryBind();
                    }
                    catch (Exception ex)
                    {
                        this.lblError.Text = ex.Message;

                        this.lblError.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// On click drops the course currently shown in the details view control, and returns the user to the student page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbDrop_Click(object sender, EventArgs e)
        {
            try
            {
                long registrationNumber = long.Parse(dvCourseInfo.Rows[0].Cells[1].Text);

                Registration registrationQuery =
                   (from x in db.Registrations
                    where x.RegistrationNumber == registrationNumber
                    select x).SingleOrDefault();

                int registrationToDrop = registrationQuery.RegistrationId;
                // Drop the course currently shown in the detailview control.
                service.dropCourse(registrationToDrop);

                Response.Redirect("~/wfStudent.aspx");
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
             
                this.lblError.Visible = true;
            }
            
        }

        /// <summary>
        /// Redirects the user back to the wfstudent form on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfStudent.aspx");
        }

        /// <summary>
        /// Updates the data view when the selected index of course changes.
        /// Calls the QueryBind method to repopulate controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dvCourseInfo_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            dvCourseInfo.PageIndex = e.NewPageIndex;

            QueryBind();
        }

        /// <summary>
        /// Calls session variables and queries the database for a students course and registration information.
        /// </summary>
        protected void QueryBind()
        {
            string courseNumber = Session["courseNumber"].ToString();

            Student student = (Student)Session["studentRecord"];

            IQueryable<Course> courses = (IQueryable<Course>)Session["courses"];

            Course courseQuery =
                (from x in courses
                 where x.CourseNumber == courseNumber
                 select x).FirstOrDefault();

            IQueryable<Registration> registrationRecords =
                (from x in db.Registrations
                 where x.StudentId == student.StudentId
                 where x.CourseId == courseQuery.CourseId
                 select x);

            dvCourseInfo.DataSource = registrationRecords.ToList();
            this.DataBind();

            //Check for grade is null
            if (dvCourseInfo.Rows[4].Cells[1].Text == "&nbsp;")
            {
                this.lbDrop.Enabled = true;
            }
            else
            {
                this.lbDrop.Enabled = false;
            }
        }
    }
}