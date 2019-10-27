using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BITCollege_BC.Models;

namespace BITCollegeSite
{
    public partial class wfRegister : System.Web.UI.Page
    {
        BITCollege_BC.Models.BITCollege_BCContext db = new BITCollege_BC.Models.BITCollege_BCContext();
        ServiceReference1.CollegeRegistrationClient service = new ServiceReference1.CollegeRegistrationClient();

        /// <summary>
        /// Loads the page and populates the control course information using session variables and linq queries.
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
                        Student student = (Student)Session["studentRecord"];

                        this.lblStudentName.Text = student.FullName;

                        IQueryable<Course> courseQuery =
                           (from x in db.Courses
                            where x.ProgramId == student.ProgramId
                            select x);

                        ddlCourses.DataSource = courseQuery.ToList();
                        ddlCourses.DataTextField = "Title";
                        ddlCourses.DataValueField = "CourseId";                       

                        this.DataBind();
                        
                    }
                    catch (Exception ex)
                    {
                        this.lblError.Text = ex.Message;

                        this.lblError.Visible = true;
                    }
                }
            }
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// On click will register the student for the course provided.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbRegister_Click(object sender, EventArgs e)
        {
            try
            { 
                int courseId = int.Parse(ddlCourses.Text);

                Student student = (Student)Session["studentRecord"];

                int registration = service.registerCourse(student.StudentId, courseId, tbNotes.Text);

                //if failed
                if (registration != 0)
                {
                    this.lblError.Text = Utility.BusinessRules.registerError(registration);

                    this.lblError.Visible = true;
                }
                //if success
                else
                {
                    Response.Redirect("~/wfStudent.aspx");
                }
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;

                this.lblError.Visible = true;
            }
        }

        /// <summary>
        /// On click will return the student to the student web form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfStudent.aspx");
        }
    }
}