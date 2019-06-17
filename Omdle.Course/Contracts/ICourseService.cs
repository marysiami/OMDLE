using Omdle.Course.Models;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;
using System.Threading.Tasks;

namespace Omdle.Course.Contracts
{
    /// <summary>Interface ICourseService</summary>
    public interface ICourseService
    {
        /// <summary>Creates the course asynchronous.</summary>
        /// <param name="title">The title.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>Task&lt;Data.Models.Course&gt;.</returns>
        Task<Data.Models.Course> CreateCourseAsync(string title, OmdleUser owner);

        /// <summary>Gets the courses.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;CourseListing&gt;.</returns>
        Task<CourseListing> GetCourses(int skip = 0, int take = 10);

        /// <summary>Gets the courses from teacher.</summary>
        /// <param name="teacher">The teacher.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;CourseListing&gt;.</returns>
        Task<CourseListing> GetCoursesFromTeacher(OmdleUser teacher,int skip = 0, int take = 10);

        /// <summary>Gets the course by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Data.Models.Course&gt;.</returns>
        Task<Data.Models.Course> GetCourseById(string id);

        /// <summary>Gets the courses from student.</summary>
        /// <param name="student">The student.</param>
        /// <returns>CourseListing.</returns>
        CourseListing GetCoursesFromStudent(OmdleUser student);

        /// <summary>Deletes the course.</summary>
        /// <param name="course">The course.</param>
        /// <returns>Task.</returns>
        Task DeleteCourse(Data.Models.Course course);

        /// <summary>Updates the course.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <returns>Task.</returns>
        Task UpdateCourse(string id, string title);

        /// <summary>Signs the in course.</summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        /// <returns>Task.</returns>
        Task SignInCourse(OmdleUser student, Data.Models.Course course);

        /// <summary>Determines whether [is student in course] [the specified student].</summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        /// <returns>
        ///   <c>true</c> if [is student in course] [the specified student]; otherwise, <c>false</c>.</returns>
        bool IsStudentInCourse(OmdleUser student, Data.Models.Course course);

        /// <summary>Checks the out of course asynchronous.</summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        /// <returns>Task.</returns>
        Task CheckOutOfCourseAsync(OmdleUser student, Data.Models.Course course);



    }
}
