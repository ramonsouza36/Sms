using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagerSystem.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
         AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<SchoolManagerSystem.Models.Course> Course { get; set; }
    public DbSet<SchoolManagerSystem.Models.Instructor> Instructor { get; set; }
    public DbSet<SchoolManagerSystem.Models.Student> Student { get; set; }
    public DbSet<SchoolManagerSystem.Models.ClassStudent> ClassStudent { get; set; }
    public DbSet<SchoolManagerSystem.Models.Plate> Plate { get; set; }
}
