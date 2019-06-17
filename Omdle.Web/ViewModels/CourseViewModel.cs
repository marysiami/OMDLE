using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel(Data.Models.Course x)
        {
            if (x.Id != null) { Id = x.Id.ToString(); }
           
            Title = x.Title;
            if (x.OwnerUser != null)
            {
                OwnerName = x.OwnerUser.FirstName;
                OwnerSurname = x.OwnerUser.LastName;
            }
            
            LessonsCount = 0;  
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public int? LessonsCount { get; set; }
    }
}
