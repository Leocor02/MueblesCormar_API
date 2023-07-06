using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MueblesCormar_API.Attributes;
using MueblesCormar_API.Models;

namespace MueblesCormar_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class DetalleRegistroesController : ControllerBase
    {
        private readonly MueblesCormarContext _context;

        public DetalleRegistroesController(MueblesCormarContext context)
        {
            _context = context;
        }

        // GET: api/DetalleRegistroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRegistro>>> GetDetalleRegistros()
        {
          if (_context.DetalleRegistros == null)
          {
              return NotFound();
          }
            return await _context.DetalleRegistros.ToListAsync();
        }

        // GET: api/DetalleRegistroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleRegistro>> GetDetalleRegistro(int id)
        {
          if (_context.DetalleRegistros == null)
          {
              return NotFound();
          }
            var detalleRegistro = await _context.DetalleRegistros.FindAsync(id);

            if (detalleRegistro == null)
            {
                return NotFound();
            }

            return detalleRegistro;
        }

        // PUT: api/DetalleRegistroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleRegistro(int id, DetalleRegistro detalleRegistro)
        {
            if (id != detalleRegistro.IddetalleRegistro)
            {
                return BadRequest();
            }

            _context.Entry(detalleRegistro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleRegistroExists(id))
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

        // POST: api/DetalleRegistroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleRegistro>> PostDetalleRegistro(DetalleRegistro detalleRegistro)
        {
          if (_context.DetalleRegistros == null)
          {
              return Problem("Entity set 'MueblesCormarContext.DetalleRegistros'  is null.");
          }
            _context.DetalleRegistros.Add(detalleRegistro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleRegistro", new { id = detalleRegistro.IddetalleRegistro }, detalleRegistro);
        }

        // DELETE: api/DetalleRegistroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleRegistro(int id)
        {
            if (_context.DetalleRegistros == null)
            {
                return NotFound();
            }
            var detalleRegistro = await _context.DetalleRegistros.FindAsync(id);
            if (detalleRegistro == null)
            {
                return NotFound();
            }

            _context.DetalleRegistros.Remove(detalleRegistro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleRegistroExists(int id)
        {
            return (_context.DetalleRegistros?.Any(e => e.IddetalleRegistro == id)).GetValueOrDefault();
        }
    }
}
