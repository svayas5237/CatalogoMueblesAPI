using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogoMueblesAPI.Data;   
using CatalogoMueblesAPI.DTOs;   
using CatalogoMueblesAPI.Models; 

namespace CatalogoMueblesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetalleVentaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DetalleVenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetalles()
        {
            // Cargamos la Venta y el Cliente asociado a la venta
            return await _context.DetallesVenta
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                    .ThenInclude(v => v.Cliente)
                .ToListAsync();
        }

        // PUT: api/DetalleVenta/5
        // Actualiza el estado de entrega usando el DTO
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVenta(int id, DetalleVentaDto detalleDto)
        {
            if (id != detalleDto.IdDetalle)
            {
                return BadRequest("El ID del detalle no coincide.");
            }

            // Buscamos el registro real en la base de datos
            var detalleExistente = await _context.DetallesVenta.FindAsync(id);
            if (detalleExistente == null)
            {
                return NotFound();
            }

            // Actualizamos solo los campos de entrega
            detalleExistente.Entregado = detalleDto.Entregado;
            detalleExistente.FechaEntrega = detalleDto.FechaEntrega;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar la base de datos.");
            }

            return NoContent();
        }
    }
}