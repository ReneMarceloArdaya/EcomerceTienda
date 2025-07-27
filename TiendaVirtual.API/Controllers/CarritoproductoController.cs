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
    public class CarritoproductoController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public CarritoproductoController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Carritoproductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carritoproducto>>> GetCarritoproductos()
        {
            return await _context.Carritoproductos.ToListAsync();
        }

        // GET: api/Carritoproductoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carritoproducto>> GetCarritoproducto(int id)
        {
            var carritoproducto = await _context.Carritoproductos.FindAsync(id);

            if (carritoproducto == null)
            {
                return NotFound();
            }

            return carritoproducto;
        }

        // PUT: api/Carritoproductoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarritoproducto(int id, CarritoproductoDTO dto)
        {
            var carritoproducto = await _context.Carritoproductos.FindAsync(id);
            if (carritoproducto == null)
            {
                return NotFound();
            }
            if (id != carritoproducto.Id)
            {
                return BadRequest();
            }


            // Map DTO to entity
            carritoproducto.CarritoId = dto.CarritoId;
            carritoproducto.ProductoId = dto.ProductoId;
            carritoproducto.Cantidad = dto.Cantidad;
            carritoproducto.PrecioUnitario = dto.PrecioUnitario;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoproductoExists(id))
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

        // POST: api/Carritoproductoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carritoproducto>> PostCarritoproducto(CarritoproductoDTO dto)
        {
            // Map DTO to entity
            var carritoproducto = new Carritoproducto
            {
                CarritoId = dto.CarritoId,
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            };
            _context.Carritoproductos.Add(carritoproducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarritoproducto", new { id = carritoproducto.Id }, carritoproducto);
        }

        // DELETE: api/Carritoproductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarritoproducto(int id)
        {
            var carritoproducto = await _context.Carritoproductos.FindAsync(id);
            if (carritoproducto == null)
            {
                return NotFound();
            }

            _context.Carritoproductos.Remove(carritoproducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoproductoExists(int id)
        {
            return _context.Carritoproductos.Any(e => e.Id == id);
        }
    }
}
