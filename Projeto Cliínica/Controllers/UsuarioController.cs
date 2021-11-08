using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projeto_Cliínica.Data;
using Projeto_Cliínica.Helpers;
using Projeto_Cliínica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Controllers
{
    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class UsuarioController : Controller
    {
        private DataContext dataContext;
        public UsuarioController(DataContext dc)
        {
            dataContext = dc;
        }

        private bool ExistUsuario(Usuario usuario)
        {
            var result = (
                from user in dataContext.TBUsuarios
                join log in dataContext.TBLogins on user.LoginID equals log.ID
                where user.CPF == usuario.CPF && user.Status == true && log.User != "novo"
                select new { 
                    id = user.ID
                }
                ).ToList();

            return (result != null && result.Count() >= 0) ? true : false;
        }

        public IActionResult Index()
        {
            List<Usuario> lista = dataContext.TBUsuarios
                .Include(l => l.Login)
                .Where(u => u.Status == true)
                .ToList();
            return View(lista);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Medico medico)
        {
            if (medico == null)
                return RedirectToAction("Index");
            else
            {
                IQueryable<Medico> query = dataContext.TBMedicos
                    .Include(u => u.Usuario)
                    .Where(m => m.Usuario.Status == true);
                if (medico.Usuario.Nome != null && medico.Usuario.Nome != "")
                    query = query.Where(
                        m => m.Usuario.Nome.ToUpper().Contains(medico.Usuario.Nome.ToUpper())
                        );
                  
                if (medico.Usuario.Email != null && medico.Usuario.Email != "")
                    query = query.Where(
                        m => m.Usuario.Email.Contains(medico.Usuario.Email)
                        );

                if (medico.Usuario.DataNascimento != new DateTime(0001, 01, 01) && medico.Usuario.DataNascimento < DateTime.Today)
                    query = query.Where(m => m.Usuario.DataNascimento == medico.Usuario.DataNascimento);

                if (medico.Usuario.CPF != null && medico.Usuario.CPF != "")
                    query = query.Where(m => m.Usuario.CPF == medico.Usuario.CPF);

                if (medico.CRM != null && medico.CRM != "")
                    query = query.Where(m => m.CRM == medico.CRM);

                return View(query.ToList());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Os dados informados são inválidos");
            if (!ExistUsuario(usuario))
                return BadRequest("O usuário informado já existe");
            if(!Validacoes.IsValidCPF(usuario.CPF))
                return BadRequest("O CPF informado está inválido");

            IQueryable<Usuario> query = dataContext.TBUsuarios;
            query = query.Where(u => u.CPF == usuario.CPF);

            if(query.Count() > 0)
            {
                List<Usuario> lista = query.ToList();
                try
                {
                    foreach (var u in lista)
                    {
                        Login login = await dataContext.TBLogins.FindAsync(u.LoginID);
                        Usuario usuarioQuery = await dataContext.TBUsuarios.FindAsync(u.ID);
                        if (login != null && usuarioQuery != null) {
                            login.User = usuario.Login.User;
                            login.Senha = usuario.Login.Senha;

                            dataContext.TBLogins.Add(login);
                            dataContext.SaveChanges();

                            usuarioQuery.Nome = usuario.Nome.Trim();
                            usuarioQuery.Email = usuario.Email;
                            usuarioQuery.DataNascimento = usuario.DataNascimento;
                            usuarioQuery.LoginID = login.ID;
                            usuarioQuery.Login = null;

                            await dataContext.SaveChangesAsync();
                            return Ok(true);
                        }
                        return BadRequest("Ocorreu um erro inesperado no cadastro do usuário, por favor tente novamente");
                    }
                }
                catch
                {
                    return BadRequest("Ocorreu um erro inesperado no cadastro do usuário, por favor tente novamente");
                }
            }
            else
            {
                try
                {
                    Login newLogin = new Login();
                    newLogin.User = usuario.Login.User;
                    newLogin.Senha = usuario.Login.Senha;
                    newLogin.Papel = 4;
                    dataContext.TBLogins.Add(newLogin);
                    dataContext.SaveChanges();

                    Usuario newUsuario = new Usuario();
                    newUsuario.Nome = usuario.Nome;
                    newUsuario.Email = usuario.Email;
                    newUsuario.DataNascimento = usuario.DataNascimento;
                    newUsuario.CPF = usuario.CPF;
                    newUsuario.Status = true;
                    newUsuario.LoginID = newLogin.ID;
                    dataContext.TBUsuarios.Add(newUsuario);

                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch
                {
                    return BadRequest("Ocorreu um erro inesperado no cadastro do médico, por favor tente novamente");
                }
            }
            return BadRequest("Ocorreu um erro inesperado no cadastro do usuário, por favor tente novamente");
        }
        
        public IActionResult Edit(int? ID)
        {
            if (ID.HasValue)
            {
                Medico medico = dataContext.TBMedicos
                    .Include(u => u.Usuario)
                    .FirstOrDefault(m => m.ID == ID);
                if (medico != null)
                    return View(medico);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Medico medico)
        {
            return Ok(true);
            //Medico medicoValidacao = await dataContext.TBMedicos
            //        .FindAsync(medico.ID);
            //Usuario usuario = await dataContext.TBUsuarios.FindAsync(medicoValidacao.UsuarioID);
            //if (ModelState.IsValid == false)
            //    return BadRequest("Os dados informados são inválido");
            //if (!Validacoes.IsValidCPF(medico.Usuario.CPF))
            //    return BadRequest("O CPF informado está inválido");
            //if (medicoValidacao == null)
            //    return BadRequest("O médico informado não existe");
            //if(medicoValidacao != null && medico.Usuario.CPF != usuario.CPF && !ExistMedico(medico))
            //    return BadRequest("O médico informado já existe");

            //try
            //{
            //    usuario.Nome = medico.Usuario.Nome;
            //    usuario.Email = medico.Usuario.Email;
            //    usuario.DataNascimento = medico.Usuario.DataNascimento;
            //    usuario.CPF = medico.Usuario.CPF;
            //    usuario.Status = true;

            //    medicoValidacao.CRM = medico.CRM;

            //    await dataContext.SaveChangesAsync();
            //    return Ok(true);
            //}
            //catch(Exception e)
            //{
            //    return BadRequest("Ocorreu um erro inesperado na atualização do médico, por favor tente novamente");
            //}


        }
    
        [HttpDelete]
        public async Task<ActionResult> Delete(int? ID)
        {
            if (ID.HasValue)
            {
                Medico medico = await dataContext.TBMedicos.FindAsync(ID);
                Usuario usuario = await dataContext.TBUsuarios.FindAsync(medico.UsuarioID);
                try
                {
                    usuario.Status = false;
                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch
                {
                    return BadRequest("Ocorreu um erro inesperado na exclusão do médico, por favor tente novamente");
                }
            }
            return BadRequest("ID inválido");
        }
    }
}
