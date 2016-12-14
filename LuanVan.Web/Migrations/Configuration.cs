namespace LuanVan.Web.Migrations
{
    using LuanVan.Web.Models.Domain;
    using LuanVan.Web.Support;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<LuanVan.Web.Models.Domain.LuanVanDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LuanVan.Web.Models.Domain.LuanVanDbContext context)
        {
            #region Delete Data
            context.Database.ExecuteSqlCommand("DELETE FROM ClassLessons");

            context.Database.ExecuteSqlCommand("DELETE FROM StudentVocabularies");
            context.Database.ExecuteSqlCommand("DELETE FROM StudentGroups");
            context.Database.ExecuteSqlCommand("DELETE FROM StudentLessons");

            context.Database.ExecuteSqlCommand("DELETE FROM Vocabularies");            
            context.Database.ExecuteSqlCommand("DELETE FROM Groups");
            context.Database.ExecuteSqlCommand("DELETE FROM Lessons");

            context.Database.ExecuteSqlCommand("DELETE FROM Students");
            context.Database.ExecuteSqlCommand("DELETE FROM Classes");

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('ClassLessons', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('StudentLessons', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('StudentGroups', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('StudentVocabularies', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Vocabularies', RESEED, 0)");            
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Groups', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Lessons', RESEED, 0)");

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Students', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Classes', RESEED, 0)");
            #endregion Delete Data

            #region UserProfiles - This Seed does not delete table 'UserProfile'
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }

            #region UserProfiles - Teacher
            var teacher1 = context.UserProfiles.Where(u => u.UserName == "teacher1").FirstOrDefault();
            if (teacher1 == null)
            {
                WebSecurity.CreateUserAndAccount("teacher1", "123456");
                teacher1 = context.UserProfiles.Where(u => u.UserName == "teacher1").FirstOrDefault();
            }
            teacher1.RoleName = "Teacher";

            var teacher2 = context.UserProfiles.Where(u => u.UserName == "teacher2").FirstOrDefault();
            if (teacher2 == null)
            {
                WebSecurity.CreateUserAndAccount("teacher2", "123456");
                teacher2 = context.UserProfiles.Where(u => u.UserName == "teacher2").FirstOrDefault();
            }
            teacher2.RoleName = "Teacher";
            #endregion UserProfiles - Teacher

            #region UserProfiles - Parent
            var parent1 = context.UserProfiles.Where(u => u.UserName == "parent1").FirstOrDefault();
            if (parent1 == null)
            {
                WebSecurity.CreateUserAndAccount("parent1", "123456");
                parent1 = context.UserProfiles.Where(u => u.UserName == "parent1").FirstOrDefault();
            }
            parent1.RoleName = "Parent";

            var parent2 = context.UserProfiles.Where(u => u.UserName == "parent2").FirstOrDefault();
            if (parent2 == null)
            {
                WebSecurity.CreateUserAndAccount("parent2", "123456");
                parent2 = context.UserProfiles.Where(u => u.UserName == "parent2").FirstOrDefault();
            }
            parent2.RoleName = "Parent";

            var parent3 = context.UserProfiles.Where(u => u.UserName == "parent3").FirstOrDefault();
            if (parent3 == null)
            {
                WebSecurity.CreateUserAndAccount("parent3", "123456");
                parent3 = context.UserProfiles.Where(u => u.UserName == "parent3").FirstOrDefault();
            }
            parent3.RoleName = "Parent";

            var parent4 = context.UserProfiles.Where(u => u.UserName == "parent4").FirstOrDefault();
            if (parent4 == null)
            {
                WebSecurity.CreateUserAndAccount("parent4", "123456");
                parent4 = context.UserProfiles.Where(u => u.UserName == "parent4").FirstOrDefault();
            }
            parent4.RoleName = "Parent";

            var parent5 = context.UserProfiles.Where(u => u.UserName == "parent5").FirstOrDefault();
            if (parent5 == null)
            {
                WebSecurity.CreateUserAndAccount("parent5", "123456");
                parent5 = context.UserProfiles.Where(u => u.UserName == "parent5").FirstOrDefault();
            }
            parent5.RoleName = "Parent";
            #endregion UserProfiles - Parent

            #endregion UserProfiles

            #region Lessons
            var lesson0 = new Lesson { Name = "Lesson0", ImageId = "5a03f229-ff3c-474b-8c9e-406b8ab208e2", ImageExtension = ".png", UserProfile = teacher1 };
            var lesson1 = new Lesson { Name = "Lesson1", ImageId = "01f4da23-cf76-45a4-a145-54a05cca460f", ImageExtension = ".png", UserProfile = teacher2 };
            #endregion Lessons

            #region Groups
            var group0 = new Group { Name = "Group0", ImageId = "1b6c86cb-5439-4bb7-8726-a9c622b2f6c0", ImageExtension = ".png", Lesson = lesson0 };
            var group1 = new Group { Name = "Group1", ImageId = "27f54b7d-21a4-4689-a477-fd7df88d5a25", ImageExtension = ".png", Lesson = lesson0 };
            var group2 = new Group { Name = "Group2", ImageId = "9c35b4f2-dc86-49c0-b5e9-388427d39564", ImageExtension = ".png", Lesson = lesson1 };
            var group3 = new Group { Name = "Group3", ImageId = "8c5dea60-98dc-4a29-bc3a-26b6fe666fc6", ImageExtension = ".png", Lesson = lesson1 };
            #endregion Groups  
         
            #region Vocabularies
            var voca0 = new Vocabulary { Word = "Number0", ImageId = "e46a1e1429494fc2884028a0a03ad403", ImageExtension = ".png", Group = group0 };
            var voca1 = new Vocabulary { Word = "Number1", ImageId = "6196593e0a004db0a286caca5bbe3ba2", ImageExtension = ".png", Group = group0 };
            var voca2 = new Vocabulary { Word = "Number2", ImageId = "1ca20b1993f4455ebcb10311a07c485b", ImageExtension = ".png", Group = group0 };
            var voca3 = new Vocabulary { Word = "Number3", ImageId = "d38d288f80f04a1ca7ad3f58304447a7", ImageExtension = ".png", Group = group1 };
            var voca4 = new Vocabulary { Word = "Number4", ImageId = "40985821e08645a6ad607860300a33d1", ImageExtension = ".png", Group = group1 };
            var voca5 = new Vocabulary { Word = "Number5", ImageId = "a8f1715b86d64bd09e787d9bbb6306d6", ImageExtension = ".png", Group = group2 };
            var voca6 = new Vocabulary { Word = "Number6", ImageId = "cb4f3bcbcf6746a6bd65107ccac33bea", ImageExtension = ".png", Group = group2 };
            var voca7 = new Vocabulary { Word = "Number7", ImageId = "ad050c44b92749899aebc25b4fff5f12", ImageExtension = ".png", Group = group2 };
            var voca8 = new Vocabulary { Word = "Number8", ImageId = "0e62095a2f184c2b8fd0b2d20e36f585", ImageExtension = ".png", Group = group3 };
            var voca9 = new Vocabulary { Word = "Number9", ImageId = "1a8b07e6c8c2418b96fd168ad0f56311", ImageExtension = ".png", Group = group3 };
            #endregion Vocabularies

            #region Classes
            var classA = new Class { Name = "ClassA" };
            var classB = new Class { Name = "ClassB" };
            #endregion Classes

            #region Students
            var student0 = new Student { Name = "Dung", Class = classA, Parent = parent1 };
            var student1 = new Student { Name = "Tri", Class = classA, Parent = parent1 };
            var student2 = new Student { Name = "Mai", Class = classA, Parent = parent2 };
            var student3 = new Student { Name = "Tung", Class = classA, Parent = parent2 };
            var student4 = new Student { Name = "Chau", Class = classA, Parent = parent3 };
            var student5 = new Student { Name = "Tin", Class = classB, Parent = parent3 };
            var student6 = new Student { Name = "Luan", Class = classB, Parent = parent4 };
            var student7 = new Student { Name = "Nhat", Class = classB, Parent = parent4 };
            var student8 = new Student { Name = "Hung", Class = classB, Parent = parent5 };
            var student9 = new Student { Name = "Tam", Class = classB, Parent = parent5 };
            #endregion Students

            #region ClassLessons
            var classLesson0 = new ClassLesson { Class = classA, Lesson = lesson0 };
            var classLesson1 = new ClassLesson { Class = classB, Lesson = lesson1 };
            #endregion ClassLessons

            #region StudentVocabularies
            var studentVocabulary00 = new StudentVocabulary { Student = student0, Vocabulary = voca0, IsFinished = true, NumberOfWrongTimes = 2, UpdatedDate = DateTime.Now.AddDays(-1) };
            var studentVocabulary01 = new StudentVocabulary { Student = student0, Vocabulary = voca1, IsFinished = true, NumberOfWrongTimes = 3, UpdatedDate = DateTime.Now.AddDays(-2) };
            var studentVocabulary02 = new StudentVocabulary { Student = student0, Vocabulary = voca2, IsFinished = true, NumberOfWrongTimes = 4, UpdatedDate = DateTime.Now.AddDays(-3) };
            var studentVocabulary03 = new StudentVocabulary { Student = student0, Vocabulary = voca3, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-4) };
            var studentVocabulary04 = new StudentVocabulary { Student = student0, Vocabulary = voca4, IsFinished = true, NumberOfWrongTimes = 6, UpdatedDate = DateTime.Now.AddDays(-5) };

            var studentVocabulary10 = new StudentVocabulary { Student = student1, Vocabulary = voca0, IsFinished = true, NumberOfWrongTimes = 7, UpdatedDate = DateTime.Now.AddDays(-5) };
            var studentVocabulary11 = new StudentVocabulary { Student = student1, Vocabulary = voca1, IsFinished = true, NumberOfWrongTimes = 8, UpdatedDate = DateTime.Now.AddDays(-4) };
            var studentVocabulary12 = new StudentVocabulary { Student = student1, Vocabulary = voca2, IsFinished = true, NumberOfWrongTimes = 9, UpdatedDate = DateTime.Now.AddDays(-3) };
            var studentVocabulary13 = new StudentVocabulary { Student = student1, Vocabulary = voca3, IsFinished = true, NumberOfWrongTimes = 2, UpdatedDate = DateTime.Now.AddDays(-2) };
            var studentVocabulary14 = new StudentVocabulary { Student = student1, Vocabulary = voca4, IsFinished = true, NumberOfWrongTimes = 3, UpdatedDate = DateTime.Now.AddDays(-1) };

            var studentVocabulary20 = new StudentVocabulary { Student = student2, Vocabulary = voca0, IsFinished = true, NumberOfWrongTimes = 4, UpdatedDate = DateTime.Now.AddDays(-5) };
            var studentVocabulary21 = new StudentVocabulary { Student = student2, Vocabulary = voca1, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-6) };
            var studentVocabulary22 = new StudentVocabulary { Student = student2, Vocabulary = voca2, IsFinished = true, NumberOfWrongTimes = 6, UpdatedDate = DateTime.Now.AddDays(-7) };
            var studentVocabulary23 = new StudentVocabulary { Student = student2, Vocabulary = voca3, IsFinished = true, NumberOfWrongTimes = 7, UpdatedDate = DateTime.Now.AddDays(-8) };
            var studentVocabulary24 = new StudentVocabulary { Student = student2, Vocabulary = voca4, IsFinished = true, NumberOfWrongTimes = 8, UpdatedDate = DateTime.Now.AddDays(-2) };

            var studentVocabulary30 = new StudentVocabulary { Student = student3, Vocabulary = voca0, IsFinished = true, NumberOfWrongTimes = 3, UpdatedDate = DateTime.Now.AddDays(-4) };
            var studentVocabulary31 = new StudentVocabulary { Student = student3, Vocabulary = voca1, IsFinished = true, NumberOfWrongTimes = 2, UpdatedDate = DateTime.Now.AddDays(-6) };
            var studentVocabulary32 = new StudentVocabulary { Student = student3, Vocabulary = voca2, IsFinished = true, NumberOfWrongTimes = 6, UpdatedDate = DateTime.Now.AddDays(-8) };
            var studentVocabulary33 = new StudentVocabulary { Student = student3, Vocabulary = voca3, IsFinished = true, NumberOfWrongTimes = 0, UpdatedDate = DateTime.Now.AddDays(-4) };
            var studentVocabulary34 = new StudentVocabulary { Student = student3, Vocabulary = voca4, IsFinished = true, NumberOfWrongTimes = 8, UpdatedDate = DateTime.Now.AddDays(-5) };

            var studentVocabulary40 = new StudentVocabulary { Student = student4, Vocabulary = voca0, IsFinished = true, NumberOfWrongTimes = 3, UpdatedDate = DateTime.Now.AddDays(-2) };
            var studentVocabulary41 = new StudentVocabulary { Student = student4, Vocabulary = voca1, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-1) };
            var studentVocabulary42 = new StudentVocabulary { Student = student4, Vocabulary = voca2, IsFinished = true, NumberOfWrongTimes = 8, UpdatedDate = DateTime.Now.AddDays(-1) };
            var studentVocabulary43 = new StudentVocabulary { Student = student4, Vocabulary = voca3, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-4) };
            var studentVocabulary44 = new StudentVocabulary { Student = student4, Vocabulary = voca4, IsFinished = true, NumberOfWrongTimes = 4, UpdatedDate = DateTime.Now.AddDays(-6) };

            var studentVocabulary55 = new StudentVocabulary { Student = student5, Vocabulary = voca5, IsFinished = true, NumberOfWrongTimes = 3, UpdatedDate = DateTime.Now.AddDays(-6) };
            var studentVocabulary56 = new StudentVocabulary { Student = student5, Vocabulary = voca6, IsFinished = true, NumberOfWrongTimes = 4, UpdatedDate = DateTime.Now.AddDays(-8) };
            var studentVocabulary57 = new StudentVocabulary { Student = student5, Vocabulary = voca7, IsFinished = true, NumberOfWrongTimes = 6, UpdatedDate = DateTime.Now.AddDays(-4) };
            var studentVocabulary58 = new StudentVocabulary { Student = student5, Vocabulary = voca8, IsFinished = true, NumberOfWrongTimes = 7, UpdatedDate = DateTime.Now.AddDays(-3) };
            var studentVocabulary59 = new StudentVocabulary { Student = student5, Vocabulary = voca9, IsFinished = true, NumberOfWrongTimes = 8, UpdatedDate = DateTime.Now.AddDays(-2) };

            var studentVocabulary65 = new StudentVocabulary { Student = student6, Vocabulary = voca5, IsFinished = true, NumberOfWrongTimes = 3, UpdatedDate = DateTime.Now.AddDays(-6) };
            var studentVocabulary66 = new StudentVocabulary { Student = student6, Vocabulary = voca6, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-8) };
            var studentVocabulary67 = new StudentVocabulary { Student = student6, Vocabulary = voca7, IsFinished = true, NumberOfWrongTimes = 6, UpdatedDate = DateTime.Now.AddDays(-9) };
            var studentVocabulary68 = new StudentVocabulary { Student = student6, Vocabulary = voca8, IsFinished = true, NumberOfWrongTimes = 4, UpdatedDate = DateTime.Now.AddDays(-6) };
            var studentVocabulary69 = new StudentVocabulary { Student = student6, Vocabulary = voca9, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-4) };

            var studentVocabulary75 = new StudentVocabulary { Student = student7, Vocabulary = voca5, IsFinished = true, NumberOfWrongTimes = 4, UpdatedDate = DateTime.Now.AddDays(-5) };
            var studentVocabulary76 = new StudentVocabulary { Student = student7, Vocabulary = voca6, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-3) };
            var studentVocabulary77 = new StudentVocabulary { Student = student7, Vocabulary = voca7, IsFinished = true, NumberOfWrongTimes = 6, UpdatedDate = DateTime.Now.AddDays(-1) };
            var studentVocabulary78 = new StudentVocabulary { Student = student7, Vocabulary = voca8, IsFinished = true, NumberOfWrongTimes = 7, UpdatedDate = DateTime.Now.AddDays(-7) };
            var studentVocabulary79 = new StudentVocabulary { Student = student7, Vocabulary = voca9, IsFinished = true, NumberOfWrongTimes = 8, UpdatedDate = DateTime.Now.AddDays(-8) };

            var studentVocabulary85 = new StudentVocabulary { Student = student8, Vocabulary = voca5, IsFinished = true, NumberOfWrongTimes = 7, UpdatedDate = DateTime.Now.AddDays(-5) };
            var studentVocabulary86 = new StudentVocabulary { Student = student8, Vocabulary = voca6, IsFinished = true, NumberOfWrongTimes = 8, UpdatedDate = DateTime.Now.AddDays(-6) };
            var studentVocabulary87 = new StudentVocabulary { Student = student8, Vocabulary = voca7, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-4) };
            var studentVocabulary88 = new StudentVocabulary { Student = student8, Vocabulary = voca8, IsFinished = true, NumberOfWrongTimes = 3, UpdatedDate = DateTime.Now.AddDays(-3) };
            var studentVocabulary89 = new StudentVocabulary { Student = student8, Vocabulary = voca9, IsFinished = true, NumberOfWrongTimes = 2, UpdatedDate = DateTime.Now.AddDays(-2) };

            var studentVocabulary95 = new StudentVocabulary { Student = student9, Vocabulary = voca5, IsFinished = true, NumberOfWrongTimes = 1, UpdatedDate = DateTime.Now.AddDays(-8) };
            var studentVocabulary96 = new StudentVocabulary { Student = student9, Vocabulary = voca6, IsFinished = true, NumberOfWrongTimes = 4, UpdatedDate = DateTime.Now.AddDays(-7) };
            var studentVocabulary97 = new StudentVocabulary { Student = student9, Vocabulary = voca7, IsFinished = true, NumberOfWrongTimes = 5, UpdatedDate = DateTime.Now.AddDays(-6) };
            var studentVocabulary98 = new StudentVocabulary { Student = student9, Vocabulary = voca8, IsFinished = true, NumberOfWrongTimes = 6, UpdatedDate = DateTime.Now.AddDays(-5) };
            var studentVocabulary99 = new StudentVocabulary { Student = student9, Vocabulary = voca9, IsFinished = true, NumberOfWrongTimes = 7, UpdatedDate = DateTime.Now.AddDays(-4) };
            #endregion StudentVocabularies

            #region StudentGroups
            var studentGroup00 = new StudentGroup { Student = student0, Group = group0, IsFinished = true };
            var studentGroup01 = new StudentGroup { Student = student0, Group = group1, IsFinished = true };
            var studentGroup10 = new StudentGroup { Student = student1, Group = group0, IsFinished = true };
            var studentGroup11 = new StudentGroup { Student = student1, Group = group1, IsFinished = true };
            var studentGroup20 = new StudentGroup { Student = student2, Group = group0, IsFinished = true };
            var studentGroup21 = new StudentGroup { Student = student2, Group = group1, IsFinished = true };
            var studentGroup30 = new StudentGroup { Student = student3, Group = group0, IsFinished = true };
            var studentGroup31 = new StudentGroup { Student = student3, Group = group1, IsFinished = true };
            var studentGroup40 = new StudentGroup { Student = student4, Group = group0, IsFinished = true };
            var studentGroup41 = new StudentGroup { Student = student4, Group = group1, IsFinished = true };

            var studentGroup52 = new StudentGroup { Student = student5, Group = group2, IsFinished = true };
            var studentGroup53 = new StudentGroup { Student = student5, Group = group3, IsFinished = true };
            var studentGroup62 = new StudentGroup { Student = student6, Group = group2, IsFinished = true };
            var studentGroup63 = new StudentGroup { Student = student6, Group = group3, IsFinished = true };
            var studentGroup72 = new StudentGroup { Student = student7, Group = group2, IsFinished = true };
            var studentGroup73 = new StudentGroup { Student = student7, Group = group3, IsFinished = true };
            var studentGroup82 = new StudentGroup { Student = student8, Group = group2, IsFinished = true };
            var studentGroup83 = new StudentGroup { Student = student8, Group = group3, IsFinished = true };
            var studentGroup92 = new StudentGroup { Student = student9, Group = group2, IsFinished = true };
            var studentGroup93 = new StudentGroup { Student = student9, Group = group3, IsFinished = true };
            #endregion StudentGroups

            #region StudentLessons
            var studentLesson0 = new StudentLesson { Student = student0, Lesson = lesson0, IsFinished = true, Feeling = 0 };
            var studentLesson1 = new StudentLesson { Student = student1, Lesson = lesson0, IsFinished = true, Feeling = 1 };
            var studentLesson2 = new StudentLesson { Student = student2, Lesson = lesson0, IsFinished = true, Feeling = 2 };
            var studentLesson3 = new StudentLesson { Student = student3, Lesson = lesson0, IsFinished = true, Feeling = 0 };
            var studentLesson4 = new StudentLesson { Student = student4, Lesson = lesson0, IsFinished = true, Feeling = 1 };

            var studentLesson5 = new StudentLesson { Student = student5, Lesson = lesson1, IsFinished = true, Feeling = 0 };
            var studentLesson6 = new StudentLesson { Student = student6, Lesson = lesson1, IsFinished = true, Feeling = 2 };
            var studentLesson7 = new StudentLesson { Student = student7, Lesson = lesson1, IsFinished = true, Feeling = 2 };
            var studentLesson8 = new StudentLesson { Student = student8, Lesson = lesson1, IsFinished = true, Feeling = 1 };
            var studentLesson9 = new StudentLesson { Student = student9, Lesson = lesson1, IsFinished = true, Feeling = 1 };
            #endregion StudentLessons

            #region context AddOrUpdate
            context.Lessons.AddOrUpdate(l => l.Name, lesson0, lesson1);
            context.Classes.AddOrUpdate(c => c.Name, classA, classB);
            context.ClassLessons.AddOrUpdate(classLesson0, classLesson1);

            context.StudentGroups.AddOrUpdate(
                studentGroup00, studentGroup01, studentGroup10, studentGroup11, studentGroup20, studentGroup21, studentGroup30, studentGroup31, studentGroup40, studentGroup41,
                 studentGroup52, studentGroup53, studentGroup62, studentGroup63, studentGroup72, studentGroup73, studentGroup82, studentGroup83, studentGroup92, studentGroup93);

            context.StudentLessons.AddOrUpdate(
                studentLesson0, studentLesson1, studentLesson2, studentLesson3, studentLesson4,
                studentLesson5, studentLesson6, studentLesson7, studentLesson8, studentLesson9);

            context.StudentVocabularies.AddOrUpdate(
                studentVocabulary00, studentVocabulary01, studentVocabulary02, studentVocabulary03, studentVocabulary04,
                studentVocabulary10, studentVocabulary11, studentVocabulary12, studentVocabulary13, studentVocabulary14,
                studentVocabulary20, studentVocabulary21, studentVocabulary22, studentVocabulary23, studentVocabulary24,
                studentVocabulary30, studentVocabulary31, studentVocabulary32, studentVocabulary33, studentVocabulary34,
                studentVocabulary40, studentVocabulary41, studentVocabulary42, studentVocabulary43, studentVocabulary44,

                studentVocabulary55, studentVocabulary56, studentVocabulary57, studentVocabulary58, studentVocabulary59,
                studentVocabulary65, studentVocabulary66, studentVocabulary67, studentVocabulary68, studentVocabulary69,
                studentVocabulary75, studentVocabulary76, studentVocabulary77, studentVocabulary78, studentVocabulary79,
                studentVocabulary85, studentVocabulary86, studentVocabulary87, studentVocabulary88, studentVocabulary89,
                studentVocabulary95, studentVocabulary96, studentVocabulary97, studentVocabulary98, studentVocabulary99);
            #endregion context AddOrUpdate
        }
    }
}