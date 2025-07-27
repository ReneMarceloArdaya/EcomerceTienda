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
    public class CuotasController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public CuotasController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Cuotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuota>>> GetCuota()
        {
            return await _context.Cuota.ToListAsync();
        }

        // GET: api/Cuotas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuota>> GetCuota(int id)
        {
            var cuota = await _context.Cuota.FindAsync(id);

            if (cuota == null)
            {
                return NotFound();
            }

            return cuota;
        }

        // PUT: api/Cuotas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuota(int id, CuotaDTO dto)
        {
            var cuota = await _context.Cuota.FindAsync(id);
            if (cuota == null)
            {
                return NotFound();
            }
            if (id != cuota.Id)
            {
                return BadRequest();
            }


            cuota.Monto = dto.Monto;
            cuota.Interes = dto.Interes;
            cuota.FechaPagoProgramado = dto.FechaPagoProgramado;
            cuota.FechaPago = dto.FechaPago;
            cuota.IdCredito = dto.IdCredito;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuotaExists(id))
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

        // POST: api/Cuotas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuota>> PostCuota(CuotaDTO dto)
        {
            var cuota = new Cuota
            {
                Monto = dto.Monto,
                Interes = dto.Interes,
                FechaPagoProgramado = dto.FechaPagoProgramado,
                FechaPago = dto.FechaPago,
                IdCredito = dto.IdCredito
            };
            _context.Cuota.Add(cuota);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuota", new { id = cuota.Id }, cuota);
        }

        // DELETE: api/Cuotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuota(int id)
        {
            var cuota = await _context.Cuota.FindAsync(id);
            if (cuota == null)
            {
                return NotFound();
            }

            _context.Cuota.Remove(cuota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuotaExists(int id)
        {
            return _context.Cuota.Any(e => e.Id == id);
        }
    }
}
