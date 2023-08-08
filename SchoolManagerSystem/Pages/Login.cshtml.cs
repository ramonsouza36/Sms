using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagerSystem.Services;
using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace SchoolManagerSystem.Pages;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> logger;

    private readonly ApplicationDbContext _context;

    private readonly UserManager<IdentityUser> _userManager;

    private LoginService loginService;

    [BindProperty]
    public string ErrorText { get; set; } = "";

    [BindProperty]
    public string? Username { get; set; }

    [BindProperty]
    public string? Password { get; set; }

    public LoginModel(
         ILogger<LoginModel> logger,
         ApplicationDbContext context,
         LoginService loginService,
         UserManager<IdentityUser> userManager)
    {
        this.logger = logger;
        _context = context;
        this.loginService = loginService;
        _userManager = userManager;
    }

    public void OnGet()
    {
        Redirect("/courses");
    }

    public async Task<IActionResult?> OnPost()
    {
        ErrorText = await loginService.LoginAsync(Username, Password,_context);
        if(string.IsNullOrEmpty(ErrorText))
        {
            var identity = new ClaimsIdentity("password");
            var user = _context.Users.Where(i => i.UserName == Username).FirstOrDefault();
            var roles = await _userManager.GetRolesAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            if(roles.Count() > 0)
            {
                foreach(var role in roles)
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal,authProperties);
            return Redirect("/");
        }
        else
            return null;
    }
}
