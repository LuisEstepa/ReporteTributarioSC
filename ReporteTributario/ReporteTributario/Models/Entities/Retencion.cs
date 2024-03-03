using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReporteTributario.Models.Entities
{
    public class Retencion
    {
        [Key]
        public decimal IdRetencion { get; set; }
        public decimal BaseRetenciones { get; set; }
        public decimal ActividadIndustrial { get; set; }
        public decimal ActividadComercial { get; set; }
        public decimal ActividadFinanciera { get; set; }
        public decimal ActividadServicios { get; set; }
        public decimal TotalRetencionesPracticadas { get; set; }
        public decimal Sanciones { get; set; }
        public decimal TotalCargo { get; set; }
        public decimal InteresesMora { get; set; }
        public decimal TotalPagar { get; set; }
        
        public int IdImpuesto { get; set; }

        [ForeignKey("IdImpuesto")]
        public ICollection<InformacionBase> InformacionBases { get; set; }
    }
}
