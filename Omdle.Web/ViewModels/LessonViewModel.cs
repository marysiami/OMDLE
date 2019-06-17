using Omdle.Data.Models;
using System;

namespace Omdle.Web.ViewModels
{
    public class LessonViewModel
    {
        public LessonViewModel(Lesson x)
        {
            Id = x.Id.ToString();
            Title = x.Title;
            Content = x.Content;
            Date = x.Date;
            CourseName = x.Course.Title;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string CourseName { get; set; }
    }
}
