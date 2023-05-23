using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using SchoolManagerSystem.Models;

namespace SchoolManagerSystem.Services
{
    public class InstructorsService
    {
        public async Task<List<Instructor>> GetInstructorsAsync(IDbContextFactory<ApplicationDbContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<Instructor> instructor = await context.Instructor.Where(i => i.Id != Guid.Empty).ToListAsync();
            Console.WriteLine($"{instructor!.FirstOrDefault()!.Name}");
            return instructor;
        }

        public async Task SetInstructorAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Instructor instructor)
        {
            using var context = DbFactory.CreateDbContext();
            instructor.Id = Guid.NewGuid();
            if(instructor is not null && instructor.Id != Guid.Empty)
                context.Instructor.Add(instructor);
            Console.WriteLine($"{instructor!.Name}");
            await context.SaveChangesAsync();
        }

        public async Task<Instructor> GetInstructorsByIdAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var instructor = context.Instructor.Where(i => i.Id != Guid.Empty).FirstOrDefault();
            return instructor;

        }
    }
}