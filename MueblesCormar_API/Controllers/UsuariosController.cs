﻿using System;
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
    public class UsuariosController : ControllerBase
    {
        private readonly MueblesCormarContext _context;

        public Tools.Crypto MyCrypto { get; set; }

        public UsuariosController(MueblesCormarContext context)
        {
            _context = context;

            MyCrypto = new Tools.Crypto();
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

       
        // GET: api/Usuarios/GetInfoUsuario?email=leonardo@gmail.com
        [HttpGet("GetInfoUsuario")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetInfoUsuario(string email)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }

            var query = (from u in _context.Usuarios
                         where u.Email == email
                         select new
                         {
                             idusuario = u.Idusuario,
                             nombre = u.Nombre,
                             email = u.Email,
                             telefono = u.Telefono,
                             idrolusuario = u.IdrolUsuario,
                         }).ToList();
            List<UsuarioDTO> list = new List<UsuarioDTO>();

            foreach (var item in query)
            {
                UsuarioDTO newItem = new UsuarioDTO();

                newItem.Idusuario = item.idusuario;
                newItem.Nombre = item.nombre;
                newItem.Email = item.email;
                newItem.Telefono = item.telefono;
                newItem.IdrolUsuario = item.idrolusuario;
                
                list.Add(newItem);
            }

            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        // GET: api/Usuarios/GetListaEmpleado
        [HttpGet("GetListaEmpleado")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetListaEmpleado()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }

            var query = from u in _context.Usuarios
                        where u.IdrolUsuario == 2
                         select new
                         {
                             Idusuario = u.Idusuario,
                             Nombre = u.Nombre,
                             Email = u.Email,
                             Telefono = u.Telefono,
                             IdrolUsuario = u.IdrolUsuario,
                         };

            List<UsuarioDTO> EmpleadoLista = new List<UsuarioDTO>();

            foreach (var usuario in query)
            {
                EmpleadoLista.Add(
                    new UsuarioDTO
                    {
                        Idusuario = usuario.Idusuario,
                        Nombre = usuario.Nombre,
                        Email = usuario.Email,
                        Telefono = usuario.Telefono,
                        IdrolUsuario= usuario.IdrolUsuario,
                    }

                    );
            }

            if (EmpleadoLista == null)
            {
                return NotFound();
            }

            return EmpleadoLista;
        }

        // GET: api/Usuarios/ValidarLogin?NombreUsuario=l&ContraseniaUsuario=1
        [HttpGet("ValidarLogin")]
        public async Task<ActionResult<Usuario>> ValidarLogin(string NombreUsuario, string ContraseniaUsuario)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }

            string ApiLevelEncriptedPassword = MyCrypto.EncriptarEnUnSentido(ContraseniaUsuario);

            var usuario = await _context.Usuarios.SingleOrDefaultAsync(e => e.Email == NombreUsuario &&
            e.Contraseña == ApiLevelEncriptedPassword);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Idusuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'MueblesCormarContext.Usuarios'  is null.");
          }

            string ApiLevelEncriptedPass = MyCrypto.EncriptarEnUnSentido(usuario.Contraseña);

            usuario.Contraseña = ApiLevelEncriptedPass;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Idusuario }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Idusuario == id)).GetValueOrDefault();
        }
    }
}
