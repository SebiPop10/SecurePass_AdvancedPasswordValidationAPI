using Microsoft.EntityFrameworkCore;
namespace Day4_Day3Refactoring.Models
{
    public class ValidationResult
    {
        public int Id { get; set; }
        public string Password { get; set; }= string.Empty;
        public string Reason { get; set; }= string.Empty;
        public bool IsValid { get; set; }
        public DateTime Timestamp { get; set; }
        public ValidationResult(string password, string reason, bool isValid, DateTime timestamp)
        {
            Password = password;
            Reason = reason;
            IsValid = isValid;
            Timestamp = timestamp;
        }

        public ValidationResult()
        {
        }
    }
}
