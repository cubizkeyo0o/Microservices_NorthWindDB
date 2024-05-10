using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServerHost.Pages.Account.Logout
{
    public class IndexModel : PageModel
    {
        public async void OnGet()
        {
            await HttpContext.SignOutAsync("Cookies");
        }
    }
}
