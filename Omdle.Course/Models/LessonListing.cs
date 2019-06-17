using Omdle.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Course.Models
{
    /// <summary>Class LessonListing.</summary>
    public class LessonListing
    {
        /// <summary>Gets or sets the lessons.</summary>
        /// <value>The lessons.</value>
        public List<Lesson> Lessons { get; set; }

        /// <summary>Gets or sets the total count.</summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }

        /// <summary>Gets or sets the name of the course.</summary>
        /// <value>The name of the course.</value>
        public string CourseName { get; set; }
    }
}
