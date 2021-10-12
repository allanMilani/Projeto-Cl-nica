using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_Cliínica.Data;
using Projeto_Cliínica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Controllers
{
    [Authorize]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AgendamentoController : Controller
    {
        private DataContext dataContext;
        public IEnumerable<SelectListItem> listaMedico;
        public IEnumerable<SelectListItem> listaPacientes;
        public AgendamentoController(DataContext dc)
        {
            dataContext = dc;
            listaMedico = dataContext.TBMedicos
                .Include(m => m.Usuario)
                .Where(m => m.Usuario.Status == true).ToList()
                .Select(
                    m => new SelectListItem()
                    {
                        Text = m.Usuario.Nome,
                        Value = m.ID.ToString()
                    }
                ).ToList();
            listaPacientes = dataContext.TBPaciente
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.Status == true).ToList()
                .Select(
                    p => new SelectListItem()
                    {
                        Text = p.Usuario.Nome,
                        Value = p.ID.ToString()
                    }
                ).ToList();
        }
        
        public bool ExisteConsulta(Agendamento agendamento)
        {
            var result = (
                from age in dataContext.TBAgendamentos
                join med in dataContext.TBMedicos on age.MedicoID equals med.ID
                where age.DataHoraConsulta == agendamento.DataHoraConsulta && age.Status == true
                select new
                {
                    id = age.ID
                }
                ).ToList();
            return (result != null && result.Count() >= 0) ? true : false;
        }
        
        public IActionResult Index()
        {
            List<Agendamento> lista = dataContext.TBAgendamentos
                .Include(p => p.Paciente.Usuario)
                .Include(m => m.Medico.Usuario)
                .Where(age => age.Status == true)
                .ToList();
            return View(lista);
        }

        public IActionResult Create()
        {
            ViewBag.Pacientes = listaPacientes;
            ViewBag.Medicos = listaMedico;
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(Agendamento agendamento)
        {
            if (agendamento.DataHoraConsulta.Date < DateTime.Today.Date)
                return BadRequest("A data da consulta está inválida");
            if (!ExisteConsulta(agendamento))
                return BadRequest("A data e hora informada já se encontra preenchida com o médico");
            try
            {
                agendamento.MedicoID = agendamento.Medico.ID;
                agendamento.Medico = null;

                agendamento.PacienteID = agendamento.Paciente.ID;
                agendamento.Paciente = null;

                agendamento.Status = true;

                dataContext.TBAgendamentos.Add(agendamento);
                await dataContext.SaveChangesAsync();
                return Ok(true);

            }
            catch
            {
                return BadRequest("Ocorreu um erro inesperado no cadastro do agendamento, por favor tente novamente"); ;
            }
        }
    }
}
