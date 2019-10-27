using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollegeRegistration" in both code and config file together.
    [ServiceContract]
    public interface ICollegeRegistration
    {
        [OperationContract]
        void DoWork();

        /// <summary>
        /// Drops a course based on registration ID passed as paramater
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns>Returns true if course was dropped successfully, false if exception occurs</returns>
        [OperationContract]
        bool dropCourse(int registrationId);

        /// <summary>
        /// Provides functionlity to register a student for a course. Returns codes for success or failures.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseId"></param>
        /// <param name="notes"></param>
        /// <returns>Returns 0 for success, or -100 if grade is null, -200 for the maximum attempts equals the limit for that course, -300 for an exception</returns>
        [OperationContract]
        int registerCourse(int studentId, int courseId, string notes);

        /// <summary>
        /// Updates the grade based on passed parameters
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="registrationId"></param>
        /// <param name="notes"></param>
        [OperationContract]
        void updateGrade(double grade, int registrationId, string notes);
    }
}
