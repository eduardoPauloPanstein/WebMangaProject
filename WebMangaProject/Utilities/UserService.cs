using System.Security.Claims;

namespace MvcPresentationLayer.Utilities
{
    public class UserService
    {
        public static string GetToken(HttpContext _context)
        {
            var token = _context.User.Claims?.Where(c => c.Type == "Token")
                   .Select(c => c.Value).SingleOrDefault();
            return token;
        }
        public static int GetId(HttpContext _context) => Convert.ToInt32(_context.User.Claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.PrimarySid, StringComparison.OrdinalIgnoreCase))?.Value);

        public static bool IsRole(string role, HttpContext _context)
        {
            var userRole = _context.User.Claims?.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value).SingleOrDefault();
            return userRole != role;
        }
    }
}
