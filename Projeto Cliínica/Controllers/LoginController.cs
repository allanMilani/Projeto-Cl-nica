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
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Controllers
{
    public class LoginController : Controller
    {
        private DataContext dataContext;
        private string chaveCriptografi = "6u4SwXD3bOmxWYLKTcYe";
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
                   .FirstOrDefault(l => l.LoginUsuario == login.LoginUsuario && l.Status == true);
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

        public IActionResult Logoff()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(string SenhaAtual, string SenhaAtualConfirmacao, string NovaSenha, string NovaSenhaConfirmacao)
        {
            int idSessao = Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            Login login = dataContext.TBLogins.FirstOrDefault(l => l.ID == idSessao);
            if(SenhaAtualConfirmacao != SenhaAtual)
            {
                ViewBag.Erro = "A senha atual e de confirmação não coencidem";
                return View();
            }
            if (login.Senha != Criptografia.GeraHash(SenhaAtual))
            {
                ViewBag.Erro = "A senha informada está incorreta";
                return View();
            }
            if(NovaSenha != NovaSenhaConfirmacao)
            {
                ViewBag.Erro = "A nova senha e de confirmação não coencidem";
                return View();
            }
            login.Senha = Criptografia.GeraHash(NovaSenha);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarSenha(string emailRecuperar)
        {
            Login loginQuery = dataContext.TBLogins
                .Include(l => l.Usuario)
               .FirstOrDefault(l => l.Usuario.Email == emailRecuperar);
            if(loginQuery == null)
            {
                ViewBag.Erro = "O email informado não existe";
                return View();
            }

            string id = Criptografia.Cryptografar(Convert.ToString(loginQuery.ID), chaveCriptografi);
            string date = Criptografia.Cryptografar(Convert.ToString(DateTime.Now), chaveCriptografi);
            string token = id + "." + date;
            string message = "<html>"
                             + "<head><title>Recuperar Senha - Projeto Clínica</title></head>"
                             + "<body>"
                             + "Olá "
                             + loginQuery.Usuario.Nome
                             + ", acesse o link para recuperar sua senha."
                             + "<a href='https://localhost:44332/Login/NovaSenha?token="+ token +"' target='_blank'>Clique aqui</a>"
                             + "</body>"
                             + "</html>";
            if(Email.SendEmail(emailRecuperar, "Recuperar Senha - Projeto Clínica", message)){
                ViewBag.Success = "O link de redefinição foi enviada para seu email";
                return View();
            }
            else
            {
            ViewBag.Erro = "Ocorreu um erro na redefinição, por favor tente novamente";
            return View();
            }

        }

        public IActionResult NovaSenha(string token)
        {
            string[] tokens = token.Split('.');
            int ID = Convert.ToInt32(Criptografia.Descriptografar(tokens[0], chaveCriptografi));
            var date = Convert.ToDateTime(Criptografia.Descriptografar(tokens[1], chaveCriptografi));
            var dataAtual = DateTime.Now;
            TimeSpan diferenca = date - dataAtual;

            if (diferenca.TotalMinutes > 15)
            {
                ViewBag.Erro = "Link expirado";
            }
            else
            {
                ViewBag.ID = ID;
            }

            return View();

        }

        [HttpPost]
        public IActionResult NovaSenha(int? ID, string NovaSenha, string NovaSenhaConfirmacao)
        {
            if (ID.HasValue)
            {
                Login login = dataContext.TBLogins
                    .FirstOrDefault(l => l.ID == ID);
                if(NovaSenha == NovaSenhaConfirmacao)
                {
                    login.Senha = Criptografia.GeraHash(NovaSenha);
                    dataContext.SaveChanges();
                    ViewBag.Success = "Senha alterada com sucesso!";
                    return View();
                }
                ViewBag.Erro = "As Senhas não coencidem";
                return View();
            }
            ViewBag.Erro = "O ID informado está inválido";
            return View();
        }
    }
}
