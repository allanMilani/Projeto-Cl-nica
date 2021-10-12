using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public string Observacao { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }

        [DisplayName("Secretaria")]
        public virtual Secretaria Secretaria { get; set; }
        public int? SecretariaID { get; set; }
        [DisplayName("Motivo Cancelamento")]
        public string MotivoCancelamento { get; set; }
        [DisplayName("Data do Cancelamento")]
        [DataType(DataType.Date)]
        public DateTime DataCancelamento { get; set; }
    }
}
