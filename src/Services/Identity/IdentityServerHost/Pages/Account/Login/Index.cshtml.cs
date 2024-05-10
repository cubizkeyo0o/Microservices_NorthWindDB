using IdentityServerHost.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace IdentityServerHost.Pages.Account.Login
{
    [AllowAnonymous] /*thêm attibute này để tránh bị loop redirect links:https://stackoverflow.com/questions/41478555/asp-net-core-cookie-authentication-too-many-requests */
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _config = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public IActionResult OnGet(string returnUrl)
        {
            BuildModel(returnUrl);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            
            if (Input.Button == "login")
            {
                var user = await _userManager.FindByNameAsync(Input.Username!);
                if (user is null) return Unauthorized();

                var userRoles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, Input.Username!),
                    new Claim(ClaimTypes.Role, "Admin"),
                };
                foreach(var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var ci = new ClaimsIdentity(claims, "Cookies");
                var cp = new ClaimsPrincipal(ci);
                await HttpContext.SignInAsync(cp);
            }

            return Redirect("~/");
        }
        private void BuildModel(string returnUrl)
        {
            Input = new InputModel()
            {
                ReturnUrl = returnUrl,
            };
        }
    }
}