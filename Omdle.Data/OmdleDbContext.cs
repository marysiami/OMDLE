using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;
using System;

namespace Omdle.Data
{
    public class OmdleDbContext : IdentityDbContext<OmdleUser, OmdleRole, Guid>
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ChatMessage> ChateMessages { get; set; }

        public OmdleDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StudentCourse>().HasKey(bc => new { bc.StudentId, bc.CourseId });
            builder.Entity<StudentCourse>()
               .HasOne(bc => bc.Student)
               .WithMany(b => b.StudentCourses)
               .HasForeignKey(bc => bc.StudentId);
            builder.Entity<StudentCourse>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(bc => bc.CourseId);

            builder.Entity<OmdleRole>().HasData(new OmdleRole
            {
                Id = new Guid("1a24743d-6a15-431e-9af0-d846bfebf1e8"),
                Name = "Teacher",
                NormalizedName = "TEACHER",
                ConcurrencyStamp = "1a24743d-6a15-431e-9af0-d846bfebf1e8"
            });
            builder.Entity<OmdleRole>().HasData(new OmdleRole
            {
                Id = new Guid("15235295-f0b4-41e5-aa31-75d00ffb4e77"),
                Name = "Student",
                NormalizedName = "STUDENT",
                ConcurrencyStamp = "15235295-f0b4-41e5-aa31-75d00ffb4e77"
            });
            builder.Entity<OmdleRole>().HasData(new OmdleRole
            {
                Id = new Guid("5d8c4438-09a8-4b1b-99a6-61e66f813606"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "5d8c4438-09a8-4b1b-99a6-61e66f813606"
            });

            //  ConfigureCourseModel(builder);
            base.OnModelCreating(builder);
        }

    }
   
   /* public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<OmdleDbContext>
    {
        OmdleDbContext IDesignTimeDbContextFactory<OmdleDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OmdleDbContext>();
            optionsBuilder.UseSqlServer<OmdleDbContext>("Server = (localdb)\\mssqllocaldb; Database = Omdle; Trusted_Connection = True; MultipleActiveResultSets = true");

            return new OmdleDbContext(optionsBuilder.Options);
        }
    }*/
}
