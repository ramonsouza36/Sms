using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using SchoolManagerSystem.Models;

namespace SchoolManagerSystem.Services
{
    public class ClassStudentService
    {
        public async Task<List<ClassStudent>> GetClassStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<ClassStudent> classStudents = await context.ClassStudent.Where(i => i.Id != Guid.Empty).ToListAsync();
            if(classStudents!= null)
            {
                foreach(var classStudent in classStudents)
                {
                    var nameInstr = context.Instructor.Where(i => i.Id == classStudent.InstructorId).FirstOrDefault();
                    var nameCourse = context.Course.Where(i => i.Id == classStudent.CourseId).FirstOrDefault();
                    classStudent.NameInstr = nameInstr!.Name!;
                    classStudent.NameCourse = nameCourse!.Name!;
                }
            }
            return classStudents;
        }

        public async Task<ClassStudent> GetClassStudentByIdAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var classStudent = context.ClassStudent.Where(i => i.Id == id).FirstOrDefault();
            if(classStudent != null)
            {
                var nameInstr = context.Instructor.Where(i => i.Id == classStudent.InstructorId).FirstOrDefault();
                var nameCourse = context.Course.Where(i => i.Id == classStudent.CourseId).FirstOrDefault();
                classStudent.NameInstr = nameInstr!.Name!;
                classStudent.NameCourse = nameCourse!.Name!;
            }
            return classStudent;
        }

        public async Task SetClassStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, ClassStudent classStudent)
        {
            using var context = DbFactory.CreateDbContext();
            classStudent.Id = Guid.NewGuid();
            var instructor = context.Instructor.Where(i => i.Name == classStudent.NameInstr).FirstOrDefault();
            var course = context.Course.Where(i => i.Name == classStudent.NameCourse).FirstOrDefault();
            classStudent.InstructorId = instructor!.Id;
            classStudent.CourseId = course!.Id!;
            if(classStudent is not null && classStudent.Id != Guid.Empty)
                context.ClassStudent.Add(classStudent);
            await context.SaveChangesAsync();
        }

        public async Task UpdateClassStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, ClassStudent classStudent)
        {
            using var context = DbFactory.CreateDbContext();
            context.ClassStudent.Update(classStudent);
            await context.SaveChangesAsync();
        }

        public async Task DeleteClassStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var classStudent = await GetClassStudentByIdAsync(DbFactory,id);
            context.ClassStudent.Remove(classStudent);
            await context.SaveChangesAsync();
        }
    }
}