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
    public class DetallepedidosController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public DetallepedidosController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Detallepedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detallepedido>>> GetDetallepedidos()
        {
            return await _context.Detallepedidos.ToListAsync();
        }

        // GET: api/Detallepedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detallepedido>> GetDetallepedido(int id)
        {
            var detallepedido = await _context.Detallepedidos.FindAsync(id);

            if (detallepedido == null)
            {
                return NotFound();
            }

            return detallepedido;
        }

        // PUT: api/Detallepedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetallepedido(int id, DetallePedidoDTO dto)
        {
            var detallepedido = await _context.Detallepedidos.FindAsync(id);
            if (detallepedido == null)
            {
                return NotFound();
            }
            if (id != detallepedido.IdPedido)
            {
                return BadRequest();
            }


            detallepedido.IdProducto = dto.IdProducto;
            detallepedido.Cantidad = dto.Cantidad;
            detallepedido.Precio = dto.Precio;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallepedidoExists(id))
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

        // POST: api/Detallepedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detallepedido>> PostDetallepedido(DetallePedidoDTO dto)
        {
            var detallepedido = new Detallepedido
            {
                IdPedido = dto.IdPedido,
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad,
                Precio = dto.Precio
            };
            _context.Detallepedidos.Add(detallepedido);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetallepedidoExists(detallepedido.IdPedido))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDetallepedido", new { id = detallepedido.IdPedido }, detallepedido);
        }

        // DELETE: api/Detallepedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallepedido(int id)
        {
            var detallepedido = await _context.Detallepedidos.FindAsync(id);
            if (detallepedido == null)
            {
                return NotFound();
            }

            _context.Detallepedidos.Remove(detallepedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallepedidoExists(int id)
        {
            return _context.Detallepedidos.Any(e => e.IdPedido == id);
        }
    }
}
