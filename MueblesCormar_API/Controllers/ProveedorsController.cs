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
    public class ProveedorsController : ControllerBase
    {
        private readonly MueblesCormarContext _context;

        public ProveedorsController(MueblesCormarContext context)
        {
            _context = context;
        }

        // GET: api/Proveedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedors()
        {
          if (_context.Proveedors == null)
          {
              return NotFound();
          }
            return await _context.Proveedors.ToListAsync();
        }

        // GET: api/Proveedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
          if (_context.Proveedors == null)
          {
              return NotFound();
          }
            var proveedor = await _context.Proveedors.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        // GET: api/Proveedors/GetDataProveedor?idProveedor=7
        [HttpGet("GetDataProveedor")]
        public ActionResult<IEnumerable<ProveedorDTO>> GetDataProveedor(int idProveedor)
        {
            //las consultas linq se parecen mucho a las normales que hemos hecho en T-SQL
            //una de las diferencias es que podemos usar una "tabla temporal" para almacenar
            //los resultados y luego usarla para llenar los atributos de un modelo o DTO

            var query = (from p in _context.Proveedors
                         where p.Idproveedor == idProveedor
                         select new
                         {
                             Idproveedor = p.Idproveedor,
                             Nombre = p.Nombre,
                             Direccion = p.Direccion
                         }).ToList();

            List<ProveedorDTO> list = new List<ProveedorDTO>();

            foreach (var proveedor in query)
            {
                ProveedorDTO NewItem = new ProveedorDTO();

                NewItem.Idproveedor = proveedor.Idproveedor;
                NewItem.Nombre = proveedor.Nombre;
                NewItem.Direccion = proveedor.Direccion;

                list.Add(NewItem);

            }

            if (list == null)
            {
                return NotFound();
            }

            return list;

        }

        // GET: api/Proveedors/GetListaProveedor
        [HttpGet("GetListaProveedor")]
        public ActionResult<IEnumerable<ProveedorDTO>> GetListaProveedor()
        {
            if (_context.Proveedors == null)
            {
                return NotFound();
            }

            var query = from p in _context.Proveedors
                        select new
                        {
                            Idproveedor = p.Idproveedor,
                            Nombre = p.Nombre,
                            Direccion = p.Direccion
                        };

            List<ProveedorDTO> ProveedorLista = new List<ProveedorDTO>();

            foreach (var proveedor in query)
            {
                ProveedorLista.Add(
                    new ProveedorDTO
                    {
                        Idproveedor = proveedor.Idproveedor,
                        Nombre = proveedor.Nombre,
                        Direccion = proveedor.Direccion

                    }

                    );
            }

            if (ProveedorLista == null)
            {
                return NotFound();
            }

            return ProveedorLista;
        }

        // PUT: api/Proveedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.Idproveedor)
            {
                return BadRequest();
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        // POST: api/Proveedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
          if (_context.Proveedors == null)
          {
              return Problem("Entity set 'MueblesCormarContext.Proveedors'  is null.");
          }
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedor", new { id = proveedor.Idproveedor }, proveedor);
        }

        // DELETE: api/Proveedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            if (_context.Proveedors == null)
            {
                return NotFound();
            }
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedors.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return (_context.Proveedors?.Any(e => e.Idproveedor == id)).GetValueOrDefault();
        }
    }
}
