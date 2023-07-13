using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolManagerSystem.Services;
using Microsoft.EntityFrameworkCore;
using SchoolManagerSystem.Data;

namespace SchoolManagerSystem.Pages;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> logger;

    private readonly ApplicationDbContext _context;

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
         LoginService loginService)
    {
        this.logger = logger;
        _context = context;
        this.loginService = loginService;
    }

    public void OnGet()
    {
        Redirect("/courses");
    }

    public async Task<IActionResult?> OnPost()
    {
        ErrorText = await loginService.LoginAsync(Username, Password,_context);
        if(string.IsNullOrEmpty(ErrorText))
            return Redirect("/");
        else
            return null;
    }
}
