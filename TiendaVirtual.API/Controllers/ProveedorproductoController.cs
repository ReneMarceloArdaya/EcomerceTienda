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
    public class ProveedorproductoController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public ProveedorproductoController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Proveedorproductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedorproducto>>> GetProveedorproductos()
        {
            return await _context.Proveedorproductos.ToListAsync();
        }

        // GET: api/Proveedorproductoes con dos id
        [HttpGet("proveedor/{idProveedor}/producto/{idProducto}")]
        public async Task<ActionResult<Proveedorproducto>> GetProveedorproducto(int idProveedor , int idProducto)
        {
            var proveedorproducto = await _context.Proveedorproductos
                .FirstOrDefaultAsync(pp => pp.IdProveedor == idProveedor && pp.IdProducto == idProducto);
          

            if (proveedorproducto == null)
            {
                return NotFound();
            }

            return proveedorproducto;
        }

        // PUT: api/Proveedorproducto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("proveedor/{idProveedor}/producto/{idProducto}")]
        public async Task<IActionResult> PutProveedorproducto(int idProveedor, int idProducto, ProveedorProductoDTO dto)
        {
            
            var proveedorproducto = await _context.Proveedorproductos
                .FirstOrDefaultAsync(pp => pp.IdProveedor == idProveedor && pp.IdProducto == idProducto);

            if (proveedorproducto == null)
            {
                return NotFound();
            }

            proveedorproducto.IdProveedor = dto.IdProveedor;
            proveedorproducto.IdProducto = dto.IdProducto;
            proveedorproducto.NombreEspecifico = dto.NombreEspecifico;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorproductoExists(idProveedor))
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

        //PUT para actualizar un producto con una lista de provedores
        [HttpPut("actualizarproveedores/producto/{idProducto}")]
        public async Task<IActionResult> ActualizarProveedores(int IdProducto, List<ProveedorDTO> lista)
        {
            var producto = await _context.Productos.FindAsync(IdProducto);
            if (producto == null)
            {
                return NotFound();
            }
            // Eliminar los proveedores existentes para el producto
            var proveedoresExistentes = _context.Proveedorproductos.Where(pp => pp.IdProducto == IdProducto);

            // Si hay provedores existentes, los eliminamos
            if  ( proveedoresExistentes != null )
            {
                _context.Proveedorproductos.RemoveRange(proveedoresExistentes);
            }

            // Agregar los nuevos proveedores
            foreach (var proveedor in lista)
            {
                var nuevoProveedorProducto = new Proveedorproducto
                {
                    IdProveedor = proveedor.Id,
                    IdProducto = IdProducto,
                    NombreEspecifico = proveedor.NombreProveedor
                };
                _context.Proveedorproductos.Add(nuevoProveedorProducto);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Proveedorproductoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedorproducto>> PostProveedorproducto(ProveedorProductoDTO dto)
        {
            var proveedorproducto = new Proveedorproducto
            {
                IdProveedor = dto.IdProveedor,
                IdProducto = dto.IdProducto,
                NombreEspecifico = dto.NombreEspecifico
            };
            _context.Proveedorproductos.Add(proveedorproducto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProveedorproductoExists(proveedorproducto.IdProveedor))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProveedorproducto", new { id = proveedorproducto.IdProveedor }, proveedorproducto);
        }

        // DELETE: api/Proveedorproductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedorproducto(int id)
        {
            var proveedorproducto = await _context.Proveedorproductos.FindAsync(id);
            if (proveedorproducto == null)
            {
                return NotFound();
            }

            _context.Proveedorproductos.Remove(proveedorproducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorproductoExists(int id)
        {
            return _context.Proveedorproductos.Any(e => e.IdProveedor == id);
        }
    }
}
