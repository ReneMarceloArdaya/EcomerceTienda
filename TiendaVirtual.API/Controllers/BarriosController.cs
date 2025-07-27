using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVirtual.API.Data;
using TiendaVirtual.API.Models;
using TiendaVirtual.API.Models.DTOs;

namespace TiendaVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarriosController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public BarriosController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Barrios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barrio>>> GetBarrios()
        {
            return await _context.Barrios.ToListAsync();
        }

        // GET: api/Barrios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Barrio>> GetBarrio(int id)
        {
            var barrio = await _context.Barrios.FindAsync(id);

            if (barrio == null)
            {
                return NotFound();
            }

            return barrio;
        }

        // PUT: api/Barrios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarrio(int id, BarrioDTO dto)
        {
            var barrio = await _context.Barrios.FindAsync(id);
            if (barrio == null)
            {
                return NotFound();
            }
            if (id != barrio.Id)
            {
                return BadRequest();
            }

            barrio.NombreBarrio = dto.NombreBarrio;
            barrio.ZonaId = dto.ZonaId;
           
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarrioExists(id))
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

        // POST: api/Barrios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Barrio>> PostBarrio(BarrioDTO dto)
        {
            var barrio = new Barrio
            {
                NombreBarrio = dto.NombreBarrio,
                ZonaId = dto.ZonaId
            };
            _context.Barrios.Add(barrio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBarrio", new { id = barrio.Id }, barrio);
        }

        // DELETE: api/Barrios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarrio(int id)
        {
            var barrio = await _context.Barrios.FindAsync(id);
            if (barrio == null)
            {
                return NotFound();
            }

            _context.Barrios.Remove(barrio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BarrioExists(int id)
        {
            return _context.Barrios.Any(e => e.Id == id);
        }
    }
}
