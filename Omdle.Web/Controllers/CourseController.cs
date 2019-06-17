using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omdle.Account.Contracts;
using Omdle.Common.Exceptions;
using Omdle.Course.Contracts;
using Omdle.Web.ViewModels;
using Omdle.Web.ViewModels.Request;
using System.Threading.Tasks;

namespace Omdle.Web.Controllers
{
    public class CourseController : OmdleBaseController
    {
        private readonly ICourseService _courseService;
        private readonly ILessonService _lessonService;
        private readonly IUserService _userService;
        private readonly IReminderService _reminderService;


        public CourseController(ICourseService courseService, ILessonService lessonService, IUserService userService, IReminderService reminderService)
        {
            _courseService = courseService;
            _lessonService = lessonService;
            _userService = userService;
            _reminderService = reminderService;
        }

        [HttpGet] //działa 
        public async Task<JsonResult> GetAllCourses([FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {
            var model = await _courseService.GetCourses(page, threadsPerPage);

            var result = new CourseListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetCoursesFromTeacher([FromQuery] string teacherId, [FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {
            var teacher = await _userService.GetUserByIdAsync(teacherId);
            var model = await _courseService.GetCoursesFromTeacher(teacher, page, threadsPerPage);

            var result = new CourseListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetCoursesFromStudent([FromQuery] string studentId)
        { 
            var student = await _userService.GetUserByIdAsync(studentId);
            var model = _courseService.GetCoursesFromStudent(student);

            var result = new CourseListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetLessonsFromCourse([FromQuery] string courseId, [FromQuery] int page,[FromQuery] int postsPerPage = 10)
        {
            var course = await _courseService.GetCourseById(courseId);
            if (course == null) throw new InvalidCourseIdException();

            var model = _lessonService.GetLessonsFromCourse(course, page, postsPerPage);

            var result = new LessonListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetLesson([FromQuery] string lessonId)
        {
            var lesson = await _lessonService.GetLessonById(lessonId);
            if (lesson == null) throw new InvalidCourseIdException();        

            var result = new LessonViewModel(lesson);

            return Json(result);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost] //działa
        public async Task<JsonResult> CreateCourse([FromBody] CreateCourseRequestViewModel request)
        {
            var owner = await _userService.GetUserByIdAsync(request.OwnerId);
            var course = await _courseService.CreateCourseAsync(request.Title,owner );
            var result = new CourseViewModel(course);

            return Json(result);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPut] //działa
        public async Task UpdateCourse([FromBody] UpdateCourseRequestViewModel request)
        {
            await _courseService.UpdateCourse(request.Id, request.Title);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpDelete] //działa
        public async Task DeleteCourse([FromQuery] string id)
        {
            var model = await _courseService.GetCourseById(id);
            await _courseService.DeleteCourse(model);
        }


        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public async Task<JsonResult> CreateLesson([FromBody] CreateLessonRequestViewModel request)
        {
            var course = await _courseService.GetCourseById(request.CourseId);
            if (course == null) throw new InvalidCourseIdException();

            var model = await _lessonService.CreateLesson(request.Title, request.Content, course, request.Date);
            var result = new LessonViewModel(model);

            return Json(result);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpDelete]
        public async Task DeleteLesson([FromQuery] string id)
        {
            var model = await _lessonService.GetLessonById(id);
            await _lessonService.DeleteLesson(model);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut]
        public async Task UpdateLesson([FromBody] UpdateLessonRequestViewModel request)
        {
            var course = await _courseService.GetCourseById(request.CourseId);
            await _lessonService.UpdateLesson(request.Id, request.Title, request.Content,course.Id);
        }

        [Authorize(Roles = "Student")]
        [HttpPut]//działa
        public async Task SignInCourse([FromBody] SiginCourseRequestViewModel request)
        {
            var course = await _courseService.GetCourseById(request.CourseId);
            var student = await _userService.GetUserByIdAsync(request.StudentId);
            await _courseService.SignInCourse(student, course);

        }


        [Authorize(Roles = "Student")]
        [HttpDelete] 
        public async Task CheckOutOfCourse([FromQuery] string studentId, [FromQuery] string courseId)
        {
            var course = await _courseService.GetCourseById(courseId);
            var student = await _userService.GetUserByIdAsync(studentId);
            await _courseService.CheckOutOfCourseAsync(student,course);
        }


    }
}
