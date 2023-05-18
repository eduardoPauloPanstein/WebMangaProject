using Entities.Enums;
using System.Security.Claims;

namespace WebApi.Services
{
    public class UserService
    {
        public static string GetToken(HttpContext _context)
        {
            var token = _context.User.Claims?.Where(c => c.Type == "Token")
                   .Select(c => c.Value).SingleOrDefault();
            return token;
        }

        public static bool IsRole(string role, HttpContext _context)
        {
            var userRole = _context.User.Claims?.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).SingleOrDefault();
            return userRole == role;
        }

        public static bool IsAmMyself(HttpContext _context, int? id)
        {
            int MyId = Convert.ToInt32(_context.User.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.PrimarySid, StringComparison.OrdinalIgnoreCase))?.Value);
            return MyId == id;
        }

        public static bool IsAuthenticated(HttpContext _context)
        {
            return _context.User.Identity.IsAuthenticated;
        }

        public static bool IsAdmin(HttpContext _context)
        {
            return IsRole(UserRoles.Admin.ToString(), _context);
        }


    }
}
