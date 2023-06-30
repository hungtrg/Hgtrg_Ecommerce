using System.ComponentModel.DataAnnotations;

namespace Hgtrg.Ecommerce.BusinessLayer.RequestModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
