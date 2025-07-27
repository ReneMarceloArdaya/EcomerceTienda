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
    public class EstadopedidohistorialController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public EstadopedidohistorialController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Estadopedidohistorials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estadopedidohistorial>>> GetEstadopedidohistorials()
        {
            return await _context.Estadopedidohistorials.ToListAsync();
        }

        // GET: api/Estadopedidohistorials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estadopedidohistorial>> GetEstadopedidohistorial(int id)
        {
            var estadopedidohistorial = await _context.Estadopedidohistorials.FindAsync(id);

            if (estadopedidohistorial == null)
            {
                return NotFound();
            }

            return estadopedidohistorial;
        }

        // PUT: api/Estadopedidohistorials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadopedidohistorial(int id, EstadoPedidoHistorialDTO dto)
        {
            var estadopedidohistorial = await _context.Estadopedidohistorials.FindAsync(id);
            if (estadopedidohistorial == null)
            {
                return NotFound();
            }
            if (id != estadopedidohistorial.Id)
            {
                return BadRequest();
            }


            // Map DTO to entity
            estadopedidohistorial.IdPedido = dto.IdPedido;
            estadopedidohistorial.EstadoAnterior = dto.EstadoAnterior;
            estadopedidohistorial.EstadoNuevo = dto.EstadoNuevo;
            estadopedidohistorial.FechaCambio = dto.FechaCambio;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadopedidohistorialExists(id))
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

        // POST: api/Estadopedidohistorials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estadopedidohistorial>> PostEstadopedidohistorial(EstadoPedidoHistorialDTO dto)
        {
            var estadopedidohistorial = new Estadopedidohistorial
            {
                IdPedido = dto.IdPedido,
                EstadoAnterior = dto.EstadoAnterior,
                EstadoNuevo = dto.EstadoNuevo,
                FechaCambio = dto.FechaCambio
            };
            _context.Estadopedidohistorials.Add(estadopedidohistorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadopedidohistorial", new { id = estadopedidohistorial.Id }, estadopedidohistorial);
        }

        // DELETE: api/Estadopedidohistorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadopedidohistorial(int id)
        {
            var estadopedidohistorial = await _context.Estadopedidohistorials.FindAsync(id);
            if (estadopedidohistorial == null)
            {
                return NotFound();
            }

            _context.Estadopedidohistorials.Remove(estadopedidohistorial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadopedidohistorialExists(int id)
        {
            return _context.Estadopedidohistorials.Any(e => e.Id == id);
        }
    }
}
