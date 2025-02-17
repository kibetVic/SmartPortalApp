using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(User newUser)
        {
            // Check if the username already exists (case insensitive comparison)
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == newUser.Username.ToLower()
                                      || u.Email.ToLower() == newUser.Email.ToLower());

            if (existingUser != null)
            {
                return false; // Username already taken
            }

            // Hash the password and save the user
            newUser.Password = HashPassword(newUser.Password);
            newUser.LastChanged = DateTime.Now;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            // Add your password hashing logic here
            return password; 
        }
    }

}