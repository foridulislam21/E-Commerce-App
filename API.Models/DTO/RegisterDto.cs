using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression ("(?=^.{6,15}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have 1 UpperCase, 1 LowerCase, 1 Number, 1 Non Aphanumeric and at least 6 characters")]
        public string Password { get; set; }
    }
}