//using System.ComponentModel.DataAnnotations;
using Day4_Day3Refactoring.Services;
using Day4_Day3Refactoring.Models;
namespace Day4_Day3Refactoring.Repositories
{
    public interface IValidationRepository
    {
        //public record ValidationResult();
        Task SaveAsync(ValidationResult result);
        Task<IEnumerable<ValidationResult>> GetAll();
        Task<IEnumerable<ValidationResult>> GetByValidity(bool isValid);
        Task DeleteAllAsync();
    }
}
