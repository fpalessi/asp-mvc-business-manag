using Microsoft.AspNetCore.Mvc;
using SandraConfecciones.Models;
using SandraConfecciones.Services.Implementation;
using SandraConfecciones.Resources;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using SandraConfecciones.Services.Interfaces;


namespace SandraConfecciones.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;

        public AuthController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Usuario model)
        {
            var existingUser = await _usuarioServicio.GetUserByEmail(model.Email);

            if (existingUser != null)
            {
                ViewData["Mensaje"] = "Ya existe un usuario registrado con este correo electrónico.";
                return View();
            }

            model.Password = Utilities.EncryptPassword(model.Password);

            Usuario newUser = await _usuarioServicio.SaveUser(model);

            if (newUser.UsuarioId > 0) return RedirectToAction("Login", "Auth");

            ViewData["Mensaje"] = "No se pudo crear el usuario";

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario userFound = await _usuarioServicio.GetUser(email, Utilities.EncryptPassword(password));

            if (userFound == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userFound.Email),
            };

            foreach (string rol in userFound.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            // Crear las propiedades de la autenticación
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                properties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Auth");
        }
    }
}
