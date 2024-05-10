using IdentityServerHost.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServerHost.Pages.Account.SeedRoles
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public void OnGet()
        {
            bool isOwnerRoleExits = _roleManager.RoleExistsAsync(StacticUserRoles.OWNER).GetAwaiter().GetResult();
            bool isUserRoleExits = _roleManager.RoleExistsAsync(StacticUserRoles.USER).GetAwaiter().GetResult();
            bool isAdminRoleExits = _roleManager.RoleExistsAsync(StacticUserRoles.ADMIN).GetAwaiter().GetResult();
            if (isOwnerRoleExits && isUserRoleExits && isAdminRoleExits)
                Console.WriteLine("Roles Seeding is already exits");
            _roleManager.CreateAsync(new IdentityRole(StacticUserRoles.ADMIN)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(StacticUserRoles.USER)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(StacticUserRoles.OWNER)).GetAwaiter().GetResult();
        }
        public async void OnPost()
        {
            bool isOwnerRoleExits = await _roleManager.RoleExistsAsync(StacticUserRoles.OWNER);
            bool isUserRoleExits = await _roleManager.RoleExistsAsync(StacticUserRoles.USER);
            bool isAdminRoleExits = await _roleManager.RoleExistsAsync(StacticUserRoles.ADMIN);
            if (isOwnerRoleExits && isUserRoleExits && isAdminRoleExits)
                Console.WriteLine("Roles Seeding is already exits");
            await _roleManager.CreateAsync(new IdentityRole(StacticUserRoles.OWNER));
            await _roleManager.CreateAsync(new IdentityRole(StacticUserRoles.OWNER));
            await _roleManager.CreateAsync(new IdentityRole(StacticUserRoles.OWNER));
        }
    }
}
