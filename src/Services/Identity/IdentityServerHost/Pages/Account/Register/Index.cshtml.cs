using Duende.IdentityServer;
using IdentityServerHost.Models;
using IdentityServerHost.Pages.Account.SeedRoles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServerHost.Pages.Account.Register
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = configuration;
        }

        [BindProperty]
        public RegisterDTO RegisterDTO { get; set; } = default!;
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if(RegisterDTO.Button == "create")
            {
                var isUserExits = await _userManager.FindByNameAsync(RegisterDTO.UserName);
                if (isUserExits != null) return BadRequest("User Already Exits");
                ApplicationUser newUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = RegisterDTO.UserName,
                    Email = RegisterDTO.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                
                
                var createdUserResult = await _userManager.CreateAsync(newUser, RegisterDTO.Password);
                if (!createdUserResult.Succeeded)
                {
                    var errorString = "User Creation Failed Because: ";
                    foreach (var error in createdUserResult.Errors)
                    {
                        errorString += " # " + error.Description;
                    }
                    return BadRequest(errorString);
                }
                await _userManager.AddToRoleAsync(newUser, StacticUserRoles.USER);
                await _userManager.GetClaimsAsync(newUser);
            }
            
            return Page();
        }
        private string GenerateNewJWT(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            var tokenObject = new JwtSecurityToken(
                    issuer: _config["JWT:ValidateIssuer"],
                    audience: _config["JWT:ValidateAudience"],
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }
    }
}
