using Microsoft.EntityFrameworkCore;
using System.Data;
using SchoolManagerSystem.Data;
using System.Text;
using System.Security.Cryptography;

namespace SchoolManagerSystem.Services
{
    public class LoginService
    {
        public UserService? userService;
        public async Task<string> LoginAsync(string userName, string password,ApplicationDbContext context)
        {
            var message = string.Empty;
            var login = await context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            if(login is not null)
            {
                var passwordHash = ComputeMD5(password);
                if(login.PasswordHash != passwordHash)
                {
                    message = "Senha inválida!";
                }
            }
            else
                message = "Usuário não existe!";
            
            return message;
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

    }
}