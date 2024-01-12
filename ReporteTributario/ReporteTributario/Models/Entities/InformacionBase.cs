﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ReporteTributario.Models.Entities
{
    [Table("Informacion")]
    public class InformacionBase
    {
        public string IdImpuesto { get; set; }
        public string Impuesto { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string FechaLimite { get; set; }
        public string Responsable { get; set; }
        public string Periodo { get; set; }
        public string Periodicidad { get; set; }
    }
}