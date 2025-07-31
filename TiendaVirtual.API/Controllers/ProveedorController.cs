using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using TiendaVirtual.API.Data;
using TiendaVirtual.API.Models;
using TiendaVirtual.API.Models.DTOs;

namespace TiendaVirtual.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public ProveedorController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Proveedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedors()
        {
            return await _context.Proveedor.ToListAsync();
        }

        // GET: api/Proveedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            return proveedor;
        }

        // GET: api/Proveedor/1/productos
        [HttpGet("{id}/productos")]
        public async Task<ActionResult<IEnumerable<ProveedorProductoViewModel>>> GetProductosPorProveedor(int id)
        {
            var productosDelProveedor = await _context.Proveedorproductos
                .Include(pp => pp.IdProductoNavigation)
                .Where(pp => pp.IdProveedor == id)
                .Select(pp => new ProveedorProductoViewModel
                {
                    IdProveedor = pp.IdProveedor,
                    IdProducto = pp.IdProducto,
                    NombreProducto = pp.IdProductoNavigation.NombreProducto,
                    ImagenProducto = pp.IdProductoNavigation.Imagen,
                    NombreEspecifico = pp.NombreEspecifico
                })
                .ToListAsync();


            if (productosDelProveedor == null)
            {
                return NotFound();
            }
            return Ok(productosDelProveedor);
        }

        // PUT: api/Proveedors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, ProveedorDTO dto)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            if (id != proveedor.Id)
            {
                return BadRequest();
            }


            proveedor.NombreProveedor = dto.NombreProveedor;
            proveedor.Direccion = dto.Direccion;
            proveedor.Telefono = dto.Telefono;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        // POST: api/Proveedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(ProveedorDTO dto)
        {
            var proveedor = new Proveedor
            {
                NombreProveedor = dto.NombreProveedor,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono
            };
            _context.Proveedor.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedor", new { id = proveedor.Id }, proveedor);
        }

        // DELETE: api/Proveedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedor.Any(e => e.Id == id);
        }
    }

    public  class ProveedorProductoViewModel
    {
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string ImagenProducto { get; set; }
        public string NombreEspecifico { get; set; }
    }
}
