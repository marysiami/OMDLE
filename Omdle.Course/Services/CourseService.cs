using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Omdle.Course.Contracts;
using Omdle.Course.Models;
using Omdle.Data;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;
using System.Linq;
using System.Threading.Tasks;
using Omdle.Data.Contracts;

namespace Omdle.Course.Services
{
    /// <summary>Class CourseService.
    /// Implements the <see cref="T:Omdle.Course.Contracts.ICourseService"/></summary>
    public class CourseService : ICourseService
    {
        /// <summary>The data service</summary>
        private readonly IDataService _dataService;

        /// <summary>The user manager</summary>
        private readonly UserManager<OmdleUser> _userManager;

        /// <summary>Initializes a new instance of the <see cref="T:Omdle.Course.Services.CourseService"/> class.</summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="userManager">The user manager.</param>
        public CourseService(IDataService dataService, UserManager<OmdleUser> userManager)
        {
            _dataService = dataService;
            _userManager = userManager;
        }

        /// <summary>create course as an asynchronous operation.</summary>
        /// <param name="title">The title.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>Task&lt;Data.Models.Course&gt;.</returns>
        public async Task<Data.Models.Course> CreateCourseAsync(string title, OmdleUser owner)
        {
            var model = new Data.Models.Course
            {
                Title = title,
                OwnerUser = owner,
                OwnerId = owner.Id
            };
            await _dataService.GetSet<Data.Models.Course>().AddAsync(model);
            await _dataService.SaveDbAsync();

            return model;
        }

        /// <summary>Deletes the course.</summary>
        /// <param name="course">The course.</param>
        /// <returns>Task.</returns>
        public async Task DeleteCourse(Data.Models.Course course)
        {

            _dataService.GetSet<Data.Models.Course>().Remove(course);
            await _dataService.SaveDbAsync();
        }

        /// <summary>Gets the course by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Data.Models.Course&gt;.</returns>
        public async Task<Data.Models.Course> GetCourseById(string id)
        {
            var model = await _dataService.GetSet<Data.Models.Course>()
                 .Include(x => x.Lessons)
                .Include(x => x.OwnerUser)
               .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            return model;
        }

        /// <summary>Gets the courses.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;CourseListing&gt;.</returns>
        public async Task<CourseListing> GetCourses(int skip = 0, int take = 10)
        {
            var result = new CourseListing();

            var query = _dataService.GetSet<Data.Models.Course>();

            result.TotalCount = query.Count();
            result.Courses = await query
                .Include(x => x.Lessons)
                .Include(x => x.OwnerUser)
                .Skip(skip * take)
                .Take(take)
                .ToListAsync();

            return result;
        }

        /// <summary>Gets the courses from teacher.</summary>
        /// <param name="teacher">The teacher.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;CourseListing&gt;.</returns>
        public async Task<CourseListing> GetCoursesFromTeacher(OmdleUser teacher, int skip = 0, int take = 10)
        {
            var result = new CourseListing();

            var query = _dataService.GetSet<Data.Models.Course>();

            result.TotalCount = query.Count();
            result.Courses = await query
                .Where(x => x.OwnerUser == teacher)
                .Include(x => x.Lessons)
                .Include(x => x.OwnerUser)
                .Skip(skip * take)
                .Take(take)
                .ToListAsync();

            return result;

        }

        /// <summary>Gets the courses from student.</summary>
        /// <param name="student">The student.</param>
        /// <returns>CourseListing.</returns>
        public CourseListing GetCoursesFromStudent(OmdleUser student)
        {
            var result = new CourseListing();
            result.Courses =_dataService.GetSet<Data.Models.StudentCourse>()
                    .Where(x => x.Student == student)
                    .Select(x => x.Course) 
                    .Include(x=>x.OwnerUser)
                    .Include(x=>x.Lessons)
                                      
                    .ToList();
          
            result.TotalCount = result.Courses.Count();

          
            return result;
        }


        /// <summary>Updates the course.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="title">The title.</param>
        /// <returns>Task.</returns>
        public async Task UpdateCourse(string id, string title)
        {
            var model = await GetCourseById(id);
            model.Title = title;

            _dataService.GetSet<Data.Models.Course>().Update(model);
            await _dataService.SaveDbAsync();
        }

        /// <summary>Determines whether [is student in course] [the specified student].</summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        /// <returns>
        ///   <c>true</c> if [is student in course] [the specified student]; otherwise, <c>false</c>.</returns>
        public bool IsStudentInCourse(OmdleUser student, Data.Models.Course course)
        {
            var model = _dataService.GetSet<StudentCourse>().Where(x => x.Student == student && x.Course == course);
            if (model != null) return true;
            else return false;
        }

        /// <summary>Signs the in course.</summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        /// <returns>Task.</returns>
        public async Task SignInCourse(OmdleUser student, Data.Models.Course course)
        {

            var model = new StudentCourse
            {
                Student = student,
                StudentId = student.Id,
                Course = course,
                CourseId = course.Id
            };

            await _dataService.GetSet<StudentCourse>().AddAsync(model);
            await _dataService.SaveDbAsync();
        }

        /// <summary>check out of course as an asynchronous operation.</summary>
        /// <param name="student">The student.</param>
        /// <param name="course">The course.</param>
        /// <returns>Task.</returns>
        public async Task CheckOutOfCourseAsync(OmdleUser student, Data.Models.Course course)
        {
            StudentCourse studentCourse = _dataService.GetSet<Data.Models.StudentCourse>().FirstOrDefault(x => x.Course == course && x.Student == student);
            _dataService.GetSet<Data.Models.StudentCourse>().Remove(studentCourse);
            await _dataService.SaveDbAsync();
        }
    }
}