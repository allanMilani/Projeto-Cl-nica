using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Cliínica.Models
{
    public class Agendamento
    {
        [DisplayName("Identificar Agendamento")]
        public int ID { get; set; }
        [DisplayName("Paciente")]
        public virtual Paciente Paciente { get; set; }
        public int? PacienteID { get; set; }
        [DisplayName("Médico")]
        public virtual Medico Medico { get; set; }
        public int? MedicoID { get; set; }
        [DisplayName("Data e Hora Consulta")]
        [Required(ErrorMessage = "Por favor informe a data e a hora da consulta")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraConsulta { get; set; }

        [DisplayName("Observação")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }

        [DisplayName("Secretaria")]
        public virtual Login LoginCriador { get; set; }
        public int? LoginCriadorID { get; set; }
        [DisplayName("Motivo Cancelamento")]
        public string MotivoCancelamento { get; set; }
         [DisplayName("Consuta cancelada")]
        public bool IsCancel { get; set; }
        [DisplayName("Data do Cancelamento")]
        [DataType(DataType.Date)]
        public DateTime DataCancelamento { get; set; }
        [DisplayName("Profissional que cancelou a consulta")]
        public virtual Login QuemCancelou { get; set; }
        public int? QuemCancelouID { get; set; }
        [DisplayName("Hora inicio do atendimento")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [DisplayName("Hora final do atendimento")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFim { get; set; }
        [DisplayName("Anotações")]
        [DataType(DataType.MultilineText)]
        public string AnotacoesMedico { get; set; }
        
        [DisplayName("Chave")]
        [DataType(DataType.Password)]
        public string Chave { get; set; }
    }
}
