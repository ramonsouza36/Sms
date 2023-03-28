using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolManagerSystem.Pages;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> logger;

    public LoginModel(
         ILogger<LoginModel> logger)
    {
        this.logger = logger;
    }

    [BindProperty]
    public string? ErrorText { get; set; }

    public void OnGet()
    {
    }
}
