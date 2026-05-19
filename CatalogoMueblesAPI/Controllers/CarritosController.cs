using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarritosController : ControllerBase
{
    private readonly AppDbContext _context;
    public CarritosController(AppDbContext context) => _context = context;

    // GET: api/Carritos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        => await _context.Carritos
            .Include(c => c.Cliente)
            .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
            .ToListAsync();

    // GET: api/Carritos/cliente/5
    [HttpGet("cliente/{idCliente}")]
    public async Task<ActionResult<Carrito>> GetCarritoByCliente(int idCliente)
    {
        var carrito = await _context.Carritos
            .Include(c => c.Cliente)
            .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(c => c.IdCliente == idCliente && c.Activo);

        if (carrito == null) return NotFound();
        return carrito;
    }

    // POST: api/Carritos
    [HttpPost]
    public async Task<ActionResult<Carrito>> PostCarrito(CarritoDto dto)
    {
        // Desactivar carrito anterior si existe
        var carritoAnterior = await _context.Carritos
            .FirstOrDefaultAsync(c => c.IdCliente == dto.IdCliente && c.Activo);

        if (carritoAnterior != null)
        {
            carritoAnterior.Activo = false;
            await _context.SaveChangesAsync();
        }

        // Crear nuevo carrito
        var carrito = new Carrito
        {
            IdCliente = dto.IdCliente,
            FechaCreacion = DateTime.Now,
            Activo = true,
        };

        _context.Carritos.Add(carrito);
        await _context.SaveChangesAsync();

        // Agregar detalles
        foreach (var detalle in dto.Detalles)
        {
            _context.CarritoDetalles.Add(new CarritoDetalle
            {
                IdCarrito = carrito.IdCarrito,
                IdProducto = detalle.IdProducto,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.PrecioUnitario,
            });
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCarritoByCliente),
            new { idCliente = carrito.IdCliente }, carrito);
    }

    // DELETE: api/Carritos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCarrito(int id)
    {
        var carrito = await _context.Carritos.FindAsync(id);
        if (carrito == null) return NotFound();
        carrito.Activo = false;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}