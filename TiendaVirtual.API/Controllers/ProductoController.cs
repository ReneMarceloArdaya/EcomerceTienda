using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
    public class ProductoController : ControllerBase
    {
        private readonly TiendavirtualContext _context;
        private readonly Cloudinary _cloudinary;

        public ProductoController(TiendavirtualContext context, IConfiguration configuration)
        {
            _context = context;
            var account = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos([FromQuery] string? include)
        {
            // Comenzamos con la consulta base de Productos
            IQueryable<Producto> query = _context.Productos;

            // Verificar si se solicitó incluir proveedores
            // Esta lógica permite futuras expansiones (ej. ?include=proveedores,stock)
            if (!string.IsNullOrWhiteSpace(include))
            {
                var includes = include.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(i => i.Trim().ToLowerInvariant());

                // Si "proveedores" está en la lista de includes, cargamos los datos relacionados
                if (includes.Contains("proveedores"))
                {
                    query = query.Include(p => p.Proveedorproductos)
                        .ThenInclude(pp => pp.IdProveedorNavigation);
                }

            }

            try
            { 
                var productos = await query.ToListAsync();
                return Ok(productos); 
            }
            catch (Exception ex)
            {
                // Manejo básico de errores
                return StatusCode(500, $"Error interno del servidor al obtener productos: {ex.Message}");
            }
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // GET: api/Producto/5/stockentiendas
        [HttpGet("{id}/stockentiendas")]
        public async Task<ActionResult<IEnumerable<ProductoStockInfo>>> GetStockEnTiendas(int id)
        {
            
            // filtrando por el ID del producto que nos interesa.
            var stockInfo = await _context.Productotienda
                .Where(pt => pt.IdProducto == id)
                .Join(
                    _context.Tienda,
                    pt => pt.IdTienda,
                    t => t.Id,
                    (pt, t) => new ProductoStockInfo
                    {
                        IdProducto = pt.IdProducto,
                        IdTienda = t.Id,
                        NombreTienda = t.NombreTienda,
                        Direccion = t.Direccion,
                        Ciudad = t.Ciudad,
                        Stock = pt.Stock
                    })
                .ToListAsync();

            return stockInfo;
        }

        



        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, ProductoDTO dto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            if (id != producto.Id)
            {
                return BadRequest();
            }
            var productoOriginal = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (productoOriginal == null)
            {
                return NotFound();
            }

            producto.NombreProducto = dto.NombreProducto;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.IdCategoria = dto.IdCategoria;
            producto.Imagen = dto.Imagen;


            try
            {
                await _context.SaveChangesAsync();

                // Si la imagen ha cambiado y la original no estaba vacía, la eliminamos de Cloudinary
                if (productoOriginal.Imagen != producto.Imagen && !string.IsNullOrEmpty(productoOriginal.Imagen))
                {
                    var publicId = GetPublicIdFromUrl(productoOriginal.Imagen);
                    if (!string.IsNullOrEmpty(publicId))
                    {
                        var deletionParams = new DeletionParams(publicId);
                        await _cloudinary.DestroyAsync(deletionParams);
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(ProductoDTO dto)
        {
            var producto = new Producto
            {
                NombreProducto = dto.NombreProducto,
                PrecioVenta = dto.PrecioVenta,
                IdCategoria = dto.IdCategoria,
                Imagen = dto.Imagen
            };
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var imagenAEliminar = producto.Imagen;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            // Si el producto tenía una imagen, la eliminamos de Cloudinary
            if (!string.IsNullOrEmpty(imagenAEliminar))
            {
                var publicId = GetPublicIdFromUrl(imagenAEliminar);
                if (!string.IsNullOrEmpty(publicId))
                {
                    var deletionParams = new DeletionParams(publicId);
                    await _cloudinary.DestroyAsync(deletionParams);
                }
            }

            return NoContent();
        }

        private string GetPublicIdFromUrl(string url)
        {
            // Expresión regular para extraer la parte de la ruta después de /upload/v.../
            var regex = new Regex(@"/v\d+/(?<public_id>.*)");
            var match = regex.Match(url);

            if (!match.Success) return null;

            var publicIdWithExtension = match.Groups["public_id"].Value;
            // Quitamos la extensión del archivo
            var lastDotIndex = publicIdWithExtension.LastIndexOf('.');
            if (lastDotIndex == -1) return publicIdWithExtension;

            return publicIdWithExtension.Substring(0, lastDotIndex);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
