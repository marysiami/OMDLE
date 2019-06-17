using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Omdle.Course.Contracts;
using Omdle.Course.Models;
using Omdle.Data.Contracts;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Course.Services
{
    /// <summary>Class LessonService.
    /// Implements the <see cref="T:Omdle.Course.Contracts.ILessonService"/></summary>
    public class LessonService : ILessonService
    {
        /// <summary>The data service</summary>
        private readonly IDataService _dataService;

        /// <summary>The user manager</summary>
        private readonly UserManager<OmdleUser> _userManager;

        /// <summary>Initializes a new instance of the <see cref="T:Omdle.Course.Services.LessonService"/> class.</summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="userManager">The user manager.</param>
        public LessonService(IDataService dataService, UserManager<OmdleUser> userManager)
        {
            _dataService = dataService;          
            _userManager = userManager;
        }

        /// <summary>Creates the lesson.</summary>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        /// <param name="course">The course.</param>
        /// <param name="date">The date.</param>
        /// <returns>Task&lt;Lesson&gt;.</returns>
        public async Task<Lesson> CreateLesson(string title, string content, Data.Models.Course course, string date)
        {
            var model = new Lesson
            {
                Title = title,
                Content = content,
                Course = course//dodac date


            };
            await _dataService.GetSet<Lesson>().AddAsync(model);
            await _dataService.SaveDbAsync();

            return model;
        }

        /// <summary>Deletes the lesson.</summary>
        /// <param name="lesson">The lesson.</param>
        /// <returns>Task.</returns>
        public async Task DeleteLesson(Lesson lesson)
        {
            _dataService.GetSet<Lesson>().Remove(lesson);
            await _dataService.SaveDbAsync();
        }

        /// <summary>Gets the lesson by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Lesson&gt;.</returns>
        public async Task<Lesson> GetLessonById(string id)
        {
            var model = await _dataService.GetSet<Lesson>()
                .Include(x=>x.Course)
               .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            return model;
        }

        /// <summary>Gets the lessons from course.</summary>
        /// <param name="course">The course.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>LessonListing.</returns>
        public LessonListing GetLessonsFromCourse(Data.Models.Course course, int skip = 0, int take = 10)
        {
            var result = new LessonListing
            {
                CourseName = course.Title,
                TotalCount = course.Lessons.Count(),
                Lessons = course.Lessons
                    .OrderBy(x => x.Date)
                    .Skip(skip * take)
                    .Take(take)
                    .ToList()
            };

            return result;
        }

        /// <summary>Updates the lesson.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="content">The content.</param>
        /// <param name="courseId">The course identifier.</param>
        /// <returns>Task.</returns>
        public async Task UpdateLesson(string id, string title, string content, Guid courseId)
        {
            var model = await GetLessonById(id);
            model.Title = title;
            model.Content = content;
            model.CourseId= courseId;

            _dataService.GetSet<Lesson>().Update(model);
            await _dataService.SaveDbAsync();
        }
    }
}
