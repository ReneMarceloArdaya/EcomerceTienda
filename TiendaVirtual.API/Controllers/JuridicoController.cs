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
    public class JuridicoController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public JuridicoController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Juridicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Juridico>>> GetJuridicos()
        {
            return await _context.Juridicos.ToListAsync();
        }

        // GET: api/Juridicoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Juridico>> GetJuridico(string id)
        {
            var juridico = await _context.Juridicos.FindAsync(id);

            if (juridico == null)
            {
                return NotFound();
            }

            return juridico;
        }

        // PUT: api/Juridicoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJuridico(string id, JuridicoDTO dto)
        {
            var juridico = await _context.Juridicos.FindAsync(id);
            if (juridico == null)
            {
                return NotFound();
            }
            if (id != juridico.Id)
            {
                return BadRequest();
            }


            juridico.RazonSocial = dto.RazonSocial;
            juridico.RepresentanteLegal = dto.RepresentanteLegal;
            juridico.ClienteId = dto.ClienteId;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JuridicoExists(id))
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

        // POST: api/Juridicoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Juridico>> PostJuridico(JuridicoDTO dto)
        {
            var juridico = new Juridico
            {
                Id = dto.Id,
                RazonSocial = dto.RazonSocial,
                RepresentanteLegal = dto.RepresentanteLegal,
                ClienteId = dto.ClienteId
            };
            _context.Juridicos.Add(juridico);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JuridicoExists(juridico.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJuridico", new { id = juridico.Id }, juridico);
        }

        // DELETE: api/Juridicoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJuridico(string id)
        {
            var juridico = await _context.Juridicos.FindAsync(id);
            if (juridico == null)
            {
                return NotFound();
            }

            _context.Juridicos.Remove(juridico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JuridicoExists(string id)
        {
            return _context.Juridicos.Any(e => e.Id == id);
        }
    }
}
