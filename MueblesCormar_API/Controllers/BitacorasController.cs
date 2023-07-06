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
