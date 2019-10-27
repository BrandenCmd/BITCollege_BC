using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BITCollege_BC.Models;
using Utility;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CollegeRegistration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CollegeRegistration.svc or CollegeRegistration.svc.cs at the Solution Explorer and start debugging.
    public class CollegeRegistration : ICollegeRegistration
    {
        BITCollege_BC.Models.BITCollege_BCContext db = new BITCollege_BC.Models.BITCollege_BCContext();

        public void DoWork()
        {
        }

        /// <summary>
        /// Drops a course based on registration ID passed as paramater
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns>Returns true if course was dropped successfully, false if exception occurs</returns>
        public bool dropCourse(int registrationId)
        {
            bool returnValue = true;

            try
            {
                Registration registrationQuery =
                   (from x in db.Registrations
                    where x.RegistrationId == registrationId
                    select x).SingleOrDefault();

                db.Registrations.Remove(registrationQuery);

                db.SaveChanges();
            }
            catch (Exception)
            {
                returnValue = false;
            }

            return returnValue;
        }

        /// <summary>
        /// Provides functionlity to register a student for a course. Returns codes for success or failures.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="notes"></param>
        /// <returns>Returns 0 for success, or -100, -200, -300 for failures</returns>
        public int registerCourse(int studentId, int courseId, string notes)
        {
            int returnValue = 0;

            try
            {
                IQueryable<Registration> registrationQuery =
                    (from x in db.Registrations
                     where x.StudentId == studentId
                     where x.CourseId == courseId
                     select x);

                Student student =
                   (from x in db.Students
                    where x.StudentId == studentId
                    select x).SingleOrDefault();

                Course courses =
                   (from x in db.Courses
                    where x.CourseId == courseId
                    select x).SingleOrDefault();

                foreach (Registration record in registrationQuery)
                {
                    if (record.Grade == null)
                    {
                        returnValue = -100;
                    }
                }
                if (courses.CourseType == "Mastery")
                {
                    MasteryCourse masteryCourse = (MasteryCourse)courses;

                    if (masteryCourse.MaximumAttempts <= registrationQuery.Count())
                    {
                        returnValue = -200;
                    }
                }
                if(returnValue == 0)
                {
                    Registration registration = new Registration();

                    registration.StudentId = studentId;
                    registration.CourseId = courseId;
                    registration.Notes = notes;
                    registration.RegistrationDate = DateTime.Today;
                    registration.setNextRegistrationNumber();

                    db.Registrations.Add(registration);

                    db.SaveChanges();

                    student.OutstandingFees += (student.gPAState.tuitionRateAdjustment(student) * courses.TuitionAmount);

                    db.SaveChanges();
                }                 
            }
            catch (Exception)
            {
                returnValue = -300;
            }

            return returnValue;
        }

        /// <summary>
        /// Updates the grade based on passed parameters
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="registrationId"></param>
        /// <param name="notes"></param>
        public void updateGrade(double grade, int registrationId, string notes)
        {
            Registration registrationRecordQuery =
                (from x in db.Registrations
                 where x.RegistrationId == registrationId
                 select x).SingleOrDefault();

            registrationRecordQuery.Grade = grade;
            registrationRecordQuery.Notes = notes;

            db.SaveChanges();

            calculateGPA(registrationRecordQuery.StudentId);

            db.SaveChanges();
        }

        /// <summary>
        /// Takes in a student ID and calculates their GPA based on completed courses and the grades in those courses.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns>Returns the modified grade point average of the student</returns>
        private double calculateGPA(int studentId)
        {
            IQueryable<Registration> registrationRecords =
                (from x in db.Registrations
                 where x.StudentId == studentId
                 where x.Grade != null
                 select x);

            double totalGradePointValue = 0;
            double totalCreditHours = 0;
            double gradePointAverage = 0;

            foreach (Registration record in registrationRecords)
            {
                double grade = (double)record.Grade;

                Course course =
                    (from x in db.Courses
                     where x.CourseId == record.CourseId
                     select x).SingleOrDefault();

                CourseType courseType = Utility.BusinessRules.courseTypeLookup(course.CourseType);

                double GPAValue = Utility.BusinessRules.gradeLookup(grade, courseType);

                double creditHours = course.CreditHours;

                if(!courseType.Equals(Utility.CourseType.AUDIT))
                {
                    gradePointAverage = GPAValue * creditHours;

                    totalGradePointValue += gradePointAverage;

                    totalCreditHours += course.CreditHours;
                }
            }

            Student student =
               (from x in db.Students
                where x.GradePointAverage == gradePointAverage
                select x).SingleOrDefault();

            gradePointAverage = totalGradePointValue / totalCreditHours;

            student.GradePointAverage = gradePointAverage;

            db.SaveChanges();

            student.changeState();

            db.SaveChanges();

            return gradePointAverage;
        }
    }
}
