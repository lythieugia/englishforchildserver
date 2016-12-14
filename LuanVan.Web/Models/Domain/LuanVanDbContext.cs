using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class LuanVanDbContext : DbContext
    {
        public LuanVanDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }

        public DbSet<ClassLesson> ClassLessons { get; set; }
        public DbSet<StudentLesson> StudentLessons { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentVocabulary> StudentVocabularies { get; set; }
        
    }
}