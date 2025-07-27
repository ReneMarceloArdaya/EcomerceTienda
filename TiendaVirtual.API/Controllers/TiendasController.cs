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
    public class TiendasController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public TiendasController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Tiendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tienda>>> GetTienda()
        {
            return await _context.Tienda.ToListAsync();
        }

        // GET: api/Tiendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tienda>> GetTienda(int id)
        {
            var tienda = await _context.Tienda.FindAsync(id);

            if (tienda == null)
            {
                return NotFound();
            }

            return tienda;
        }

        // PUT: api/Tiendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTienda(int id, TiendaDTO dto)
        {
            var tienda = await _context.Tienda.FindAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }
            if (id != tienda.Id)
            {
                return BadRequest();
            }


            tienda.NombreTienda = dto.NombreTienda;
            tienda.Direccion = dto.Direccion;
            tienda.Ciudad = dto.Ciudad;
            tienda.Latitud = dto.Latitud;
            tienda.Longitud = dto.Longitud;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiendaExists(id))
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

        // POST: api/Tiendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tienda>> PostTienda(TiendaDTO dto)
        {
            var tienda = new Tienda
            {
                NombreTienda = dto.NombreTienda,
                Direccion = dto.Direccion,
                Ciudad = dto.Ciudad,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud
            };
            _context.Tienda.Add(tienda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTienda", new { id = tienda.Id }, tienda);
        }

        // DELETE: api/Tiendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTienda(int id)
        {
            var tienda = await _context.Tienda.FindAsync(id);
            if (tienda == null)
            {
                return NotFound();
            }

            _context.Tienda.Remove(tienda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiendaExists(int id)
        {
            return _context.Tienda.Any(e => e.Id == id);
        }
    }
}
