using HelpUs.API.DataAccess;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace HelpUs.API.Common
{
    public class HelperMethods(IHttpContextAccessor _httpContextAccessor, AppDbContext _context)
    {
        public (string,string) GetUserId()
        {
            var user = _httpContextAccessor.HttpContext!.User;
            
            if (user is null) return ("","");
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return ("", "");

            
            var role =  string.Empty;

            if (user.IsInRole(Roles.Admin))
            {
                role = Roles.Admin;
            }else if (user.IsInRole(Roles.Member))
            {
                role = Roles.Member;
            }else
                role = Roles.Donor;

            return (userId, role);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        public bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, "[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");
            bool hasSpecialChar = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':;{}|<>]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
    }
}
