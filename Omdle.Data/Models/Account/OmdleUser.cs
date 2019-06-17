using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Data.Models.Account
{
    public class OmdleUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }
        public string GetDisplayName()
        {
            var userName = UserName;

            if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            {
                userName = $"{FirstName} {LastName}";
            }

            return userName;
        }    
    }
}
