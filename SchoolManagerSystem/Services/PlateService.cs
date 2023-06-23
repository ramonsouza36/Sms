using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using SchoolManagerSystem.Models;

namespace SchoolManagerSystem.Services
{
    public class PlateService
    {
        
        public async Task<List<Plate>> GetPlateAsync(IDbContextFactory<ApplicationDbContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<Plate> plates = await context.Plate.Where(i => i.Id != Guid.Empty).ToListAsync();
            if(plates!= null)
            {
                foreach(var plate in plates)
                {
                    var classStudent = await context.ClassStudent.Where(i => i.Id == plate.ClassStudentId).FirstOrDefaultAsync();
                    var student = await context.Student.Where(i => i.Id == plate.StudentId).FirstOrDefaultAsync();
                    var course = await context.Course.Where(i => i.Id == classStudent!.CourseId).FirstOrDefaultAsync();
                    plate.NameCourse = course!.Name!;
                    plate.NameStudent = student!.Name!;
                }
            }
            return plates;
        }

        public async Task<Plate> GetPlateByIdAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid Id)
        {
            using var context = DbFactory.CreateDbContext();
            var plate = await context.Plate.Where(i => i.Id == Id).FirstOrDefaultAsync();
            if(plate!= null)
            {
                var classStudent = await context.ClassStudent.Where(i => i.Id == plate.ClassStudentId).FirstOrDefaultAsync();
                var student = await context.Student.Where(i => i.Id == plate.StudentId).FirstOrDefaultAsync();
                var course = await context.Course.Where(i => i.Id == classStudent!.CourseId).FirstOrDefaultAsync();
                plate.NameCourse = course!.Name!;
                plate.NameStudent = student!.Name!;
            }
            return plate;
        }

        public async Task SetPlateAsync(IDbContextFactory<ApplicationDbContext> DbFactory,Plate plate)
        {
            using var context = DbFactory.CreateDbContext();
            plate.Id = Guid.NewGuid();
            var classStudent = await context.ClassStudent.Where(i => i.Id == plate.ClassStudentId).ToListAsync();
            var student = await context.Student.Where(i => i.Id == plate.StudentId).ToListAsync();
            plate.NameCourse = classStudent.FirstOrDefault()!.NameCourse;
            plate.NameStudent = student.FirstOrDefault()!.Name!;    
            context.Plate.Add(plate);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePlateAsync(IDbContextFactory<ApplicationDbContext> DbFactory,Plate plate)
        {
            using var context = DbFactory.CreateDbContext();
            context.Plate.Update(plate);
            await context.SaveChangesAsync();
        }

        public async Task DeleteClassStudentAsync(IDbContextFactory<ApplicationDbContext> DbFactory, Guid id)
        {
            using var context = DbFactory.CreateDbContext();
            var plate = await GetPlateByIdAsync(DbFactory,id);
            context.Plate.Remove(plate);
            await context.SaveChangesAsync();
        }
    }
}