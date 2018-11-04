using System.ComponentModel.DataAnnotations;

namespace HighSchool.Web.ViewModels
{
    public class UserForViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "This field must have at least 5 characters")]
        [Display(Name = "Username or email")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "This field must have at least 5 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}