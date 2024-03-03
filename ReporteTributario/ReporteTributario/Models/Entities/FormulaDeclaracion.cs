using System.ComponentModel.DataAnnotations.Schema;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;

namespace ReporteTributario.Models.Entities
{
    public class FormulaDeclaracion
    {
        public int AñoRadica { get; set; }
        public int MesRadica { get; set; }
        public int DiaRadica { get; set; }
        public int AñoDeclarado { get; set; }
        public int MesDeclarado { get; set; }
        public int AñoCorrecion { get; set; }
        public int MesCorrecion { get; set; }
        public int IdAgenteRetenedor { get; set; }
        public decimal IdRetencion { get; set; }


        [ForeignKey("IdAgenteRedendor")]
        public ICollection<AgenteRetenedor> AgenteRetenedors { get; set; }
        [ForeignKey("IdRetencion")]
        public ICollection<Retencion> Retencions { get; set; }
    }    
}
