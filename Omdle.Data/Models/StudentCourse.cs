using Omdle.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Data.Models
{
    public class StudentCourse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public OmdleUser Student { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
