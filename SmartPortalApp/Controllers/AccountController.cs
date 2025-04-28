using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Data;
using SmartPortalApp.Models;

namespace SmartPortalApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject ApplicationDbContext
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the login page
        public IActionResult Login()
        {
            return View();
        }

        // Login method - handles user authentication
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            // Validate input
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            // Retrieve the user from the database
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Log a message to confirm successful user retrieval
                Console.WriteLine($"User {user.Username} authenticated successfully.");
                //create role by assigning claims the name from roles table
                var roleName = await _context.Roles.Where(r => r.RoleId == user.RoleId).Select(r => r.RoleName).FirstOrDefaultAsync();

                // Create the identity for the user
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Role,roleName?? string.Empty)
                    // Optional: Include email in claims
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Sign the user in
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true, // Persistent across browser sessions
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20) // Cookie expiration
                });

                // Redirect to the return URL or the home page
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            // Log invalid login attempt
            Console.WriteLine($"Invalid login attempt for user: {username}");
            ModelState.AddModelError("", "Invalid username or password.");

            return View();
        }

        // Logout method - clears the authentication cookie
        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            // Clear the existing authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Log the logout action
            Console.WriteLine("User logged out successfully.");

            // Optionally redirect to a return URL or a default page
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Login", "Account"); // Redirect to Login page
        }
    }
}



