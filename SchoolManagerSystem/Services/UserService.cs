using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using SchoolManagerSystem.Models;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagerSystem.Services
{
    public class UserService
    {
        public async Task<List<IdentityUser>> GetUsersAsync(IDbContextFactory<ApplicationDbContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<IdentityUser> users = await context.Users.Where(i => i.Id != Guid.Empty.ToString()).ToListAsync();
            return users;
        }

        public async Task<IdentityUser> GetUserByIdAsync(IDbContextFactory<ApplicationDbContext> DbFactory, string id)
        {
            using var context = DbFactory.CreateDbContext();
            var users = await context.Users.Where(i => i.Id == id).FirstOrDefaultAsync();
            return users;
        }

        public async Task<IdentityUser> GetUserByUserNameAsync(IDbContextFactory<ApplicationDbContext> DbFactory, string userName)
        {
            using var context = DbFactory.CreateDbContext();
            var users = await context.Users.Where(i => i.UserName == userName).FirstOrDefaultAsync();
            return users;
        }

        public async Task SetUserAsync(IDbContextFactory<ApplicationDbContext> DbFactory,IdentityUser newUser, string password,Guid typeId)
        {
            using var context = DbFactory.CreateDbContext();
            var id = Guid.NewGuid();
            newUser.Id = id.ToString();
            newUser.PasswordHash = ComputeMD5(password);
            context.Users.Add(newUser);
            var userRoles = new IdentityUserRole<string>();
            userRoles.UserId = id.ToString();
            userRoles.RoleId = typeId.ToString();
            context.UserRoles.Add(userRoles);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(IDbContextFactory<ApplicationDbContext> DbFactory, IdentityUser newUser, string password,Guid typeId)
        {
            using var context = DbFactory.CreateDbContext();
            if(!string.IsNullOrEmpty(password))  
                newUser.PasswordHash = ComputeMD5(password);
            var roles = await context.UserRoles.Where(i =>i.RoleId == typeId.ToString() && i.UserId == newUser.Id).ToListAsync();
            if(roles != null && !roles.Any(i => i.RoleId == typeId.ToString()))
            {
                var userRoles = new IdentityUserRole<string>
                {
                    UserId = newUser.Id,
                    RoleId = typeId.ToString()
                };
                context.UserRoles.Add(userRoles);
            }
            context.Users.Update(newUser);
            await context.SaveChangesAsync();
        }

        static string ComputeMD5(string s)
        {
            StringBuilder sb = new StringBuilder();
    
            // Inicializa um objeto hash MD5
            using (MD5 md5 = MD5.Create())
            {
                // Calcula o hash da string dada
                byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
    
                // Converte o array de bytes para o formato string
                foreach (byte b in hashValue) {
                    sb.Append($"{b:X2}");
                }
            }   
            return sb.ToString();
        }

        public async Task UpdateUserAsync(IDbContextFactory<ApplicationDbContext> DbFactory,IdentityUser user)
        {
            using var context = DbFactory.CreateDbContext();
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(IDbContextFactory<ApplicationDbContext> DbFactory,string id)
        {
            using var context = DbFactory.CreateDbContext();
            var user = await GetUserByIdAsync(DbFactory,id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}