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
    public class NotificacionusuariosController : ControllerBase
    {
        private readonly TiendavirtualContext _context;

        public NotificacionusuariosController(TiendavirtualContext context)
        {
            _context = context;
        }

        // GET: api/Notificacionusuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notificacionusuario>>> GetNotificacionusuarios()
        {
            return await _context.Notificacionusuarios.ToListAsync();
        }

        // GET: api/Notificacionusuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacionusuario>> GetNotificacionusuario(int id)
        {
            var notificacionusuario = await _context.Notificacionusuarios.FindAsync(id);

            if (notificacionusuario == null)
            {
                return NotFound();
            }

            return notificacionusuario;
        }

        // PUT: api/Notificacionusuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificacionusuario(int id, NotificacionUsuarioDTO dto)
        {
            var notificacionusuario = await _context.Notificacionusuarios.FindAsync(id);
            if (notificacionusuario == null)
            {
                return NotFound();
            }
            if (id != notificacionusuario.Id)
            {
                return BadRequest();
            }


            notificacionusuario.UsuarioId = dto.UsuarioId;
            notificacionusuario.Mensaje = dto.Mensaje;
            notificacionusuario.FechaEnvio = dto.FechaEnvio;
            notificacionusuario.Leido = dto.Leido;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacionusuarioExists(id))
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

        // POST: api/Notificacionusuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notificacionusuario>> PostNotificacionusuario(NotificacionUsuarioDTO dto)
        {
            var notificacionusuario = new Notificacionusuario
            {
                UsuarioId = dto.UsuarioId,
                Mensaje = dto.Mensaje,
                FechaEnvio = dto.FechaEnvio,
                Leido = dto.Leido
            };
            _context.Notificacionusuarios.Add(notificacionusuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificacionusuario", new { id = notificacionusuario.Id }, notificacionusuario);
        }

        // DELETE: api/Notificacionusuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacionusuario(int id)
        {
            var notificacionusuario = await _context.Notificacionusuarios.FindAsync(id);
            if (notificacionusuario == null)
            {
                return NotFound();
            }

            _context.Notificacionusuarios.Remove(notificacionusuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificacionusuarioExists(int id)
        {
            return _context.Notificacionusuarios.Any(e => e.Id == id);
        }
    }
}
