using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PoliceProject.Data;
using PoliceProject.Models;
using System.Security.Claims;

namespace PoliceProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly PoliceDbContext _context;

        public LoginController(PoliceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            List<Login> list = _context.Logins.ToList();

            foreach (Login login1 in list)
            {
                if (login.Username == login1.Username && login.Password == login1.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim (ClaimTypes.Name, login.Username),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                    {
                        IsPersistent = login.IsPersistent
                    });

                    return RedirectToAction("Index", "Report");
                }
            }
            ViewData["Errore"] = "Username e/o passoword sbagliati";
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
