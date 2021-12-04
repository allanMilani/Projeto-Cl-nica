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
                .Where(age => age.Status == true && age.Medico.Usuario.Status == true && age.Paciente.Usuario.Status == true && age.DataHoraConsulta.Month == DateTime.Today.Month)
                .ToList();
            return View(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Agendamento agendamento)
        {
            if (agendamento == null)
                return RedirectToAction("Index");
            else
            {
                IQueryable<Agendamento> query = dataContext.TBAgendamentos
                  .Include(um => um.Medico.Usuario)
                  .Include(up => up.Paciente.Usuario)
                  .Where(u => u.Medico.Usuario.Status == true && u.Paciente.Usuario.Status == true && u.Status == true);

                if (agendamento.Medico.Usuario.Nome != null && agendamento.Medico.Usuario.Nome != "")
                    query = query.Where(
                        m => m.Medico.Usuario.Nome.ToUpper().Contains(agendamento.Medico.Usuario.Nome.ToUpper())
                        );
                if (agendamento.Paciente.Usuario.Nome != null && agendamento.Paciente.Usuario.Nome != "")
                    query = query.Where(
                        m => m.Paciente.Usuario.Nome.ToUpper().Contains(agendamento.Paciente.Usuario.Nome.ToUpper())
                        );
                if (agendamento.DataHoraConsulta != new DateTime(0001, 01, 01))
                    query = query.Where(m => m.DataHoraConsulta.Date == agendamento.DataHoraConsulta.Date);

                return View(query.ToList());
            }
        }

        public IActionResult Create(int? ID, string Chave = "")
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
                agendamento.LoginCriadorID = Convert.ToInt32(User.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.Sid).Value);
                agendamento.Status = true;

                dataContext.TBAgendamentos.Add(agendamento);
                await dataContext.SaveChangesAsync();
                return Ok(true);

            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro inesperado no cadastro do agendamento, por favor tente novamente"); ;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Cancel(int? ID, string motivo)
        {
            if (ID.HasValue && motivo.Trim() != "")
            {
                Agendamento agendamento = await dataContext.TBAgendamentos.FindAsync(ID);
                try
                {
                    agendamento.DataCancelamento = DateTime.Today;
                    agendamento.MotivoCancelamento = motivo;
                    agendamento.IsCancel = true;
                    agendamento.QuemCancelouID = Convert.ToInt32(User.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.Sid).Value);
                    await dataContext.SaveChangesAsync();
                    return Ok(true);
                }
                catch
                {
                    return BadRequest("Ocorreu um erro inesperado no cancelamento da consulta, por favor tente novamente"); ;
                }
            }
            return BadRequest("Dados inválidos");
        }

        public IActionResult Anotacoes(int? ID, string Chave = "")
        {
            if (ID.HasValue)
            {
                if (Chave.Trim() != "")
                {
                    Agendamento agendamento = dataContext.TBAgendamentos
                            .FirstOrDefault(a => a.ID == ID);
                    if (Criptografia.GeraHash(Chave) == agendamento.Chave)
                    {
                        if (agendamento != null)
                        {
                            agendamento.AnotacoesMedico = Criptografia.Descriptografar(agendamento.AnotacoesMedico, Chave);
                          
                            return View(agendamento);
                        }
                    }
                }
                else
                {
                    Agendamento agendamento = dataContext.TBAgendamentos
                            .FirstOrDefault(a => a.ID == ID);
                    if (agendamento != null)
                    {
                       
                        return View(agendamento);
                    }

                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Anotacoes(int? ID, string AnotacoesMedico, TimeSpan HoraInicio, TimeSpan HoraFim, string Chave)
        {
            if (ID.HasValue)
            {
                if (AnotacoesMedico.Trim() != "")
                {
                    Agendamento agendamentoQuery = await dataContext.TBAgendamentos
                        .FindAsync(ID);
                    if(agendamentoQuery.Chave != null && agendamentoQuery.Chave.Trim() != "")
                    {
                        string ChaveHashe = Criptografia.GeraHash(Chave);
                        if (ChaveHashe != agendamentoQuery.Chave)
                        {
                            return BadRequest("A Chave deve ser igual a cadastrada anteriormente");
                        }
                    }
                    if(HoraFim > HoraInicio)
                    {
                        return BadRequest("Hora da consulta inconsitente");
                    }
                        try
                    {
                        agendamentoQuery.HoraInicio = HoraInicio;
                        agendamentoQuery.HoraFim = HoraFim;
                        agendamentoQuery.AnotacoesMedico = Criptografia.Cryptografar(AnotacoesMedico, Chave);
                        agendamentoQuery.Chave = Criptografia.GeraHash(Chave);
                        await dataContext.SaveChangesAsync();
                        return Ok(true);
                    }
                    catch
                    {
                        return BadRequest("Ocorreu um erro inesperado na atualização da anotação, por favor tente novamente");
                    }
                }
            }
            return BadRequest("ID inválido");
        }

        public IActionResult Visualizar(int? ID, string Chave)
        {
            if (ID.HasValue && Chave.Trim() != "")
            {
                Agendamento agendamento = dataContext.TBAgendamentos
                        .FirstOrDefault(a => a.ID == ID);
                if (Criptografia.GeraHash(Chave) == agendamento.Chave)
                {
                    if (agendamento != null)
                    {
                        agendamento.AnotacoesMedico = Criptografia.Descriptografar(agendamento.AnotacoesMedico, Chave);
                        return View(agendamento);
                    }
                }
                return NoContent();
            }
            return NoContent();
        }

        [HttpPost]
        public ActionResult ValidaChave(int? ID, string Chave)
        {

            if (ID.HasValue && Chave.Trim() != "")
            {
                Agendamento agendamento = dataContext.TBAgendamentos
                        .FirstOrDefault(a => a.ID == ID);
                if (agendamento != null && agendamento.Chave.Trim() != null && agendamento.AnotacoesMedico.Trim() != null)
                {
                    string ChaveHashe = Criptografia.GeraHash(Chave);
                    if (ChaveHashe == agendamento.Chave)
                    {
                        return Ok(true);
                    }
                }
                else
                {
                    return BadRequest("Anotação inexistente");
                }
            }
            return BadRequest("Identificar ou Chave inválido");
        }

        [HttpPost]
        public ActionResult ExisteAnotacao(int? ID)
        {
            if (ID.HasValue)
            {
                Agendamento agendamento = dataContext.TBAgendamentos
                        .FirstOrDefault(a => a.ID == ID);
                if (agendamento.AnotacoesMedico != null && agendamento.AnotacoesMedico.Trim() != "")
                {
                    return Ok(true);
                }
            }
            return BadRequest("Identificar inválido");
        }

    }
}
