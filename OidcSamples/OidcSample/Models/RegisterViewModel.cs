using System.ComponentModel.DataAnnotations;

namespace OidcSample.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password"),DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}