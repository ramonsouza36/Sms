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
            List<ClassStudent> classStudent = await context.ClassStudent.Where(i => i.Id != Guid.Empty).ToListAsync();
            return classStudent;
        }

        public async Task<ClassStudent> GetClassStudentByIdAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var classStudent = context.ClassStudent.Where(i => i.Id == id).FirstOrDefault();
            return classStudent;
        }

        public async Task SetClassStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, ClassStudent classStudent)
        {
            using var context = DbFactory.CreateDbContext();
            classStudent.Id = Guid.NewGuid();
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