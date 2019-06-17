using Omdle.Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels
{
    public class CourseListingViewModel
    {
        public CourseListingViewModel(CourseListing model)
        {
            TotalCount = model.TotalCount;
            Courses = model.Courses.Select(x => new CourseViewModel(x)).ToList();
        }

        public int TotalCount { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
