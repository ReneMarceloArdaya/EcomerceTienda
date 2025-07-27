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
    public class CreditoController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public CreditoController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Creditoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credito>>> GetCreditos()
        {
            return await _context.Creditos.ToListAsync();
        }

        // GET: api/Creditoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Credito>> GetCredito(int id)
        {
            var credito = await _context.Creditos.FindAsync(id);

            if (credito == null)
            {
                return NotFound();
            }

            return credito;
        }

        // PUT: api/Creditoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCredito(int id, CreditoDTO dto)
        {
            var credito = await _context.Creditos.FindAsync(id);
            if (credito == null)
            {
                return NotFound();
            }
            if (id != credito.Id)
            {
                return BadRequest();
            }


            credito.Monto = dto.Monto;
            credito.InteresMensual = dto.InteresMensual;
            credito.FechaDesembolso = dto.FechaDesembolso;
            credito.IdPedido = dto.IdPedido;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditoExists(id))
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

        // POST: api/Creditoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Credito>> PostCredito(CreditoDTO dto)
        {
            var credito = new Credito
            {
                Monto = dto.Monto,
                InteresMensual = dto.InteresMensual,
                FechaDesembolso = dto.FechaDesembolso,
                IdPedido = dto.IdPedido
            };
            _context.Creditos.Add(credito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredito", new { id = credito.Id }, credito);
        }

        // DELETE: api/Creditoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredito(int id)
        {
            var credito = await _context.Creditos.FindAsync(id);
            if (credito == null)
            {
                return NotFound();
            }

            _context.Creditos.Remove(credito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditoExists(int id)
        {
            return _context.Creditos.Any(e => e.Id == id);
        }
    }
}
