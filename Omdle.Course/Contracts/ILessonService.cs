using Omdle.Course.Models;
using Omdle.Data.Models;
using System;
using System.Threading.Tasks;

namespace Omdle.Course.Contracts
{
    /// <summary>Interface ILessonService</summary>
    public interface ILessonService
    {
        /// <summary>Creates the lesson.</summary>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        /// <param name="course">The course.</param>
        /// <param name="date">The date.</param>
        /// <returns>Task&lt;Lesson&gt;.</returns>
        Task<Lesson> CreateLesson(string title, string content, Data.Models.Course course, string date);
        /// <summary>Gets the lesson by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Lesson&gt;.</returns>
        Task<Lesson> GetLessonById(string id);
        /// <summary>Gets the lessons from course.</summary>
        /// <param name="course">The course.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>LessonListing.</returns>
        LessonListing GetLessonsFromCourse(Data.Models.Course course, int skip = 0, int take = 10);
        /// <summary>Deletes the lesson.</summary>
        /// <param name="lesson">The lesson.</param>
        /// <returns>Task.</returns>
        Task DeleteLesson(Lesson lesson);
        /// <summary>Updates the lesson.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        /// <param name="courseId">The course identifier.</param>
        /// <returns>Task.</returns>
        Task UpdateLesson(string id, string title, string content,Guid courseId);

    }
}
