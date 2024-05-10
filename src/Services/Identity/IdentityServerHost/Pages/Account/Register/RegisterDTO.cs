using System.ComponentModel.DataAnnotations;

namespace IdentityServerHost.Pages.Account.Register
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Button {  get; set; }
    }
}
