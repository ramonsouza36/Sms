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
            return course;
        }

        public async Task SetCourseAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Course course)
        {
            using var context = DbFactory.CreateDbContext();
            course.Id = Guid.NewGuid();
            if(course is not null && course.Id != Guid.Empty)
                context.Course.Add(course);
            await context.SaveChangesAsync();
        }

        public async Task<Course> GetCoursesByIdAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var course = context.Course.Where(i => i.Id == id).FirstOrDefault();
            return course;
        }

        public async Task DeleteCourseAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var course = await GetCoursesByIdAsync(DbFactory,id);
            context.Course.Remove(course);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Course course)
        {
            using var context = DbFactory.CreateDbContext();
            context.Course.Update(course);
            await context.SaveChangesAsync();
        }
    }
}