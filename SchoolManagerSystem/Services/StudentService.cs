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

        public async Task DeleteStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var student = await GetStudentByIdAsync(DbFactory,id);
            context.Student.Remove(student);
            await context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Student student)
        {
            using var context = DbFactory.CreateDbContext();
            var studentOld = await GetStudentByIdAsync(DbFactory,student.Id);
            studentOld.Name = student.Name;
            studentOld.Email = student.Email;
            studentOld.FederalRegistration = student.FederalRegistration;
            studentOld.PhoneNumber = student.PhoneNumber;
            studentOld.BirthDate = student.BirthDate;
            
            context.Student.Update(studentOld);
            await context.SaveChangesAsync();
        }
    }
}