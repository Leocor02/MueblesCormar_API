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
    public class ProveedorInventariosController : ControllerBase
    {
        private readonly MueblesCormarContext _context;

        public ProveedorInventariosController(MueblesCormarContext context)
        {
            _context = context;
        }

        // GET: api/ProveedorInventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorInventario>>> GetProveedorInventarios()
        {
          if (_context.ProveedorInventarios == null)
          {
              return NotFound();
          }
            return await _context.ProveedorInventarios.ToListAsync();
        }

        // GET: api/ProveedorInventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorInventario>> GetProveedorInventario(int id)
        {
          if (_context.ProveedorInventarios == null)
          {
              return NotFound();
          }
            var proveedorInventario = await _context.ProveedorInventarios.FindAsync(id);

            if (proveedorInventario == null)
            {
                return NotFound();
            }

            return proveedorInventario;
        }

        // GET: api/ProveedorInventarios/GetDataProveedorInventario?idProveedorInventario=1
        [HttpGet("GetDataProveedorInventario")]
        public ActionResult<IEnumerable<ProveedorInventarioDTO>> GetDataProveedorInventario(int idProveedorInventario)
        {
            if (_context.ProveedorInventarios == null)
            {
                return NotFound();
            }
            //las consultas linq se parecen mucho a las normales que hemos hecho en T-SQL
            //una de las diferencias es que podemos usar una "tabla temporal" para almacenar
            //los resultados y luego usarla para llenar los atributos de un modelo o DTO

            var query = (from pi in _context.ProveedorInventarios
                         join p in _context.Proveedors on pi.Idproveedor equals p.Idproveedor
                         join i in _context.Inventarios on pi.Idproducto equals i.Idproducto
                         where pi.IdproveedorInventario == idProveedorInventario
                         select new
                         {
                             IdproveedorInventario = pi.IdproveedorInventario,
                             Idproveedor = p.Idproveedor,
                             Idproducto = i.Idproducto,
                             NombreProveedor = p.Nombre,
                             NombreProducto = i.Nombre
                         }).ToList();

            List<ProveedorInventarioDTO> list = new List<ProveedorInventarioDTO>();

            foreach (var proveedorInventario in query)
            {
                ProveedorInventarioDTO NewItem = new ProveedorInventarioDTO();

                NewItem.IdproveedorInventario = proveedorInventario.IdproveedorInventario;
                NewItem.Idproveedor = proveedorInventario.Idproveedor;
                NewItem.Idproducto = proveedorInventario.Idproducto;
                NewItem.NombreProveedor = proveedorInventario.NombreProveedor;
                NewItem.NombreProducto = proveedorInventario.NombreProducto;

                list.Add(NewItem);

            }

            if (list == null)
            {
                return NotFound();
            }

            return list;

        }

        // GET: api/ProveedorInventarios/GetListaProveedorInventario
        [HttpGet("GetListaProveedorInventario")]
        public ActionResult<IEnumerable<ProveedorInventarioDTO>> GetListaProveedorInventario()
        {
            if (_context.ProveedorInventarios == null)
            {
                return NotFound();
            }

            var query = from pi in _context.ProveedorInventarios
                        join p in _context.Proveedors on pi.Idproveedor equals p.Idproveedor
                        join i in _context.Inventarios on pi.Idproducto equals i.Idproducto
                        select new
                        {
                            IdproveedorInventario = pi.IdproveedorInventario,
                            Idproveedor = p.Idproveedor,
                            Idproducto = i.Idproducto,
                            NombreProveedor = p.Nombre,
                            NombreProducto = i.Nombre
                        };

            List<ProveedorInventarioDTO> ProveedorInventarioLista = new List<ProveedorInventarioDTO>();

            foreach (var proveedorInventario in query)
            {
                ProveedorInventarioLista.Add(
                    new ProveedorInventarioDTO
                    {
                        IdproveedorInventario = proveedorInventario.IdproveedorInventario,
                        Idproveedor = proveedorInventario.Idproveedor,
                        Idproducto = proveedorInventario.Idproducto,
                        NombreProveedor = proveedorInventario.NombreProveedor,
                        NombreProducto = proveedorInventario.NombreProducto

                    }

                    );
            }

            if (ProveedorInventarioLista == null)
            {
                return NotFound();
            }

            return ProveedorInventarioLista;
        }

        // PUT: api/ProveedorInventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedorInventario(int id, ProveedorInventario proveedorInventario)
        {
            if (id != proveedorInventario.IdproveedorInventario)
            {
                return BadRequest();
            }

            _context.Entry(proveedorInventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorInventarioExists(id))
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

        // POST: api/ProveedorInventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProveedorInventario>> PostProveedorInventario(ProveedorInventario proveedorInventario)
        {
          if (_context.ProveedorInventarios == null)
          {
              return Problem("Entity set 'MueblesCormarContext.ProveedorInventarios'  is null.");
          }
            _context.ProveedorInventarios.Add(proveedorInventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedorInventario", new { id = proveedorInventario.IdproveedorInventario }, proveedorInventario);
        }

        // DELETE: api/ProveedorInventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedorInventario(int id)
        {
            if (_context.ProveedorInventarios == null)
            {
                return NotFound();
            }
            var proveedorInventario = await _context.ProveedorInventarios.FindAsync(id);
            if (proveedorInventario == null)
            {
                return NotFound();
            }

            _context.ProveedorInventarios.Remove(proveedorInventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorInventarioExists(int id)
        {
            return (_context.ProveedorInventarios?.Any(e => e.IdproveedorInventario == id)).GetValueOrDefault();
        }
    }
}
