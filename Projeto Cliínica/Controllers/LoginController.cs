using Microsoft.AspNetCore.Mvc;
using Projeto_Cliínica.Data;
using Projeto_Cliínica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Projeto_Cliínica.Controllers
{
    public class LoginController : Controller
    {
        private DataContext dataContext;
        public LoginController(DataContext dc)
        {
            dataContext = dc;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login login)
        {
            login.Senha = login.geraHash(login.Senha);
            if (ModelState.IsValid)
            {
                bool logado = dataContext.TBLogins.Any(x => x.User == login.User && x.Senha == login.Senha);

                if (logado == true)
                {
                    Login user = dataContext.TBLogins.FirstOrDefault(x => x.User == login.User);

                    string role;
                    switch (user.Papel)
                    {
                        case 0:
                            role = "A";
                            break;
                        case 1:
                            role = "M";
                            break;
                        case 2:
                            role = "S";
                            break;
                        default:
                            role = "P";
                            break;
                    }
                    List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.User),
                new Claim(ClaimTypes.Role, role),
            };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                        IsPersistent = true,
                    };

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Erro = "Usuário e/ou senha inválidos. Teste2: " + login.Senha;
            return View();


        }


    }
}
