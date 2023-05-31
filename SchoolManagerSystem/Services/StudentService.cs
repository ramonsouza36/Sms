using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using SchoolManagerSystem.Models;

namespace SchoolManagerSystem.Services
{
    public class StudentService
    {
        public async Task<List<Student>> GetStudentsAsync(IDbContextFactory<ApplicationDbContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<Student> students = await context.Student.Where(i => i.Id != Guid.Empty).ToListAsync();
            return students;
        }

        public async Task<Student> GetStudentByIdAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var student = context.Student.Where(i => i.Id == id).FirstOrDefault();
            return student;

        }

        public async Task SetStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Student student)
        {
            using var context = DbFactory.CreateDbContext();
            student.Id = Guid.NewGuid();
            if(student is not null && student.Id != Guid.Empty)
                context.Student.Add(student);
            await context.SaveChangesAsync();
        }
    }
}