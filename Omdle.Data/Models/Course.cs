using Omdle.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Data.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
        public Guid OwnerId { get; set; }
        public virtual OmdleUser OwnerUser { get; set; }
      
        public virtual ICollection<Lesson> Lessons { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
