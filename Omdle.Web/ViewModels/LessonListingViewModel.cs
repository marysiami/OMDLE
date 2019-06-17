using Omdle.Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels
{
    public class LessonListingViewModel
    {
        public LessonListingViewModel(LessonListing model)
        {
            TotalCount = model.TotalCount;
            Lessons = model.Lessons.Select(x => new LessonViewModel(x)).ToList();
            CourseName = model.CourseName;
        }

        public int TotalCount { get; set; }
        public List<LessonViewModel> Lessons { get; set; }
        public string CourseName { get; set; }
    }
}
