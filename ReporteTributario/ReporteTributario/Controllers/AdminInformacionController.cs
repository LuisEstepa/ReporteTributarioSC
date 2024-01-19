using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ReporteTributario.Extensions;
using ReporteTributario.Models;
using ReporteTributario.Models.Entities;
using ReporteTributario.Models.ViewModels;
using ReporteTributario.Servicios.Contrato;
using System.Data;
namespace ReporteTributario.Controllers
{
    public class AdminInformacionController : BaseController
    {
        private readonly DbTtributarioContext _dbcontext;
        private readonly IAdminInformacionService _Service;

        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";

        public int pageSize, skip, recordsTotal;

        public AdminInformacionController(DbTtributarioContext dbcontext, IAdminInformacionService Service)
        {
            _dbcontext = dbcontext;
            _Service = Service;
        }
        public IActionResult Index()
        {
            return Redirect("AdminInformacion/CargarInformacion");
        }

        public IActionResult CargarInformacion()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var Response = await TraerDatos();
            
            return Json(Response);
        }

        public async Task<List<InformacionBaseVM>> TraerDatos()
        {
            var _data = await _dbcontext.InformacionBase.ToListAsync();
            List<InformacionBaseVM> datos = new();
            if (_data != null && _data.Count > 0)
            {
                _data.ForEach(item =>
                {
                    datos.Add(new InformacionBaseVM()
                    {
                        IdImpuesto = item.IdImpuesto,
                        Impuesto = item.Impuesto,
                        Ciudad = item.Ciudad,
                        Departamento = item.Departamento,
                        FechaLimite = item.FechaLimite,
                        Responsable = item.Responsable,
                        Periodo = item.Periodo,
                        Periodicidad = item.Periodicidad                       

                    });
                });
            }
            return datos;
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

        public async Task<IActionResult> MostrarInformacion()
        {
            var datos = await ObtenerRegistrosAsync();
            //ViewBag.Datos = await ObtenerRegistrosAsync();
            return View(datos);
        }

        public async Task<IActionResult> MostrarInformacion2()
        {
            var datos = await ObtenerRegistrosAsync();
            //ViewBag.Datos = await ObtenerRegistrosAsync();
            return View(datos);
        }

        [HttpPost]
        public ActionResult Json()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;          

            //var lista = _Service.ObtenerRegistrosAsync();

            IQueryable<InformacionBaseVM> query = (from d in _dbcontext.InformacionBase
                       select new InformacionBaseVM
                       {
                           IdImpuesto = d.IdImpuesto,
                           Impuesto = d.Impuesto,
                           Ciudad = d.Ciudad,
                           Departamento = d.Departamento,
                           FechaLimite = d.FechaLimite,
                           Responsable = d.Responsable,
                           Periodo = d.Periodo,
                           Periodicidad = d.Periodicidad                           
                       }); 
            

            if(searchValue != "" )
                query = query.Where(s => s.Ciudad.Contains(searchValue));

            if(!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                if (sortColumnDir == "asc") {
                    query = query.OrderBy(l => l.Ciudad);
                }
                else
                {
                    query = query.OrderByDescending(l => l.Ciudad);
                }                     

            }

            recordsTotal = query.Count();

            var lst = query.Skip(skip).Take(pageSize).ToList();

            return Json(new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = lst
            });

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
                Periodo = x.Periodo,
                Periodicidad = x.Periodicidad

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

        public async Task<IActionResult> AgregarNuevo()
        {            
            return View();
        }
        public async Task<bool> AgregarRegistro(InformacionBase model)
        {
            try
            {
                await _dbcontext.InformacionBase.AddAsync(model);

                await _dbcontext.SaveChangesAsync();

                BasicNotification("Creado", NotificationType.Success, "Correcto!");

                return true;

            }
            catch (Exception ex)
            {
                // Manejar la excepción, como registrar el error o informar al usuario
                BasicNotification("Algo fallo!..", NotificationType.Error, ex.ToString());
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
            var LstEven = await _dbcontext.InformacionBase.Where(x => x.Vigente == true).ToListAsync();
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
