using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly AppDbContext _context;
    public VentasController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        => await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .Include(v => v.Detalles).ThenInclude(d => d.Producto)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Venta>> GetVenta(int id)
    {
        var venta = await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .Include(v => v.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.IdVenta == id);
        if (venta == null) return NotFound();
        return venta;
    }

    [HttpPost]
    public async Task<ActionResult<Venta>> PostVenta(VentaDto dto)
    {
        var venta = new Venta
        {
            IdCliente = dto.IdCliente,
            IdUsuario = dto.IdUsuario,
            Fecha = DateTime.Now,
            Detalles = dto.Detalles.Select(d => new DetalleVenta
            {
                IdProducto = d.IdProducto,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList()
        };
        venta.Total = venta.Detalles.Sum(d => d.Subtotal);

        _context.Ventas.Add(venta);

        foreach (var detalle in venta.Detalles)
        {
            var inventario = await _context.Inventarios
                .FirstOrDefaultAsync(i => i.IdProducto == detalle.IdProducto);
            if (inventario != null)
            {
                inventario.Stock -= detalle.Cantidad;
                _context.MovimientosInventario.Add(new MovimientoInventario
                {
                    IdProducto = detalle.IdProducto,
                    Tipo = "VENTA",
                    Cantidad = detalle.Cantidad,
                    Fecha = DateTime.Now
                });
            }
        }

        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVenta), new { id = venta.IdVenta }, venta);
    }
}