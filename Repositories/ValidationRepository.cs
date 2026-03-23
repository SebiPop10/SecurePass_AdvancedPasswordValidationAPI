//using System.ComponentModel.DataAnnotations;
using Day4_Day3Refactoring.Services;
using Day4_Day3Refactoring.Models;
using Day4_Day3Refactoring.Data;
using Microsoft.EntityFrameworkCore;
namespace Day4_Day3Refactoring.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        //private  List<PasswordValidatorService.ValidationResult> validationResults;

        private ValidationDbContext _context;
        public ValidationRepository(ValidationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(ValidationResult result)
        {
             _context.ValidationResults.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ValidationResult>> GetAll()
        {
            // return all database entries except the passwords, which are not needed for the history endpoint and should not be exposed
            return await _context.ValidationResults.Select(r => new ValidationResult
            {
                Id = r.Id,
                Reason = r.Reason,
                IsValid = r.IsValid,
                Timestamp = r.Timestamp


            }).ToListAsync();
        }

        public async  Task<IEnumerable<ValidationResult>> GetByValidity(bool isValid)
        {
            return await _context.ValidationResults.Where(r => r.IsValid == isValid).ToListAsync();
        }

        public async Task DeleteAllAsync()
        {
            _context.ValidationResults.RemoveRange(_context.ValidationResults);
            await _context.SaveChangesAsync();
        }

        //public record ValidationResult(string Password, string Reason, bool IsValid, DateTime Timestamp);


    }
}
