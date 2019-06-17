using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Course.Models
{
    /// <summary>Class CourseListing.</summary>
    public class CourseListing
    {
        /// <summary>Gets or sets the courses.</summary>
        /// <value>The courses.</value>
        public List<Omdle.Data.Models.Course> Courses { get; set; }

        /// <summary>Gets or sets the total count.</summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }
    }
}
