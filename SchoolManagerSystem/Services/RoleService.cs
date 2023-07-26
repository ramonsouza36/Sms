using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using SchoolManagerSystem.Models;

namespace SchoolManagerSystem.Services
{
    public class RoleService
    {
        public async Task<List<IdentityRole>> GetRolesAsync(IDbContextFactory<ApplicationDbContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<IdentityRole> roles = await context.Roles.Where(i => i.Id != Guid.Empty.ToString()).ToListAsync();
            return roles;
        }

        public async Task SetRoleAsync(IDbContextFactory<ApplicationDbContext> DbFactory, string nameRole)
        {
            using var context = DbFactory.CreateDbContext();
            var role = new IdentityRole();
            role.Name = nameRole;
            role.NormalizedName = nameRole.ToUpper();
            role.Id = Guid.NewGuid().ToString();
            context.Roles.Add(role);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(IDbContextFactory<ApplicationDbContext> DbFactory,string id)
        {
            using var context = DbFactory.CreateDbContext();
            var role = await context.Roles.Where(i => i.Id == id).FirstOrDefaultAsync();
            context.Roles.Remove(role!);
            await context.SaveChangesAsync();
        }
    }
}