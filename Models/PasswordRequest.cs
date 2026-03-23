using System.ComponentModel.DataAnnotations;

namespace Day4_Day3Refactoring.Models
{
    public class PasswordRequest
    {
        [Required]
        [MinLength(1, ErrorMessage ="You must provide at least one password")]
        public List<string> Passwords { get; set; } = new List<string>();

    }
}
