using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels.Request
{
    public class UpdateReminderRequestViewModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string ownerId { get; set; }
    }
}