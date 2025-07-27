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
    public class ProductotiendasController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public ProductotiendasController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Productotiendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productotienda>>> GetProductotienda()
        {
            return await _context.Productotienda.ToListAsync();
        }

        // GET: api/Productotiendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productotienda>> GetProductotienda(int id)
        {
            var productotienda = await _context.Productotienda.FindAsync(id);

            if (productotienda == null)
            {
                return NotFound();
            }

            return productotienda;
        }

        // PUT: api/Productotiendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductotienda(int id, ProductoTiendaDTO dto)
        {

            var productotienda = await _context.Productotienda.FindAsync(id);
            if (productotienda == null)
            {
                return NotFound();
            }
            if (id != productotienda.IdProducto)
            {
                return BadRequest();
            }

            productotienda.IdProducto = dto.IdProducto;
            productotienda.IdTienda = dto.IdTienda;
            productotienda.Stock = dto.Stock;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductotiendaExists(id))
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

        // PUT: api/ProductoTienda
        [HttpPut("/api/ProductoTienda")]
        public async Task<IActionResult> UpdateProductoTienda([FromBody] ProductoStockInfo stockUpdate)
        {
            var productoTienda = await _context.Productotienda.FirstOrDefaultAsync(
                pt => pt.IdProducto == stockUpdate.IdProducto && pt.IdTienda == stockUpdate.IdTienda);

            if (productoTienda == null)
            {
                return NotFound("No se encontró el registro de stock para este producto y tienda.");
            }

            productoTienda.Stock = stockUpdate.Stock;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Productotiendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productotienda>> PostProductotienda(ProductoTiendaDTO dto)
        {
            var productotienda = new Productotienda
            {
                IdProducto = dto.IdProducto,
                IdTienda = dto.IdTienda,
                Stock = dto.Stock
            };
            _context.Productotienda.Add(productotienda);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductotiendaExists(productotienda.IdProducto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductotienda", new { id = productotienda.IdProducto }, productotienda);
        }

        // DELETE: api/Productotiendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductotienda(int id)
        {
            var productotienda = await _context.Productotienda.FindAsync(id);
            if (productotienda == null)
            {
                return NotFound();
            }

            _context.Productotienda.Remove(productotienda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductotiendaExists(int id)
        {
            return _context.Productotienda.Any(e => e.IdProducto == id);
        }
    }
}
