using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Cliínica.Data;
using Projeto_Cliínica.Helpers;
using Projeto_Cliínica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login login)
        {
            if (login.LoginUsuario.Trim() != "" && login.Senha.Trim() != "")
            {
                Login loginQuery = dataContext.TBLogins
                     .Include(l => l.TipoAcesso)
                   .FirstOrDefault(l => l.LoginUsuario == login.LoginUsuario);
                if (loginQuery != null)
                {
                    if (loginQuery.Senha == Criptografia.GeraHash(login.Senha))
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Sid, loginQuery.ID.ToString()),
                            new Claim(ClaimTypes.Name, loginQuery.LoginUsuario),
                            new Claim(ClaimTypes.Role, loginQuery.TipoAcesso.Grupo)
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
                else
                {
                    Login loginQueryAdmin = dataContext.TBLogins
                .FirstOrDefault(l => l.LoginUsuario == "admin");
                    if (loginQueryAdmin != null)
                    {
                        return RedirectToAction("Index", "Login");
                    }

                    Login padrao = new Login();
                    padrao.LoginUsuario = "admin";
                    padrao.Senha = Criptografia.GeraHash("admin");
                    return RedirectToAction("Alterar", "Login");
                }
            }
            ViewBag.Erro = "Usuário e/ou senha inválidos";
            return View();
        }

        public IActionResult Alterar()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenhaAdmin(string Senha, string SenhaConfirmacao)
        {
            if (Senha != SenhaConfirmacao)
            {
                ViewBag.Erro = "As Senha não coencidem";
                return View("Alterar");
            }

            Login loginQuery = dataContext.TBLogins
                  .FirstOrDefault(l => l.LoginUsuario == "admin");
            if (loginQuery != null)
            {
                return RedirectToAction("Index", "Login");
            }

            Login loginAdm = new Login();
            loginAdm.LoginUsuario = "admin";
            loginAdm.Senha = Criptografia.GeraHash(Senha);
            loginAdm.TipoAcessoID = 1;

            dataContext.TBLogins.Add(loginAdm);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
