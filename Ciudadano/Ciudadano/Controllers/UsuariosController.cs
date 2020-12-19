using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ciudadano.DataContext;
using Ciudadano.Models;
using Ciudadano.ApplicationServices;

namespace Ciudadano.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CiudadanoDataContext _context;
        private readonly UsuarioAppService _usuarioAppService;

        public UsuariosController(CiudadanoDataContext context, UsuarioAppService usuarioAppService)
        {
            _context = context;
            _usuarioAppService = usuarioAppService;

            if (_context.Usuarios.Count() == 0)
            {
                _context.Usuarios.Add(new Usuario { UsuarioId = "sampineda", Contrasenia = "password", EstaActivo = true });
                _context.SaveChanges();
            }
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Estudiante/1
        [HttpGet("{usuarioId}/{contrasenia}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string usuarioId, string contrasenia)
        {
            var response = await _usuarioAppService.TieneAccesoUsuario(usuarioId, contrasenia);

            if (response != "success")
            {
                return BadRequest(response);

            }
            return Ok("success");
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(string id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
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
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }
    }
}
