namespace ReporteTributario.Models.ViewModels
{
    public class InformacionBaseVM
    {
        public int IdImpuesto { get; set; }
        public string Impuesto { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string FechaLimite { get; set; }
        public string Responsable { get; set; }
        public string Periodo { get; set; }
        public string Periodicidad { get; set; }
        public bool Vigente { get; set; }
    }
}
