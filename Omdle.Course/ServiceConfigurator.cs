using Microsoft.Extensions.DependencyInjection;
using Omdle.Course.Contracts;
using Omdle.Course.Services;
using System;

namespace Omdle.Course
{
    /// <summary>Class ServiceConfigurator.</summary>
    public static class ServiceConfigurator
    {
        /// <summary>Courses the module.</summary>
        /// <param name="services">The services.</param>
        public static void CourseModule(this IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILessonService, LessonService>();
        }

        /// <summary>Reminders the module.</summary>
        /// <param name="services">The services.</param>
        public static void ReminderModule(this IServiceCollection services)
        {
            services.AddScoped<IReminderService, ReminderService>();
        }
    }
}
