using Day4_Day3Refactoring.Repositories;

//using System.ComponentModel.DataAnnotations;

using Day4_Day3Refactoring.Models;

namespace Day4_Day3Refactoring.Services

{

    public class PasswordValidatorService

    {



        private IValidationRepository _repo;

        private IConfiguration _configuration;

        //private List<ValidationResult> _validationResults;

        public PasswordValidatorService(IConfiguration configuration, IValidationRepository repo)

        {

            _configuration = configuration;

            _repo = repo;

            //_validationResults = new List<ValidationResult>();

        }



        //Service tells the repository to save the result
        /// <summary>
        /// Validation rules:
        /// Password must be at least 10 characters long
        /// Password must contain at least one digit and one of the following special characters: !, @, #, $, %
        /// Password must not contain any of the banned words specified in the configuration
        /// Password must not be a duplicate of any previously validated password
        /// </summary>
        /// <param name="passwords"></param>
        /// <returns></returns>
        public async Task<List<ValidationResult>> ValidatePassword(PasswordRequest passwords)

        {

            var bannedWords = _configuration.GetSection("ValidationSettings:BannedWords").Get<List<string>>() ?? new List<string>();

            var currentResults = new List<ValidationResult>();





            var processedPass = new HashSet<string>();

            List<char> specialCharac = new List<char> { '!', '@', '#', '$', '%' };



            foreach (string pass in passwords.Passwords)

            {



                bool hasDigit = false, hasCharac = false;

                if (processedPass.Contains(pass))

                {

                    ValidationResult result = new ValidationResult(pass, "Duplicate", false, DateTime.UtcNow);

                    await _repo.SaveAsync(result);

                    currentResults.Add(result);

                    continue;

                }

                if (pass.Length < 10)

                {

                    ValidationResult result = new ValidationResult(pass, "Pass is not long enough", false, DateTime.UtcNow);

                    await _repo.SaveAsync(result);

                    currentResults.Add(result);

                    continue;

                }

                foreach (char c in pass)

                {

                    if (char.IsDigit(c))

                    {

                        hasDigit = true;



                    }



                    if (specialCharac.Contains(c))

                    {

                        hasCharac = true;

                    }







                }



                if (!hasDigit || !hasCharac)

                {

                    ValidationResult result = new ValidationResult(pass, "Pass does not contain a number or a special charac", false, DateTime.UtcNow);

                    await _repo.SaveAsync(result);

                    currentResults.Add(result);

                    continue;

                }

                if (bannedWords.Any(bw => pass.Contains(bw, StringComparison.OrdinalIgnoreCase)))

                {

                    ValidationResult result = new ValidationResult(pass, "Forbidden content", false, DateTime.UtcNow);

                    await _repo.SaveAsync(result);

                    currentResults.Add(result);

                    continue;

                }





                processedPass.Add(pass);

                ValidationResult result1 = new ValidationResult(pass, "Pass is valid", true, DateTime.UtcNow);

                await _repo.SaveAsync(result1);

                currentResults.Add(result1);



            }







            return currentResults;

        }



        //public record ValidationResult(string Password, string Reason, bool IsValid, DateTime CheckedAt);

    }
}
