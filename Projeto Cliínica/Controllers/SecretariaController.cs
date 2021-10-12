using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class SecretariaController : Controller
    {
        private DataContext dataContext;
        public SecretariaController(DataContext dc)
        {
            dataContext = dc;
        }
        private bool ExisteSecretaria(Secretaria secretaria)
        {
            var result = (
                from user in dataContext.TBUsuarios
                join sec in dataContext.TBSecretaria on user.ID equals sec.UsuarioID
                where user.CPF == secretaria.Usuario.CPF && user.Status == true
                select new
                {
                    id = user.ID
                }
                ).ToList();
            return (result != null && result.Count() >= 0) ? true : false;
        }
        public IActionResult Index()
        {
            List<Secretaria> lista = dataContext.TBSecretaria
                .Include(s => s.Usuario)
                .Where(s => s.Usuario.Status == true)
                .ToList();
            return View(lista);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Secretaria secretaria)
        {
            if (secretaria == null)
                return RedirectToAction("Index");
            else
            {
                IQueryable<Secretaria> query = dataContext.TBSecretaria
                    .Include(s => s.Usuario)
                    .Where(s => s.Usuario.Status == true);
                if (secretaria.Usuario.Nome != null && secretaria.Usuario.Nome != "")
                    query = query.Where(
                        m => m.Usuario.Nome.ToUpper().Contains(secretaria.Usuario.Nome.ToUpper())
                        );

                if (secretaria.Usuario.Email != null && secretaria.Usuario.Email != "")
                    query = query.Where(
                        m => m.Usuario.Email.Contains(secretaria.Usuario.Email)
                        );

                if (secretaria.Usuario.DataNascimento != new DateTime(0001, 01, 01) && secretaria.Usuario.DataNascimento < DateTime.Today)
                    query = query.Where(m => m.Usuario.DataNascimento == secretaria.Usuario.DataNascimento);

                if (secretaria.Usuario.CPF != null && secretaria.Usuario.CPF != "")
                    query = query.Where(m => m.Usuario.CPF == secretaria.Usuario.CPF);

                return View(query.ToList());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Secretaria secretaria)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Os dados informados são inválidos");
            if (!ExisteSecretaria(secretaria))
                return BadRequest("O médico informado já existe");
            if (!Validacoes.IsValidCPF(secretaria.Usuario.CPF))
                return BadRequest("O CPF informado está inválido");
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nome = secretaria.Usuario.Nome;
                usuario.Email = secretaria.Usuario.Email;
                usuario.DataNascimento = secretaria.Usuario.DataNascimento;
                usuario.CPF = secretaria.Usuario.CPF;
                usuario.Status = true;
                dataContext.TBUsuarios.Add(usuario);
                dataContext.SaveChanges();

                secretaria.UsuarioID = usuario.ID;
                secretaria.Usuario = null;

                dataContext.TBSecretaria.Add(secretaria);
                await dataContext.SaveChangesAsync();
                return Ok(true);
            }
            catch(Exception e)
            {
                return BadRequest(e);
                //return BadRequest("Ocorreu um erro inesperado no cadastro da secretaria, por favor tente novamente");
            }
        }
        
        public IActionResult Edit(int? ID)
        {
            if (ID.HasValue)
            {
                Secretaria secretaria = dataContext.TBSecretaria
                    .Include(s => s.Usuario)
                    .FirstOrDefault(s => s.ID == ID);
                if (secretaria != null)
                    return View(secretaria);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Secretaria secretaria)
        {
            Secretaria secretariaQuery = await dataContext.TBSecretaria
                .FindAsync(secretaria.ID);
            Usuario usuario = await dataContext.TBUsuarios.FindAsync(secretariaQuery.UsuarioID);
            if (ModelState.IsValid == false)
                return BadRequest("Os dados informados são inválidos");
            if (!Validacoes.IsValidCPF(secretaria.Usuario.CPF))
                return BadRequest("O CPF informado está inválido");
            if (secretariaQuery == null)
                return BadRequest("A Secretaria informada não existe");
            if (secretariaQuery != null && secretariaQuery.Usuario.CPF != usuario.CPF && !ExisteSecretaria(secretaria))
                return BadRequest("A secretaria informada já existe");

            try
            {
                usuario.Nome = secretaria.Usuario.Nome;
                usuario.Email = secretaria.Usuario.Email;
                usuario.DataNascimento = secretaria.Usuario.DataNascimento;
                usuario.CPF = secretaria.Usuario.CPF;
                usuario.Status = true;

                await dataContext.SaveChangesAsync();
                return Ok(true);
            }
            catch
            {
                return BadRequest("Ocorreu um erro inesperado na atualização da secretaria, por favor tente novamente");
            }
        }
        
        [HttpDelete]
        public async Task<ActionResult> Delete(int? ID)
        {
            if (ID.HasValue)
            {
                Secretaria secretaria = await dataContext.TBSecretaria.FindAsync(ID);
                Usuario usuario = await dataContext.TBUsuarios.FindAsync(secretaria.UsuarioID);
                try
                {
                    usuario.Status = false;
                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch
                {
                    return BadRequest("Ocorreu um erro inesperado na exclusão da secretari, por favor tente novamente"); ;
                }
            }
            return BadRequest("ID inválido");
        }
    }
}
