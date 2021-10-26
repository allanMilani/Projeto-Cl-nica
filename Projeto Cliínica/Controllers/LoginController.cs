using Microsoft.AspNetCore.Mvc;
using Projeto_Cliínica.Data;
using Projeto_Cliínica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

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
            primeiroUsuario();
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login login)
        {
            Login padrao = new Login();
            padrao.User = "admin";
            padrao.Senha = "admin";

            string senhaInicial;
            senhaInicial = (login.Senha == padrao.Senha) ? "S" : "F";

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
                new Claim(ClaimTypes.Expired, senhaInicial)
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

            ViewBag.Erro = "Usuário e/ou senha inválidos.";
            return View();


        }

        public IActionResult Sair()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AlteraSenha(string senha1, string senha2)
        {
            Login login = new Login();
            login.User = "admin";
            Login loginValidacao = await dataContext.TBLogins
                    .FindAsync(1);
            if (senha1 != senha2)
                return BadRequest("As senhas não coincidem!");

            try
            {
                loginValidacao.Senha = senha1;
                await dataContext.SaveChangesAsync();
                Sair();
                return Ok(true);
            }catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro inesperado ao processar a alteração de senha!");
            }

            return BadRequest("Erro ao alterar");
        }
        private void primeiroUsuario()
        {
            
            IQueryable<Login> query = dataContext.TBLogins;
            query = query.Where(l => l.ID != 0);

            if (query.Count() == 0)
            {
                Login login = new Login();
                login.User = "admin";
                login.Senha = "admin";
                login.Papel = 0;
                dataContext.TBLogins.Add(login);
                dataContext.SaveChanges();
            }
        }

    }
}
