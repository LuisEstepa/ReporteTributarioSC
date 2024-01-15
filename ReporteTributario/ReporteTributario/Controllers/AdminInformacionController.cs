using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ReporteTributario.Models;
using ReporteTributario.Models.Entities;
using ReporteTributario.Models.ViewModels;

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
                if (fila.Count() > 1)
                    lista.Add(new InformacionBaseVM
                    {

                        //IdImpuesto = Convert.ToInt32(fila.GetCell(0)),
                        Impuesto = fila.GetCell(1).ToString(),
                        Ciudad = fila.GetCell(2).ToString(),
                        Departamento = fila.GetCell(3).ToString(),
                        FechaLimite = Convert.ToDateTime(fila.GetCell(4).ToString()),
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
                if (fila.Count() > 1)
                    lista.Add(new InformacionBase
                    {
                        Impuesto = fila.GetCell(1).ToString(),
                        Ciudad = fila.GetCell(2).ToString(),
                        Departamento = fila.GetCell(3).ToString(),
                        FechaLimite = Convert.ToDateTime(fila.GetCell(4).ToString()),
                        Responsable = fila.GetCell(5).ToString(),
                        Periodo = fila.GetCell(6).ToString(),
                        Periodicidad = fila.GetCell(7).ToString(),
                        Vigente = true

                    });
            }

            _dbcontext.BulkInsert(lista);

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        public async Task<IActionResult> GestionarInformacion()
        {
            var datos = await ObtenerRegistrosAsync();
            //ViewBag.Datos = await ObtenerRegistrosAsync();
            return View(datos);
        }

        public async Task<List<InformacionBaseVM>> ObtenerRegistrosAsync()
        {
            List<InformacionBaseVM> Datos = new();

            Datos = await _dbcontext.InformacionBase.Select(x => new InformacionBaseVM
            {
                IdImpuesto = x.IdImpuesto,
                Impuesto = x.Impuesto,
                Ciudad = x.Ciudad,
                Departamento = x.Departamento,
                FechaLimite = x.FechaLimite,
                Responsable = x.Responsable,
                Periodo = x.Periodo

            }).ToListAsync();

            return Datos;
        }

        public async Task<InformacionBase> ObtenerRegistro(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id debe ser mayor que 0.");
            }
            var registro = await _dbcontext.InformacionBase.FindAsync(id);

            return registro;
        }

        public async Task<bool> AgregarRegistro(InformacionBase model)
        {
            try
            {
                await _dbcontext.InformacionBase.AddAsync(model);

                await _dbcontext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                // Manejar la excepción, como registrar el error o informar al usuario
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> ActualizarRegistro(InformacionBase model)
        {
            InformacionBase modelExistente = await ObtenerRegistro(model.IdImpuesto);
            try
            {
                modelExistente.Impuesto = model.Impuesto;
                modelExistente.Ciudad = model.Ciudad;
                modelExistente.Departamento = model.Departamento;
                modelExistente.FechaLimite = model.FechaLimite;
                modelExistente.Responsable = model.Responsable;
                modelExistente.Periodo = model.Periodo;
                modelExistente.Periodicidad = model.Periodicidad;

                await _dbcontext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                // Manejar la excepción, como registrar el error o informar al usuario
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> EliminarRegistro(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El id debe ser mayor que 0.");
            }
            try
            {
                InformacionBase model = await ObtenerRegistro(id);

                _dbcontext.InformacionBase.Remove(model);

                await _dbcontext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Manejar la excepción, como registrar el error o informar al usuario
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> EliminarRegistroLogico(InformacionBase model)
        {
            InformacionBase modelExistente = await ObtenerRegistro(model.IdImpuesto);
            try
            {
                modelExistente.Vigente = model.Vigente;

                await _dbcontext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                // Manejar la excepción, como registrar el error o informar al usuario
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #region Calendario
        [HttpGet]
        public async Task<IActionResult> CalendarioImpuesto()
        {
            return await Task.FromResult(View());
        }
        [HttpGet]
        public async Task<List<VMEventos>> GetAllEventos()
        {
            List<VMEventos> eventos = new List<VMEventos>();
            var LstEven = await _dbcontext.InformacionBase.Where(x=>x.Vigente==true).ToListAsync();
            foreach (var item in LstEven)
            {
                VMEventos vMEventos = new VMEventos();
                vMEventos.id = item.IdImpuesto;
                vMEventos.title = item.Impuesto;
                vMEventos.start = item.FechaLimite.ToString("yyyy-MM-dd HH:mm:ss");
                //vMEventos.end = item.FechaLimite.FormatWith("yyyy-MM-dd HH:mm:ss");
                eventos.Add(vMEventos);
            }
            return eventos;
        }
        #endregion
    }
}
