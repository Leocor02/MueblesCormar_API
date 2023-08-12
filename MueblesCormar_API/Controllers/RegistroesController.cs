using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MueblesCormar_API.Attributes;
using MueblesCormar_API.Models;
using MueblesCormar_API.Models.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MueblesCormar_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class RegistroesController : ControllerBase
    {
        private readonly MueblesCormarContext _context;

        public RegistroesController(MueblesCormarContext context)
        {
            _context = context;
        }

        // GET: api/Registroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registro>>> GetRegistros()
        {
          if (_context.Registros == null)
          {
              return NotFound();
          }
            return await _context.Registros.ToListAsync();
        }

        // GET: api/Registroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registro>> GetRegistro(int id)
        {
          if (_context.Registros == null)
          {
              return NotFound();
          }
            var registro = await _context.Registros.FindAsync(id);

            if (registro == null)
            {
                return NotFound();
            }

            return registro;
        }
        
        // GET: api/Registroes/GetDataRegistro?idRegistro=1
        [HttpGet("GetDataRegistro")]
        public ActionResult<IEnumerable<RegistroDTO>> GetDataRegistro(int idRegistro)
        {
            //las consultas linq se parecen mucho a las normales que hemos hecho en T-SQL
            //una de las diferencias es que podemos usar una "tabla temporal" para almacenar
            //los resultados y luego usarla para llenar los atributos de un modelo o DTO

            var query = (from r in _context.Registros
                         where r.Idregistro == idRegistro
                         select new
                         {
                             Idregistro = r.Idregistro,
                             Fecha = r.Fecha,
                             Nota = r.Nota,
                             Idusuario = r.Idusuario
                         }).ToList();

            List<RegistroDTO> list = new List<RegistroDTO>();

            foreach (var registro in query)
            {
                RegistroDTO NewItem = new RegistroDTO();

                NewItem.Idregistro = registro.Idregistro;
                NewItem.Fecha = registro.Fecha;
                NewItem.Nota = registro.Nota;
                NewItem.Idusuario = registro.Idusuario;

                list.Add(NewItem);
            }

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // GET: api/Registroes/GetListaRegistro
        [HttpGet("GetListaRegistro")]
        public ActionResult<IEnumerable<RegistroDTO>> GetListaRegistro()
        {
            if (_context.Registros == null)
            {
                return NotFound();
            }

            var query = from r in _context.Registros
                        select new
                        {
                            Idregistro = r.Idregistro,
                            Fecha = r.Fecha,
                            Nota = r.Nota,
                            Idusuario = r.Idusuario
                        };

            List<RegistroDTO> RegistroLista = new List<RegistroDTO>();

            foreach (var registro in query)
            {
                RegistroLista.Add(
                    new RegistroDTO
                    {
                        Idregistro = registro.Idregistro,
                        Fecha = registro.Fecha,
                        Nota = registro.Nota,
                        Idusuario = registro.Idusuario

                    }

                    );
            }

            if (RegistroLista == null)
            {
                return NotFound();
            }

            return RegistroLista;
        }


        // PUT: api/Registroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistro(int id, Registro registro)
        {
            if (id != registro.Idregistro)
            {
                return BadRequest();
            }

            _context.Entry(registro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Registroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Registro>> PostRegistro(Registro registro)
        {
          if (_context.Registros == null)
          {
              return Problem("Entity set 'MueblesCormarContext.Registros'  is null.");
          }
            _context.Registros.Add(registro);
            await _context.SaveChangesAsync();

            ////var query = from dr in _context.DetalleRegistros
            //            select new
            //            {
            //                IddetalleRegistro = dr.IddetalleRegistro,
            //                Cantidad = dr.Cantidad,
            //                PrecioUnidad = dr.PrecioUnidad,
            //                Subtotal = dr.Subtotal,
            //                Impuestos = dr.Impuestos,
            //                Total = dr.Total,
            //                Idregistro = dr.Idregistro,
            //                Idproducto = dr.Idproducto
            //            };

            List<DetalleRegistro> detalleRegistroLista = new List<DetalleRegistro>();

            foreach (var detalleRegistro in registro.DetalleRegistros)
            {
                ////detalleRegistroLista.Add(
                //new DetalleRegistro()
                //{
                //    IddetalleRegistro = detalleRegistro.IddetalleRegistro,
                //    Cantidad = detalleRegistro.Cantidad,
                //    PrecioUnidad = detalleRegistro.PrecioUnidad,
                //    Subtotal = detalleRegistro.Subtotal,
                //    Impuestos = detalleRegistro.Impuestos,
                //    Total = detalleRegistro.Total,
                //    Idregistro = detalleRegistro.Idregistro,
                //    Idproducto = detalleRegistro.Idproducto
                //});

                PostDetalleRegistro(detalleRegistro);

            }

            //if (detalleRegistroLista == null)
            //{
            //    return NotFound();
            //}

            //hacer el for each y recorrer el detalle de registro y llamar al post del controlador del detalle de registro 

            return CreatedAtAction("GetRegistro", new { id = registro.Idregistro }, registro);
        }

        private bool PostDetalleRegistro(DetalleRegistro detalleRegistro)
        {
            if (_context.DetalleRegistros == null)
            {
                return false;
            }
            _context.DetalleRegistros.Add(detalleRegistro);
            _context.SaveChangesAsync();

            return true;
        }

        // DELETE: api/Registroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            if (_context.Registros == null)
            {
                return NotFound();
            }
            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
            {
                return NotFound();
            }

            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroExists(int id)
        {
            return (_context.Registros?.Any(e => e.Idregistro == id)).GetValueOrDefault();
        }
    }
}
