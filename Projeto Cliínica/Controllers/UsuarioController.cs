using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IEnumerable<SelectListItem> listaAcessos;
        public UsuarioController(DataContext dc)
        {
            dataContext = dc;
            listaAcessos = dataContext.TBTipoAcessos.ToList()
               .Select(
                   e => new SelectListItem()
                   {
                       Text = e.Nome,
                       Value = e.ID.ToString()
                   }).ToList();
        }
        private bool ExisteUsuario(Login login)
        {
            var result = (
                from TBlog in dataContext.TBLogins
                join TBusu in dataContext.TBUsuarios on TBlog.UsuarioID equals TBusu.ID
                where TBusu.CPF == login.Usuario.CPF && TBusu.Status == true && TBlog.LoginUsuario == login.LoginUsuario
                select new
                {
                    id = TBusu.ID
                }
                ).ToList();
            return (result != null && result.Count() > 0) ? true : false;
        }
        public IActionResult Index()
        {
            ViewBag.Acessos = listaAcessos;
            List<Login> lista = dataContext.TBLogins
               .Include(u => u.Usuario)
               .Include(ta => ta.TipoAcesso)
               .Where(u => u.Usuario.Status == true)
               .ToList();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Login login)
        {
            if (login == null)
                return RedirectToAction("Index");
            else
            {
                ViewBag.Acessos = listaAcessos;
                IQueryable<Login> query = dataContext.TBLogins
                    .Include(u => u.Usuario)
                    .Where(u => u.Usuario.Status == true);
                if (login.Usuario.Nome != null && login.Usuario.Nome != "")
                    query = query.Where(
                        m => m.Usuario.Nome.ToUpper().Contains(login.Usuario.Nome.ToUpper())
                        );

                if (login.Usuario.Email != null && login.Usuario.Email != "")
                    query = query.Where(
                        m => m.Usuario.Email.Contains(login.Usuario.Email)
                        );

                if (login.Usuario.DataNascimento != new DateTime(0001, 01, 01) && login.Usuario.DataNascimento < DateTime.Today)
                    query = query.Where(m => m.Usuario.DataNascimento == login.Usuario.DataNascimento);

                if (login.Usuario.CPF != null && login.Usuario.CPF != "")
                    query = query.Where(m => m.Usuario.CPF == login.Usuario.CPF);

                if (login.LoginUsuario != null && login.LoginUsuario != "")
                    query = query.Where(m => m.LoginUsuario == login.LoginUsuario);

                if(login.TipoAcessoID != null)
                    query = query.Where(m => m.TipoAcessoID == login.TipoAcessoID);

                return View(query.ToList());
            }
        }

        public IActionResult Create()
        {
           
            ViewBag.Acessos = listaAcessos;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Login login)
        {
            if (
                login.Usuario.Nome.Trim() == "" ||
                login.Usuario.Email.Trim() == "" ||
                login.Usuario.CPF.Trim() == "" ||
                login.LoginUsuario.Trim() == "" ||
                login.Senha.Trim() == ""
               )
                return BadRequest("Os dados informados são inválidos");
            if (ExisteUsuario(login))
                return BadRequest("O usuário informado já existe");
            if (!Validacoes.IsValidCPF(login.Usuario.CPF))
                return BadRequest("O CPF informado está inválido");
            Usuario usuarioQuery = await dataContext.TBUsuarios.FirstOrDefaultAsync(u => u.CPF == login.Usuario.CPF);
            if(usuarioQuery != null)
            {
                try
                {
                    usuarioQuery.Nome = login.Usuario.Nome;
                    usuarioQuery.Email = login.Usuario.Email;
                    usuarioQuery.DataNascimento = login.Usuario.DataNascimento;
                    usuarioQuery.CPF = login.Usuario.CPF;

                    login.TipoAcessoID = login.TipoAcesso.ID;

                    login.UsuarioID = usuarioQuery.ID;
                    login.Usuario = null;

                    login.Senha = Criptografia.GeraHash(login.Senha);
                    dataContext.TBLogins.Add(login);

                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                    //return BadRequest("Ocorreu um erro inesperado na atualização da secretaria, por favor tente novamente");
                }
            }
            else
            {
                try
                {
                    Usuario usuario = new Usuario();
                    usuario.Nome = login.Usuario.Nome;
                    usuario.Email = login.Usuario.Email;
                    usuario.CPF = login.Usuario.CPF;
                    usuario.DataNascimento = login.Usuario.DataNascimento;
                    usuario.Status = true;

                    login.TipoAcessoID = login.TipoAcesso.ID;
                    login.TipoAcesso = null;

                    dataContext.TBUsuarios.Add(usuario);
                    dataContext.SaveChanges();

                    login.UsuarioID = usuario.ID;
                    login.Usuario = null;

                    login.Senha = Criptografia.GeraHash(login.Senha);
                    dataContext.TBLogins.Add(login);
                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch(Exception e)
                {
                    return BadRequest(e);
                    //return BadRequest("Ocorreu um erro inesperado no cadastro da secretaria, por favor tente novamente");
                }
            }
        }

        public IActionResult Edit(int? ID)
        {
            if (ID.HasValue)
            {
                ViewBag.Acessos = listaAcessos;
                Login login = dataContext.TBLogins
                    .Include(u => u.Usuario)
                    .Include(ta => ta.TipoAcesso)
                    .FirstOrDefault(log => log.ID == ID);
                if (login != null)
                    return View(login);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Login login)
        {
            Login loginQuery = await dataContext.TBLogins
                .FindAsync(login.ID);
            Usuario usuario = await dataContext.TBUsuarios.FindAsync(loginQuery.UsuarioID);
            if (
              login.Usuario.Nome.Trim() == "" ||
              login.Usuario.Email.Trim() == "" ||
              login.Usuario.CPF.Trim() == "" ||
              login.LoginUsuario.Trim() == "" 
             )
                return BadRequest("Os dados informados são inválidos");
            if (!Validacoes.IsValidCPF(login.Usuario.CPF))
                return BadRequest("O CPF informado está inválido");
            if(loginQuery == null)
                return BadRequest("O usuário informado não existe");
            if (loginQuery != null && loginQuery.Usuario.CPF != usuario.CPF && ExisteUsuario(login))
                return BadRequest("O usuário informado já existe");
            try
            {
                usuario.Nome = login.Usuario.Nome;
                usuario.Email = login.Usuario.Email;
                usuario.DataNascimento = login.Usuario.DataNascimento;
                usuario.CPF = login.Usuario.CPF;

                login.TipoAcesso.ID = login.TipoAcesso.ID;
                loginQuery.LoginUsuario = login.LoginUsuario;

                await dataContext.SaveChangesAsync();
                return Ok(true);
            }
            catch
            {
                return BadRequest("Ocorreu um erro inesperado na atualização do usuário(a), por favor tente novamente");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int? ID)
        {
            if (ID.HasValue)
            {
                Login login = await dataContext.TBLogins.FindAsync(ID);
                Usuario usuario = await dataContext.TBUsuarios.FindAsync(login.UsuarioID);
                try
                {
                    usuario.Status = false;
                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch
                {
                    return BadRequest("Ocorreu um erro inesperado na exclusão da usuário(a), por favor tente novamente"); ;
                }
            }
            return BadRequest("ID inválido");
        }

    }
}
