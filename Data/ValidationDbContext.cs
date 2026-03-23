using Microsoft.EntityFrameworkCore;
using Day4_Day3Refactoring.Models;
using Day4_Day3Refactoring.Services;

namespace Day4_Day3Refactoring.Data
{
    public class ValidationDbContext: DbContext
    {
        public ValidationDbContext(DbContextOptions<ValidationDbContext> options ): base(options) { }

        public DbSet<ValidationResult> ValidationResults { get; set; }
    }
}
