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
    public class CiudadanosController : ControllerBase
    {
        private readonly CiudadanoDataContext _context;

        public CiudadanosController(CiudadanoDataContext context)
        {
            _context = context;

            if (_context.Ciudadanos.Count() == 0)
            {
                _context.Ciudadanos.Add(new CiudadanoI { Nombre = "Sam", Apellido="Pineda", Edad=20, EstadoCivilId=1});
                _context.SaveChanges();
            }
        }

        // GET: api/Ciudadanos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CiudadanoI>>> GetCiudadanos()
        {
            return await _context.Ciudadanos.Include(q => q.EstadoCivil).ToListAsync();
        }

        // GET: api/Ciudadanos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CiudadanoI>> GetCiudadanoI(int id)
        {
            var ciudadanoI = await _context.Ciudadanos.Include(q => q.EstadoCivil).FirstOrDefaultAsync(q => q.Id == id);

            if (ciudadanoI == null)
            {
                return NotFound();
            }

            return ciudadanoI;
        }

        // PUT: api/Ciudadanos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiudadanoI(int id, CiudadanoI ciudadanoI)
        {
            if (id != ciudadanoI.Id)
            {
                return BadRequest();
            }

            _context.Entry(ciudadanoI).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadanoIExists(id))
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

        // POST: api/Ciudadanos
        [HttpPost]
        public async Task<ActionResult<CiudadanoI>> PostCiudadanoI(CiudadanoI ciudadanoI)
        {
            _context.Ciudadanos.Add(ciudadanoI);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCiudadanoI", new { id = ciudadanoI.Id }, ciudadanoI);
        }

        // DELETE: api/Ciudadanos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CiudadanoI>> DeleteCiudadanoI(int id)
        {
            var ciudadanoI = await _context.Ciudadanos.FindAsync(id);
            if (ciudadanoI == null)
            {
                return NotFound();
            }

            _context.Ciudadanos.Remove(ciudadanoI);
            await _context.SaveChangesAsync();

            return ciudadanoI;
        }

        private bool CiudadanoIExists(int id)
        {
            return _context.Ciudadanos.Any(e => e.Id == id);
        }
    }
}
