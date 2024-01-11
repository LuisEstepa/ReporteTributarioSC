using Microsoft.AspNetCore.Mvc;

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using ReporteTributario.Models.ViewModels;
using ReporteTributario.Models.Entities;
using ReporteTributario.Models;
using EFCore.BulkExtensions;

namespace ReporteTributario.Controllers
{
    public class AdminInformacionController : Controller
    {
        private readonly DbTtributarioContext _dbcontext;

        public AdminInformacionController(DbTtributarioContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            return Redirect("AdminInformacion/CargarInformacion");
        }

        public IActionResult CargarInformacion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MostrarDatos([FromForm] IFormFile ArchivoExcel)
        {
            Stream stream = ArchivoExcel.OpenReadStream();

            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ISheet HojaExcel = MiExcel.GetSheetAt(0);

            int cantidadFilas = HojaExcel.LastRowNum;

            List<InformacionBaseVM> lista = new();

            for (int i = 1; i <= cantidadFilas; i++)
            {

                IRow fila = HojaExcel.GetRow(i);

                lista.Add(new InformacionBaseVM
                {

                    IdImpuesto = fila.GetCell(0).ToString(),
                    Impuesto = fila.GetCell(1).ToString(),
                    Ciudad = fila.GetCell(2).ToString(),
                    Departamento = fila.GetCell(3).ToString(),
                    FechaLimite = fila.GetCell(4).ToString(),
                    Responsable = fila.GetCell(5).ToString(),
                    Periodo = fila.GetCell(6).ToString(),
                    Periodicidad = fila.GetCell(7).ToString()

                });
            }

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost]
        public IActionResult EnviarDatos([FromForm] IFormFile ArchivoExcel)
        {
            Stream stream = ArchivoExcel.OpenReadStream();

            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ISheet HojaExcel = MiExcel.GetSheetAt(0);
            int cantidadFilas = HojaExcel.LastRowNum;

            List<InformacionBase> lista = new();

            for (int i = 1; i <= cantidadFilas; i++)
            {

                IRow fila = HojaExcel.GetRow(i);

                lista.Add(new InformacionBase
                {
                    IdImpuesto = fila.GetCell(0).ToString(),
                    Impuesto = fila.GetCell(1).ToString(),
                    Ciudad = fila.GetCell(2).ToString(),
                    Departamento = fila.GetCell(3).ToString(),
                    FechaLimite = fila.GetCell(4).ToString(),
                    Responsable = fila.GetCell(5).ToString(),
                    Periodo = fila.GetCell(6).ToString(),
                    Periodicidad = fila.GetCell(7).ToString()

                });
            }

            _dbcontext.BulkInsert(lista);

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }
    }
}
