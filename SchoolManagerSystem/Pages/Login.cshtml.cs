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

    [BindProperty]
    public string? ErrorText { get; set; }

    [BindProperty]
    public string? Username { get; set; }

    [BindProperty]
    public string? Password { get; set; }

    public LoginModel(
         ILogger<LoginModel> logger)
    {
        this.logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        if (Username.Equals("abc") && Password.Equals("123"))
        {
            return RedirectToPage("Welcome");
        }
        else
        {
            return Page();
        }
    }
}
