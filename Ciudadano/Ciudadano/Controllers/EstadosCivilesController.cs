using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ciudadano.DataContext;
using Ciudadano.Models;

namespace Ciudadano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosCivilesController : ControllerBase
    {
        private readonly CiudadanoDataContext _context;

        public EstadosCivilesController(CiudadanoDataContext context)
        {
            _context = context;

            if (_context.EstadosCiviles.Count() == 0)
            {
                _context.EstadosCiviles.Add(new EstadoCivil { Estado = "Soltero"});
                _context.SaveChanges();
            }
        }

        // GET: api/EstadosCiviles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoCivil>>> GetEstadosCiviles()
        {
            return await _context.EstadosCiviles.Include(q => q.Ciudadanos).ToListAsync();
        }

        // GET: api/EstadosCiviles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoCivil>> GetEstadoCivil(int id)
        {
            var estadoCivil = await _context.EstadosCiviles.Include(q => q.Ciudadanos).FirstOrDefaultAsync(q => q.Id == id);

            if (estadoCivil == null)
            {
                return NotFound();
            }

            return estadoCivil;
        }

        // PUT: api/EstadosCiviles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoCivil(int id, EstadoCivil estadoCivil)
        {
            if (id != estadoCivil.Id)
            {
                return BadRequest();
            }

            _context.Entry(estadoCivil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoCivilExists(id))
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

        // POST: api/EstadosCiviles
        [HttpPost]
        public async Task<ActionResult<EstadoCivil>> PostEstadoCivil(EstadoCivil estadoCivil)
        {
            _context.EstadosCiviles.Add(estadoCivil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadoCivil", new { id = estadoCivil.Id }, estadoCivil);
        }

        // DELETE: api/EstadosCiviles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EstadoCivil>> DeleteEstadoCivil(int id)
        {
            var estadoCivil = await _context.EstadosCiviles.FindAsync(id);
            if (estadoCivil == null)
            {
                return NotFound();
            }

            _context.EstadosCiviles.Remove(estadoCivil);
            await _context.SaveChangesAsync();

            return estadoCivil;
        }

        private bool EstadoCivilExists(int id)
        {
            return _context.EstadosCiviles.Any(e => e.Id == id);
        }
    }
}
