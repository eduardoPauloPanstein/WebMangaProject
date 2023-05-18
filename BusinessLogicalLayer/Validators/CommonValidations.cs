using Shared;
using Shared.Responses;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BusinessLogicalLayer.Validators
{
    internal class CommonValidations
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static Response HasValidPassword(string pw)
		{
			var lowercase = new Regex("[a-z]+");
			var uppercase = new Regex("[A-Z]+");
			var digit = new Regex("(\\d)+");
			var symbol = new Regex("(\\W)+");

            if (!digit.IsMatch(pw))
                return ResponseFactory.CreateInstance().CreateFailedResponse("Password needs at least one digit");
            else if (symbol.IsMatch(pw))
                return ResponseFactory.CreateInstance().CreateFailedResponse("Password needs at least one symbol");
            else if (uppercase.IsMatch(pw))
                return ResponseFactory.CreateInstance().CreateFailedResponse("Password needs at least one uppercase character");
            else if (lowercase.IsMatch(pw))
                return ResponseFactory.CreateInstance().CreateFailedResponse("Password needs at least one lowercase character");
            else
                return ResponseFactory.CreateInstance().CreateSuccessResponse();
		}

        public static bool HasDigit(string pw)
        {
            var digit = new Regex("(\\d)+");
            return digit.IsMatch(pw);
        }

    }
}
