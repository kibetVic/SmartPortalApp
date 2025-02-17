using System;
using System.Linq;
using SmartPortalApp.Data;

namespace SmartPortalApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ValidateLastChanged(string lastChanged)
        {
            // Parse lastChanged and compare with database value
            if (DateTime.TryParse(lastChanged, out var lastChangedDate))
            {
                var user = _context.Users.FirstOrDefault(u => u.LastChanged == lastChangedDate);
                return user != null;
            }
            return false;
        }
    }
}
