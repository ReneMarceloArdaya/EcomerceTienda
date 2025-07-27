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
    public class DireccionenviosController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public DireccionenviosController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Direccionenvios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Direccionenvio>>> GetDireccionenvios()
        {
            return await _context.Direccionenvios.ToListAsync();
        }

        // GET: api/Direccionenvios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Direccionenvio>> GetDireccionenvio(int id)
        {
            var direccionenvio = await _context.Direccionenvios.FindAsync(id);

            if (direccionenvio == null)
            {
                return NotFound();
            }

            return direccionenvio;
        }

        // PUT: api/Direccionenvios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDireccionenvio(int id, DireccionEnvioDTO dto)
        {
            var direccionenvio = await _context.Direccionenvios.FindAsync(id);
            if (direccionenvio == null)
            {
                return NotFound();
            }
            if (id != direccionenvio.Id)
            {
                return BadRequest();
            }


            direccionenvio.UsuarioId = dto.UsuarioId;
            direccionenvio.Direccion = dto.Direccion;
            direccionenvio.Ciudad = dto.Ciudad;
            direccionenvio.Departamento = dto.Departamento;
            direccionenvio.Latitud = dto.Latitud;
            direccionenvio.Longitud = dto.Longitud;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionenvioExists(id))
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

        // POST: api/Direccionenvios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Direccionenvio>> PostDireccionenvio(DireccionEnvioDTO dto)
        {
            var direccionenvio = new Direccionenvio
            {
                UsuarioId = dto.UsuarioId,
                Direccion = dto.Direccion,
                Ciudad = dto.Ciudad,
                Departamento = dto.Departamento,
                Latitud = dto.Latitud,
                Longitud = dto.Longitud
            };
            _context.Direccionenvios.Add(direccionenvio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDireccionenvio", new { id = direccionenvio.Id }, direccionenvio);
        }

        // DELETE: api/Direccionenvios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDireccionenvio(int id)
        {
            var direccionenvio = await _context.Direccionenvios.FindAsync(id);
            if (direccionenvio == null)
            {
                return NotFound();
            }

            _context.Direccionenvios.Remove(direccionenvio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DireccionenvioExists(int id)
        {
            return _context.Direccionenvios.Any(e => e.Id == id);
        }
    }
}
