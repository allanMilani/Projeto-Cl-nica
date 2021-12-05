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
    [Authorize(Roles = "A,S,M")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class PacienteController : Controller
    {
        private DataContext dataContext;
        public List<SelectListItem> sexo = new List<SelectListItem>
        {
            new SelectListItem(){ Text = "Masculino", Value = "M" },
            new SelectListItem(){ Text = "Feminino", Value = "F" },
            new SelectListItem(){ Text = "Prefiro não informar", Value = "O" }
        };
        public PacienteController(DataContext dc)
        {
            dataContext = dc;
        }

        public bool ExistePaciente(Paciente paciente)
        {
            var result = (
                    from user in dataContext.TBUsuarios
                    join pac in dataContext.TBPaciente on user.ID equals pac.UsuarioID
                    where user.CPF == pac.Usuario.CPF && user.Status == true
                    select new
                    {
                        id = user.ID
                    }
                ).ToList();
            return (result != null && result.Count() >= 0) ? true : false;
        }

        public IActionResult Index()
        {
            List<Paciente> lista = dataContext.TBPaciente
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.Status == true)
                .ToList();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Paciente paciente)
        {
            if (paciente == null)
                return RedirectToAction("Index");
            else
            {
                IQueryable<Paciente> query = dataContext.TBPaciente
                    .Include(p => p.Usuario)
                    .Where(p => p.Usuario.Status == true);

                if (paciente.Usuario.Nome != null && paciente.Usuario.Nome != "")
                    query = query.Where(
                        p => p.Usuario.Nome.ToUpper().Contains(paciente.Usuario.Nome.ToUpper())
                        );
                if (paciente.Usuario.Email != null && paciente.Usuario.Email != "")
                    query = query.Where(
                        p => p.Usuario.Email.Contains(paciente.Usuario.Email)
                        );
                if (paciente.Usuario.DataNascimento != new DateTime(0001, 01, 01) && paciente.Usuario.DataNascimento < DateTime.Today)
                    query = query.Where(p => p.Usuario.DataNascimento == paciente.Usuario.DataNascimento);
                if (paciente.Usuario.CPF != null && paciente.Usuario.CPF != "")
                    query = query.Where(p => p.Usuario.CPF == paciente.Usuario.CPF);
                if (paciente.Telefone != null && paciente.Telefone != "")
                    query = query.Where(p => p.Telefone == paciente.Telefone);
                if (paciente.Profissao != null && paciente.Profissao != "")
                    query = query.Where(
                        p => p.Profissao.ToUpper().Contains(paciente.Profissao.ToUpper())
                        );
                if (paciente.Endereco != null && paciente.Endereco != "")
                    query = query.Where(
                        p => p.Endereco.ToUpper().Contains(paciente.Endereco.ToUpper())
                        );
                if (paciente.Sexo != null && paciente.Sexo != "")
                    query = query.Where(p => p.Sexo == paciente.Sexo);

                return View(query.ToList());
            }
        }

        public IActionResult Create()
        {
            ViewBag.Sexo = sexo;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Paciente paciente)
        {
            if (ModelState.IsValid == false)
                return BadRequest("Os dados informados são inválidos");
            if (!ExistePaciente(paciente))
                return BadRequest("O paciente informado já existe");
            if (!Validacoes.IsValidCPF(paciente.Usuario.CPF))
                return BadRequest("O CPF informado está inválido");
            try
            {
                Usuario usuario = new Usuario();
                usuario.Nome = paciente.Usuario.Nome;
                usuario.Email = paciente.Usuario.Email;
                usuario.DataNascimento = paciente.Usuario.DataNascimento;
                usuario.CPF = paciente.Usuario.CPF;
                usuario.Status = true;
                dataContext.TBUsuarios.Add(usuario);
                dataContext.SaveChanges();

                paciente.UsuarioID = usuario.ID;
                paciente.Usuario = null;

                dataContext.TBPaciente.Add(paciente);
                await dataContext.SaveChangesAsync();
                return Ok(true);
            }
            catch
            {
                return BadRequest("Ocorreu um erro inesperado no cadastro do paciente, por favor tente novamente");
            }
                    
        }
        
        public IActionResult Edit(int? ID)
        {
            ViewBag.Sexo = sexo;
            if (ID.HasValue)
            {
                Paciente paciente = dataContext.TBPaciente
                    .Include(p => p.Usuario)
                    .FirstOrDefault(p => p.ID == ID);
                if (paciente != null)
                    return View(paciente);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Paciente paciente)
        {
            Paciente pacienteQuery = await dataContext.TBPaciente
                .FindAsync(paciente.ID);
            Usuario usuario = await dataContext.TBUsuarios.FindAsync(pacienteQuery.UsuarioID);
            if (ModelState.IsValid == false)
                return BadRequest("Os dados informados são inválidos");
            if (!Validacoes.IsValidCPF(paciente.Usuario.CPF))
                return BadRequest("O CPF informado está inválido");
            if (pacienteQuery == null)
                return BadRequest("O paciente informado não existe");
            if (pacienteQuery != null && paciente.Usuario.CPF != usuario.CPF && !ExistePaciente(paciente))
                return BadRequest("O paciente informado já existe");
            try
            {
                usuario.Nome = paciente.Usuario.Nome;
                usuario.Email = paciente.Usuario.Email;
                usuario.DataNascimento = paciente.Usuario.DataNascimento;
                usuario.CPF = paciente.Usuario.CPF;
                usuario.Status = true;

                pacienteQuery.Telefone = paciente.Telefone;
                pacienteQuery.Endereco = paciente.Endereco;
                pacienteQuery.Profissao = paciente.Profissao;
                pacienteQuery.Sexo = paciente.Sexo;
                pacienteQuery.Observacao = paciente.Observacao;

                await dataContext.SaveChangesAsync();
                return Ok(true);

            }
            catch
            {
                return BadRequest("Ocorreu um erro inesperado na atualização do paciente, por favor tente novamente");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int? ID)
        {
            if (ID.HasValue)
            {
                Paciente paciente = await dataContext.TBPaciente.FindAsync(ID);
                Usuario usuario = await dataContext.TBUsuarios.FindAsync(paciente.UsuarioID);
                try{
                    usuario.Status = false;
                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch {
                    return BadRequest("Ocorreu um erro inesperado na exclusão do paciente, por favor tente novamente");
                }
            }

            return BadRequest("Id inválido");
        }
    }
}
