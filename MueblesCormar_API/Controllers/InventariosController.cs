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
    [ApiKey]
    public class InventariosController : ControllerBase
    {
        private readonly MueblesCormarContext _context;

        public InventariosController(MueblesCormarContext context)
        {
            _context = context;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetInventarios()
        {
          if (_context.Inventarios == null)
          {
              return NotFound();
          }
            return await _context.Inventarios.ToListAsync();
        }

        // GET: api/Inventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventario>> GetInventario(int id)
        {
          if (_context.Inventarios == null)
          {
              return NotFound();
          }
            var inventario = await _context.Inventarios.FindAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return inventario;
        }

        
        // GET: api/Inventarios/GetListaProducto
        [HttpGet("GetListaProducto")]
        public ActionResult<IEnumerable<InventarioDTO>> GetListaProducto()
        {
            if (_context.Inventarios == null)
            {
                return NotFound();
            }

            var query = from i in _context.Inventarios
                        select new
                        {
                            Idproducto = i.Idproducto,
                            Nombre = i.Nombre,
                            Cantidad = i.Cantidad,
                            Descripcion = i.Descripcion,
                            ImagenProducto = i.ImagenProducto,
                            PrecioUnidad = i.PrecioUnidad
                        };

            List<InventarioDTO> inventarioLista = new List<InventarioDTO>();

            foreach (var producto in query)
            {
                inventarioLista.Add(
                    new InventarioDTO
                    {
                        Idproducto = producto.Idproducto,
                        Nombre = producto.Nombre,
                        Cantidad = producto.Cantidad,
                        Descripcion = producto.Descripcion,
                        ImagenProducto = producto.ImagenProducto,
                        PrecioUnidad = producto.PrecioUnidad
                    }

                    );
            }

            if (inventarioLista == null)
            {
                return NotFound();
            }

            return inventarioLista;
        }

        // PUT: api/Inventarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario(int id, Inventario inventario)
        {
            if (id != inventario.Idproducto)
            {
                return BadRequest();
            }

            _context.Entry(inventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioExists(id))
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

        // POST: api/Inventarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
          if (_context.Inventarios == null)
          {
              return Problem("Entity set 'MueblesCormarContext.Inventarios'  is null.");
          }
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario", new { id = inventario.Idproducto }, inventario);
        }

        // DELETE: api/Inventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            if (_context.Inventarios == null)
            {
                return NotFound();
            }
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioExists(int id)
        {
            return (_context.Inventarios?.Any(e => e.Idproducto == id)).GetValueOrDefault();
        }
    }
}
