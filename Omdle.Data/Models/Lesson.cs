using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Data.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        
    }
}
