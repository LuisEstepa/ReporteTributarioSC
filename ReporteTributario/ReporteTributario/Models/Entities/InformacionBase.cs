﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReporteTributario.Models.Entities
{
    [Table("Informacion")]
    public class InformacionBase
    {
        public int IdImpuesto { get; set; }
        public string Impuesto { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public DateTime FechaLimite { get; set; }
        public string Responsable { get; set; }
        public string Periodo { get; set; }
        public string Periodicidad { get; set; }
        //[DefaultValue(true)]
        public bool Vigente { get; set; }
    }
}
