using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using SchoolManagerSystem.Models;

namespace SchoolManagerSystem.Services
{
    public class CourseService
    {
        public async Task<List<Course>> GetCourseAsync(IDbContextFactory<ApplicationDbContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<Course> course = await context.Course.Where(i => i.Id != Guid.Empty).ToListAsync();
            Console.WriteLine($"{course!.FirstOrDefault()!.Name} from service");
            return course;
        }

        public async Task SetCourseAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Course course)
        {
            using var context = DbFactory.CreateDbContext();
            course.Id = Guid.NewGuid();
            if(course is not null && course.Id != Guid.Empty)
                context.Course.Add(course);
            Console.WriteLine($"{course!.Name} from Service");
            await context.SaveChangesAsync();
        }
    }
}