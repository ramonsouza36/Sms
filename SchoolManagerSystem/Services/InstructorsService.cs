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
    }
}