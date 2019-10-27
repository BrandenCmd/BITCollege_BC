using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility;
using System.Data;
using System.Data.SqlClient;

namespace BITCollege_BC.Models
{
    /// <summary>
    /// Implementation for the student class from the class diagram
    /// stateChange method provices functionality for a changing the GPAState of a student
    /// </summary>
    public class Student
    {
        protected static BITCollege_BCContext db = new BITCollege_BCContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("gPAState")]
        public int GPAStateId { get; set; }
        
        [ForeignKey("program")]
        public int? ProgramId { get; set; }

        [Display(Name ="Student\nNumber")]
        public long StudentNumber { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name ="First\nName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        [Display(Name ="Last\nName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string Address { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 1)]
        public string City { get; set; }

        [Required(ErrorMessage ="Must enter a Province (Format: MB)")]
        [StringLength(2)]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK)$", ErrorMessage ="Error (Format: MB)")]
        public string Province { get; set; }

        [Required(ErrorMessage ="Must enter a Postal Code (Format: A1A 1A1)")]
        [StringLength(7)]
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage ="Error (Format: A1A 1A1")]
        [Display(Name ="Postal\nCode")]
        public string PostalCode { get; set; }
        
        [Required]
        [Display(Name ="Date\nCreated")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime DateCreated { get; set; }

        [Display(Name ="Grade\nPoint\nAverage")]
        [DisplayFormat(DataFormatString ="{0:n}")]
        [Range(0, 4.5)]
        public double? GradePointAverage { get; set; }

        [Display(Name ="Outstanding\nFees")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double OutstandingFees { get; set; }

        public string Notes { get; set; }

        [Display(Name ="Name")]
        public string FullName
        {
            get
            {
                return string.Format($"{FirstName} {LastName}");
            }
        }

        [Display(Name ="Address")]
        public string FullAddress
        {
            get
            {
                return string.Format($"{Address} {City} {Province}, {PostalCode}");
            }
        }

        /// <summary>
        /// Sets the student number to the next available number
        /// </summary>
        public void setNextStudentNumber()
        {
            this.StudentNumber = (long)StoredProcedures.nextNumber("NextStudentNumbers");
        }

        /// <summary>
        /// Facilitates the changing of GPAStates for student when a large jump happens.
        /// </summary>
        public void changeState()
        {
            int oldGpaStateId = this.GPAStateId;
            int newGpaStateId;

            do
            {
                newGpaStateId = oldGpaStateId;
                db.GPAStates.Where(x => x.GPAStateId == oldGpaStateId).Select(x => x).SingleOrDefault().stateChangeCheck(this);
                oldGpaStateId = this.GPAStateId; 
            } while (oldGpaStateId != newGpaStateId);
        }

        //Navigation property - coded for a 1 on the class diagram
        //Virtual will allow for .notation access to related tables
        public virtual GPAState gPAState { get; set; }

        public virtual Program program { get; set; }

        public virtual ICollection<Registration> registration { get; set; }

        //Navigation property -- Assignment milestone 4
        public virtual ICollection<StudentCard> studentCard { get; set; }
    }

    /// <summary>
    /// Provides implementation of the StudentCard model from the class diagram -- Milestone 4
    /// </summary>
    public class StudentCard
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentCardId { get; set; }

        public long CardNumber { get; set; }

        [Required]
        [ForeignKey("student")]
        public int StudentId { get; set; }

        //Coded for navigation to the student model on the class diagram
        //Navigation property
        public virtual Student student { get; set; }
    }

    /// <summary>
    /// Provides implementation for the NextStudentNumber class from the class diagram -- Milestone 4
    /// </summary>
    public class NextStudentNumber
    {
        protected static BITCollege_BCContext db = new BITCollege_BCContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextStudentNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextStudentNumber nextStudentNumber;

        private NextStudentNumber()
        {
            NextAvailableNumber = 20000000;
        }

        /// <summary>
        /// Returns an instance of NextStudentNumber if one is not constructed and does not exist in the database
        /// </summary>
        /// <returns>Gets an instance of the NextStudentNumber</returns>
        public static NextStudentNumber getInstance()
        {
            if(nextStudentNumber == null)
            {
                nextStudentNumber = db.NextStudentNumbers.SingleOrDefault();

                if(nextStudentNumber == null)
                {
                    nextStudentNumber = new NextStudentNumber();
                    db.NextStudentNumbers.Add(nextStudentNumber);
                    db.SaveChanges();
                }
            }
            return nextStudentNumber;
        }
    }

    /// <summary>
    /// Provides implementation of the program model from the class diagram
    /// </summary>
    public class Program
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProgramId { get; set; }

        [Required]
        [Display(Name ="Program")]
        public string ProgramAcronym { get; set; }

        [Required]
        [Display(Name ="Program\nName")]
        public string Description { get; set; }

        //Navigation properties
        public virtual ICollection<Student> student { get; set; }

        public virtual ICollection<Course> course { get; set; }
    }

    /// <summary>
    /// Provides implementation of the registration model from the class diagram
    /// </summary>
    public class Registration
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        [Display(Name ="Registration\nNumber")]
        public long RegistrationNumber { get; set; }

        [Required]
        [ForeignKey("student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name ="Registration\nDate")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }

        [DisplayFormat(NullDisplayText ="Ungraded")]
        [Range(0, 100)]
        public double? Grade { get; set; }

        public string Notes { get; set; }

        public Registration()
        {

        }

        /// <summary>
        /// Sets the Registration number to the next available number
        /// </summary>
        public void setNextRegistrationNumber()
        {
            this.RegistrationNumber = (long)StoredProcedures.nextNumber("NextRegistrationNumbers");
        }

        //Navigation properties
        public virtual Student student { get; set; }

        public virtual Course course { get; set; }
    }

    /// <summary>
    /// Provides implementation for the NextRegistrationNumber class from the class model diagram -- Milestone 4
    /// </summary>
    public class NextRegistrationNumber
    {
        protected static BITCollege_BCContext db = new BITCollege_BCContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextRegistrationNumberId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextRegistrationNumber nextRegistrationNumber;

        private NextRegistrationNumber()
        {
            NextAvailableNumber = 700;
        }

        /// <summary>
        /// Returns an instance of NextRegistrationNumber if one is not constructed and does not exist in the database
        /// </summary>
        /// <returns>Gets and instance of the NextRegistrationNumber</returns>
        public static NextRegistrationNumber getInstance()
        {
            if(nextRegistrationNumber == null)
            {
                nextRegistrationNumber = db.NextRegistrationNumbers.SingleOrDefault();

                if(nextRegistrationNumber == null)
                {
                    nextRegistrationNumber = new NextRegistrationNumber();
                    db.NextRegistrationNumbers.Add(nextRegistrationNumber);
                    db.SaveChanges();
                }
            }
            return nextRegistrationNumber;
        }
    }

    /// <summary>
    /// Implementation of the course model from the class diagram
    /// </summary>
    public abstract class Course
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [ForeignKey("program")]
        public int? ProgramId { get; set; }

        [Display(Name ="Course\nNumber")]
        public string CourseNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Credit\nHours")]
        public double CreditHours { get; set; }

        [Required]
        [Display(Name ="Tuition\nAmount")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public double TuitionAmount { get; set; }

        [Display(Name ="Course\nType")]
        public string CourseType
        {
            get
            {
                return Utility.MyUtilities.Trim(GetType().Name, "Course");
            }
        }

        public string Notes { get; set; }

        public abstract void setNextCourseNumber();

        //Navigation properties
        public virtual Program program { get; set; }

        public virtual ICollection<Registration> registration { get; set; }
    }

    /// <summary>
    /// Implementation of the gradedcourse model from the class diagram
    /// </summary>
    public class GradedCourse : Course
    {
        [Required]
        [Display(Name ="Assignment\nWeight")]
        [DisplayFormat(DataFormatString = "{0:p}")]
        public double AssignmentWeight { get; set; }

        [Required]
        [Display(Name ="Midterm\nWeight")]
        [DisplayFormat(DataFormatString = "{0:p}")]
        public double MidtermWeight { get; set; }

        [Required]
        [Display(Name ="Final\nWeight")]
        [DisplayFormat(DataFormatString = "{0:p}")]
        public double FinalWeight { get; set; }

        /// <summary>
        /// Sets the next graded course number by calling the stored procedure method
        /// </summary>
        public override void setNextCourseNumber()
        {
            this.CourseNumber = "G-" + StoredProcedures.nextNumber("NextGradedCourses");
        }
    }

    /// <summary>
    /// Provides implementation for the NextGradedCourse model from the class diagram -- Milestone 4
    /// </summary>
    public class NextGradedCourse
    {
        protected static BITCollege_BCContext db = new BITCollege_BCContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextGradedCourseId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextGradedCourse nextGradedCourse;

        private NextGradedCourse()
        {
            NextAvailableNumber = 200000;
        }

        /// <summary>
        /// Returns an instance of NextGradedCourse if one is not constructed and does not exist in the database
        /// </summary>
        /// <returns>Gets an instance of the NextGradedCourse</returns>
        public static NextGradedCourse getInstance()
        {
            if(nextGradedCourse == null)
            {
                nextGradedCourse = db.NextGradedCourses.SingleOrDefault();

                if(nextGradedCourse == null)
                {
                    nextGradedCourse = new NextGradedCourse();
                    db.NextGradedCourses.Add(nextGradedCourse);
                    db.SaveChanges();
                }
            }
            return nextGradedCourse;
        }
    }

    /// <summary>
    /// Implementation of the auditcourse model from the class diagram
    /// </summary>
    public class AuditCourse : Course
    {
        /// <summary>
        /// Sets the next audit course number by calling the stored procedure method
        /// </summary>
        public override void setNextCourseNumber()
        {
            this.CourseNumber = "A-" + StoredProcedures.nextNumber("NextAuditCourses");
        }
    }

    /// <summary>
    /// Provides implementation for the NextAuditCourse model from the class diagram -- Milestone 4
    /// </summary>
    public class NextAuditCourse
    {
        protected static BITCollege_BCContext db = new BITCollege_BCContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextAuditCourseId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextAuditCourse nextAuditCourse;

        private NextAuditCourse()
        {
            NextAvailableNumber = 2000;
        }

        /// <summary>
        /// Returns an instance of NextAuditCourse if one is not constructed and does not exist in the database
        /// </summary>
        /// <returns>Gets and instance of the NextAuditCourse</returns>
        public static NextAuditCourse getInstance()
        {
            if(nextAuditCourse == null)
            {
                nextAuditCourse = db.NextAuditCourses.SingleOrDefault();

                if(nextAuditCourse == null)
                {
                    nextAuditCourse = new NextAuditCourse();
                    db.NextAuditCourses.Add(nextAuditCourse);
                    db.SaveChanges();
                }
            }
            return nextAuditCourse;
        }
    }

    /// <summary>
    /// Implementation of the masterycourse model from the class diagram
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name ="Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }

        /// <summary>
        /// Sets the next mastery course number by calling the stored procedure method
        /// </summary>
        public override void setNextCourseNumber()
        {
            this.CourseNumber = "M-" + StoredProcedures.nextNumber("NextMasteryCourses");
        }
    }

    /// <summary>
    /// Provides implementation for the NextMasteryCourse model from the class diagram -- Milestone 4
    /// </summary>
    public class NextMasteryCourse
    {
        protected static BITCollege_BCContext db = new BITCollege_BCContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NextMasteryCourseId { get; set; }

        public long NextAvailableNumber { get; set; }

        private static NextMasteryCourse nextMasteryCourse;

        private NextMasteryCourse()
        {
            NextAvailableNumber = 20000;
        }

        /// <summary>
        /// Returns an instance of NextMasteryCourse if one is not constructed and does not exist in the database
        /// </summary>
        /// <returns>Gets and instance of the NextMasteryCourse</returns>
        public static NextMasteryCourse getInstance()
        {
            if(nextMasteryCourse == null)
            {
                nextMasteryCourse = db.NextMasteryCourses.SingleOrDefault();

                if(nextMasteryCourse == null)
                {
                    nextMasteryCourse = new NextMasteryCourse();
                    db.NextMasteryCourses.Add(nextMasteryCourse);
                    db.SaveChanges();
                }
            }
            return nextMasteryCourse;
        }
    }

    /// <summary>
    /// Implementation of the GPAState model from the class diagram
    /// </summary>
    public abstract class GPAState
    {
        protected static BITCollege_BCContext db = new BITCollege_BCContext();

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GPAStateId { get; set; }

        [Required]
        [Display(Name ="Lower\nLimit")]
        [DisplayFormat(DataFormatString = "{0:n}")]
        public double LowerLimit { get; set; }

        [Required]
        [Display(Name ="Upper\nLimit")]
        [DisplayFormat(DataFormatString = "{0:n}")]
        public double UpperLimit { get; set; }

        [Required]
        [Display(Name ="Tuition\nRate\nFactor")]
        [DisplayFormat(DataFormatString = "{0:n}")]
        public double TuitionRateFactor { get; set; }

        [Display(Name ="GPA\nState")]
        public string Description
        {
            get
            {
                return Utility.MyUtilities.Trim(GetType().Name, "State");
            }
        }

        //Implementation in a later milestone
        public virtual double tuitionRateAdjustment(Student student)
        {
            return 0;
        }

        public virtual void stateChangeCheck(Student student)
        {
            
        }

        //Navigation property
        public virtual ICollection<Student> student { get; set; }
    }

    /// <summary>
    /// Implementation of the probationstate model from the class diagram
    /// Has methods for checking the state of a students GPAState and facilitates moving between--
    /// states using the singleton design pattern.
    /// </summary>
    public class ProbationState : GPAState
    {
        private static ProbationState probationState;

        private const double LOWER_LIMIT = 1;
        private const double UPPER_LIMIT = 2;
        private const double TUITION_RATE_FACTOR = 1.075;

        /// <summary>
        /// Sets the default state for ProbationState
        /// </summary>
        private ProbationState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        /// <summary>
        /// Checks the database for an instance of probation state, creates an instance should none exist.
        /// </summary>
        /// <returns></returns>
        public static ProbationState getInstance()
        {
            if(probationState == null)
            {
                probationState = db.ProbationStates.SingleOrDefault();

                if(probationState == null)
                {
                    probationState = new ProbationState();
                    db.GPAStates.Add(probationState);
                    db.SaveChanges();
                }
            }
            return probationState;
        }

        /// <summary>
        /// Adjusts the tuition rate of a student based on completed courses.
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Returns the adjusted tuition rate for a student</returns>
        public override double tuitionRateAdjustment(Student student)
        {
            IQueryable<Registration> completeCourses =
                from x in db.Registrations
                where x.Grade != null
                where x.StudentId == x.student.StudentId
                select x;

            double tuitionRate = this.TuitionRateFactor;

            if (completeCourses.Count() >= 5)
            {
                tuitionRate -= 0.04;
            }

            return tuitionRate;
        }

        /// <summary>
        /// Checks the GPAState of a student and sets it up or down a level.
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if(student.GradePointAverage > UPPER_LIMIT)
            {
                student.GPAStateId = RegularState.getInstance().GPAStateId;
            }
            else if(student.GradePointAverage < LOWER_LIMIT)
            {
                student.GPAStateId = SuspendedState.getInstance().GPAStateId;
            }
        }
    }

    /// <summary>
    /// Implementation of the honoursstate model from the class diagram
    /// </summary>
    public class HonoursState : GPAState
    {
        private static HonoursState honoursState;

        private const double LOWER_LIMIT = 3.7;
        private const double UPPER_LIMIT = 4.5;
        private const double TUITION_RATE_FACTOR = .9;

        /// <summary>
        /// Sets the default state for HonoursState
        /// </summary>
        private HonoursState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        /// <summary>
        /// Checks the database for an instance of honours state, creates an instance should none exist.
        /// </summary>
        /// <returns>Gets an instance of honours state</returns>
        public static HonoursState getInstance()
        {
            if(honoursState == null)
            {
                honoursState = db.HonoursStates.SingleOrDefault();

                if(honoursState == null)
                {
                    honoursState = new HonoursState();
                    db.GPAStates.Add(honoursState);
                    db.SaveChanges();
                }
            }
            return honoursState;
        }

        /// <summary>
        /// Adjusts the tuition rate of a student based on completed courses and grade point average.
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Returns the adjusted tuition rate for a student</returns>
        public override double tuitionRateAdjustment(Student student)
        {
            IQueryable<Registration> completeCourses =
                from x in db.Registrations
                where x.Grade != null
                where x.StudentId == x.student.StudentId
                select x;

            double tuitionRate = this.TuitionRateFactor;

            if (completeCourses.Count() >= 5)
            {
                tuitionRate -= 0.05;
            }
            if (student.GradePointAverage > 4.25)
            {
                tuitionRate -= 0.02;
            }

            return tuitionRate;
        }

        /// <summary>
        /// Checks the GPAState of a student and sets it up or down a level.
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if(student.GradePointAverage < LOWER_LIMIT)
            {
                student.GPAStateId = RegularState.getInstance().GPAStateId;
            }
        }
    }

    /// <summary>
    /// Implementation of the suspendedstate model from the class diagram
    /// </summary>
    public class SuspendedState : GPAState
    {
        private static SuspendedState suspendedState;

        private const double LOWER_LIMIT = 0;
        private const double UPPER_LIMIT = 1;
        private const double TUITION_RATE_FACTOR = 1.1;

        /// <summary>
        /// Sets the default values for SuspendedState
        /// </summary>
        private SuspendedState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        /// <summary>
        /// Checks the database for an instance of suspended state, creates an instance should none exist.
        /// </summary>
        /// <returns>Gets an instance of suspended state</returns>
        public static SuspendedState getInstance()
        {
            if(suspendedState == null)
            {
                suspendedState = db.SuspendedStates.SingleOrDefault();

                if(suspendedState == null)
                {
                    suspendedState = new SuspendedState();
                    db.GPAStates.Add(suspendedState);
                    db.SaveChanges();
                }
            }
            return suspendedState;
        }

        /// <summary>
        /// Adjusts the tuition rate of the student based on their grade point average
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Returns the adjusted tuition rate for the student</returns>
        public override double tuitionRateAdjustment(Student student)
        {
            double tuitionRate = this.TuitionRateFactor;

            if(student.GradePointAverage < 0.75)
            {
                tuitionRate += 0.02;
            }
            if(student.GradePointAverage < 0.50)
            {
                tuitionRate += 0.03;
            }

            return tuitionRate;
        }

        /// <summary>
        /// Checks the GPAState of a student and sets it up or down a level.
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if(student.GradePointAverage > UPPER_LIMIT)
            {
                student.GPAStateId = ProbationState.getInstance().GPAStateId;
            }
        }
    }

    /// <summary>
    /// Implementation of the regularstate model from the class diagram
    /// </summary>
    public class RegularState : GPAState
    {
        private static RegularState regularState;

        private const double LOWER_LIMIT = 2;
        private const double UPPER_LIMIT = 3.7;
        private const double TUITION_RATE_FACTOR = 1;

        /// <summary>
        /// Sets the default values for regular state
        /// </summary>
        private RegularState()
        {
            this.LowerLimit = LOWER_LIMIT;
            this.UpperLimit = UPPER_LIMIT;
            this.TuitionRateFactor = TUITION_RATE_FACTOR;
        }

        /// <summary>
        /// Returns an instance of regular state
        /// </summary>
        /// <returns>Gets an instance of regular state</returns>
        public static RegularState getInstance()
        {
            if(regularState == null)
            {
                regularState = db.RegularStates.SingleOrDefault();

                if(regularState == null)
                {
                    regularState = new RegularState();
                    db.GPAStates.Add(regularState);
                    db.SaveChanges();
                }
            }
            return regularState;
        }

        /// <summary>
        /// Adjusts the tuition rate for the student in the regular state
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Returns the tuition rate of the student</returns>
        public override double tuitionRateAdjustment(Student student)
        {
            double tuitionRate = TuitionRateFactor;

            return tuitionRate;
        }

        /// <summary>
        /// Checks the GPAState of a student and sets it up or down a level.
        /// </summary>
        /// <param name="student"></param>
        public override void stateChangeCheck(Student student)
        {
            if(student.GradePointAverage > UPPER_LIMIT)
            {
                student.GPAStateId = HonoursState.getInstance().GPAStateId;
            }
            else if(student.GradePointAverage < LOWER_LIMIT)
            {
                student.GPAStateId = ProbationState.getInstance().GPAStateId;
            }
        }
    }

    /// <summary>
    /// Provides implementation for the StoredProcedures class from the model diagram
    /// </summary>
    public static class StoredProcedures
    {
        /// <summary>
        /// Returns the next number available by establishing a connection to the db and executing a procedure.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>Gets the next number available</returns>
        public static long? nextNumber(string tableName)
        {
            //Constructs a new SQL connection and provides a connection string
            SqlConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=BITCollege_BCContext;Integrated Security=True");

            //Initializes the returnValue
            long? returnValue = 0;

            //Initializes storedProcedure with the connection string
            SqlCommand storedProcedure = new SqlCommand("next_number", connection);

            //Declares what type of SQL command storedProcedure is
            storedProcedure.CommandType = CommandType.StoredProcedure;

            //Adds a table name parameter to the stored procedure variable
            storedProcedure.Parameters.AddWithValue("@TableName", tableName);

            //Provides the output for the stored procedure
            SqlParameter outputParameter = new SqlParameter("@NewVal", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            //Adds an output parameter to the storeProcedure variable/command
            storedProcedure.Parameters.Add(outputParameter);

            //Opens the db connection
            connection.Open();

            //Executes the stored procedure
            storedProcedure.ExecuteNonQuery();

            //Closes the db connection
            connection.Close();

            //Assigns the returnValue the new number from the procedure
            returnValue = (long?)outputParameter.Value;

            //Returns the returnValue
            return returnValue;
        }
    }
}