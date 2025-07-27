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
    public class ClientenaturalController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public ClientenaturalController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Clientenaturals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientenatural>>> GetClientenaturals()
        {
            return await _context.Clientenatural.ToListAsync();
        }

        // GET: api/Clientenaturals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientenatural>> GetClientenatural(int id)
        {
            var clientenatural = await _context.Clientenatural.FindAsync(id);

            if (clientenatural == null)
            {
                return NotFound();
            }

            return clientenatural;
        }

        // PUT: api/Clientenaturals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientenatural(int id, ClienteNaturalDTO dto)
        {
            var clientenatural = await _context.Clientenatural.FindAsync(id);
            if (clientenatural == null)
            {
                return NotFound();
            }
            if (id != clientenatural.Id)
            {
                return BadRequest();
            }


            clientenatural.NombreCompleto = dto.NombreCompleto;
            clientenatural.ApellidoPaterno = dto.ApellidoPaterno;
            clientenatural.ApellidoMaterno = dto.ApellidoMaterno;
            clientenatural.ClienteId = dto.ClienteId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientenaturalExists(id))
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

        // POST: api/Clientenaturals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Clientenatural>> PostClientenatural(ClienteNaturalDTO dto)
        {
            var clientenatural = new Clientenatural
            {
                NombreCompleto = dto.NombreCompleto,
                ApellidoPaterno = dto.ApellidoPaterno,
                ApellidoMaterno = dto.ApellidoMaterno,
                ClienteId = dto.ClienteId
            };
            _context.Clientenatural.Add(clientenatural);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientenatural", new { id = clientenatural.Id }, clientenatural);
        }

        // DELETE: api/Clientenaturals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientenatural(int id)
        {
            var clientenatural = await _context.Clientenatural.FindAsync(id);
            if (clientenatural == null)
            {
                return NotFound();
            }

            _context.Clientenatural.Remove(clientenatural);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientenaturalExists(int id)
        {
            return _context.Clientenatural.Any(e => e.Id == id);
        }
    }
}
