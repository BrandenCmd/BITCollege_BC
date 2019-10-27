using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BITCollege_BC;
using BITCollege_BC.Models;

namespace BITCollegeSite
{
    public partial class wfStudent : System.Web.UI.Page
    {
        BITCollege_BC.Models.BITCollege_BCContext db = new BITCollege_BC.Models.BITCollege_BCContext();

        /// <summary>
        /// On load collects user name and displays is.
        /// On load trims user name and queries for it in the database to return student courses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                lblStudentName.Text = Page.User.Identity.Name;

                if (!IsPostBack)
                {
                    //Tries to populate the grid view, catches exceptions thrown.
                    try
                    {
                        string loginString = base.Page.User.Identity.Name;
                        string loginId = Utility.MyUtilities.Trim(loginString, "@bit.com");
                        long userId = long.Parse(loginId);

                        //Queries and selects student record.
                        Student studentQuery =
                            (from x in db.Students
                             where x.StudentNumber == userId
                             select x).SingleOrDefault();

                        Session["studentRecord"] = studentQuery;

                        lblStudentName.Text = studentQuery.FullName;

                        //Queries and selects the course records for the current student logged in.
                        IQueryable<Course> courseRecords =
                            (from x in db.Courses
                             join i in db.Registrations on 
                             x.CourseId equals i.CourseId
                             where i.StudentId == studentQuery.StudentId
                             select x);

                        Session["courses"] = courseRecords;

                        gvCourseData.DataSource = courseRecords.ToList();
                        this.DataBind();
                    }
                    catch (Exception ex)
                    {
                        this.lblError.Text = ex.Message;

                        this.lblError.Visible = true;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        /// <summary>
        /// Pulls the selected course number and moves the user to the drop webpage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCourseData_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["courseNumber"] = gvCourseData.Rows[gvCourseData.SelectedIndex].Cells[1].Text;

            Response.Redirect("~/wfDrop.aspx");
        }

        /// <summary>
        /// Redirects the user to the Register web form on click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/wfRegister.aspx");
        }
    }
}