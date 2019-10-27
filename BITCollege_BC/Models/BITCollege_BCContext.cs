using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BITCollege_BC.Models
{
    public class BITCollege_BCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BITCollege_BCContext() : base("name=BITCollege_BCContext")
        {
        }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.GPAState> GPAStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.Program> Programs { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.Registration> Registrations { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.AuditCourse> AuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.GradedCourse> GradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.MasteryCourse> MasteryCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.ProbationState> ProbationStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.RegularState> RegularStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.SuspendedState> SuspendedStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.HonoursState> HonoursStates { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.NextStudentNumber> NextStudentNumbers { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.StudentCard> StudentCards { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.NextRegistrationNumber> NextRegistrationNumbers { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.NextGradedCourse> NextGradedCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.NextAuditCourse> NextAuditCourses { get; set; }

        public System.Data.Entity.DbSet<BITCollege_BC.Models.NextMasteryCourse> NextMasteryCourses { get; set; }
    }
}
