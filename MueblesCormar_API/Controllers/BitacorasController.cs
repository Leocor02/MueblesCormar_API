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

namespace MueblesCormar_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class BitacorasController : ControllerBase
    {
        private readonly MueblesCormarContext _context;

        public BitacorasController(MueblesCormarContext context)
        {
            _context = context;
        }

        // GET: api/Bitacoras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bitacora>>> GetBitacoras()
        {
          if (_context.Bitacoras == null)
          {
              return NotFound();
          }
            return await _context.Bitacoras.ToListAsync();
        }

        // GET: api/Bitacoras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bitacora>> GetBitacora(int id)
        {
          if (_context.Bitacoras == null)
          {
              return NotFound();
          }
            var bitacora = await _context.Bitacoras.FindAsync(id);

            if (bitacora == null)
            {
                return NotFound();
            }

            return bitacora;
        }

        // GET: api/Bitacoras/GetDataBitacora?idBitacora=7
        [HttpGet("GetDataBitacora")]
        public ActionResult<IEnumerable<Bitacora>> GetDataBitacora(int idBitacora)
        {
            //las consultas linq se parecen mucho a las normales que hemos hecho en T-SQL
            //una de las diferencias es que podemos usar una "tabla temporal" para almacenar
            //los resultados y luego usarla para llenar los atributos de un modelo o DTO

            var query = (from b in _context.Bitacoras
                         where b.Idbitacora == idBitacora
                         select new
                         {
                             Idbitacora = b.Idbitacora,
                             Accion = b.Accion,
                             fecha = b.Fecha,
                             Idusuario = b.Idusuario
                         }).ToList();

            List<Bitacora> list = new List<Bitacora>();

            foreach (var bitacora in query)
            {
                Bitacora NewItem = new Bitacora();

                NewItem.Idbitacora = bitacora.Idbitacora;
                NewItem.Accion = bitacora.Accion;
                NewItem.Fecha = bitacora.fecha;
                NewItem.Idusuario = bitacora.Idusuario;

                list.Add(NewItem);

            }

            if (list == null)
            {
                return NotFound();
            }

            return list;

        }

        // GET: api/Bitacoras/GetListaBitacora
        [HttpGet("GetListaBitacora")]
        public ActionResult<IEnumerable<Bitacora>> GetListaBitacora()
        {
            if (_context.Bitacoras == null)
            {
                return NotFound();
            }

            var query = from b in _context.Bitacoras
                        select new
                        {
                            Idbitacora = b.Idbitacora,
                            Accion = b.Accion,
                            Fecha = b.Fecha,
                            Idusuario = b.Idusuario
                        };

            List<Bitacora> BitacoraLista = new List<Bitacora>();

            foreach (var bitacora in query)
            {
                BitacoraLista.Add(
                    new Bitacora
                    {
                        Idbitacora = bitacora.Idbitacora,
                        Accion = bitacora.Accion,
                        Fecha = bitacora.Fecha,
                        Idusuario= bitacora.Idusuario
                    });
            }

            if (BitacoraLista == null)
            {
                return NotFound();
            }

            return BitacoraLista;
        }

        // PUT: api/Bitacoras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBitacora(int id, Bitacora bitacora)
        {
            if (id != bitacora.Idbitacora)
            {
                return BadRequest();
            }

            _context.Entry(bitacora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BitacoraExists(id))
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

        // POST: api/Bitacoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bitacora>> PostBitacora(Bitacora bitacora)
        {
          if (_context.Bitacoras == null)
          {
              return Problem("Entity set 'MueblesCormarContext.Bitacoras'  is null.");
          }
            _context.Bitacoras.Add(bitacora);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBitacora", new { id = bitacora.Idbitacora }, bitacora);
        }

        // DELETE: api/Bitacoras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBitacora(int id)
        {
            if (_context.Bitacoras == null)
            {
                return NotFound();
            }
            var bitacora = await _context.Bitacoras.FindAsync(id);
            if (bitacora == null)
            {
                return NotFound();
            }

            _context.Bitacoras.Remove(bitacora);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BitacoraExists(int id)
        {
            return (_context.Bitacoras?.Any(e => e.Idbitacora == id)).GetValueOrDefault();
        }
    }
}
