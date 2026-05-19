using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientosInventarioController : ControllerBase
{
    private readonly AppDbContext _context;
    public MovimientosInventarioController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientos()
        => await _context.MovimientosInventario
            .Include(m => m.Producto)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();

    [HttpGet("producto/{idProducto}")]
    public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetPorProducto(int idProducto)
        => await _context.MovimientosInventario
            .Include(m => m.Producto)
            .Where(m => m.IdProducto == idProducto)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();

    [HttpPost]
    public async Task<ActionResult<MovimientoInventario>> PostMovimiento(MovimientoInventarioDto dto)
    {
        var movimiento = new MovimientoInventario
        {
            IdProducto = dto.IdProducto,
            Tipo = dto.Tipo,
            Cantidad = dto.Cantidad,
            Fecha = DateTime.Now
        };

        _context.MovimientosInventario.Add(movimiento);

        var inventario = await _context.Inventarios
            .FirstOrDefaultAsync(i => i.IdProducto == dto.IdProducto);

        if (inventario != null)
        {
            if (dto.Tipo == "COMPRA") inventario.Stock += dto.Cantidad;
            else if (dto.Tipo == "VENTA") inventario.Stock -= dto.Cantidad;
            else if (dto.Tipo == "AJUSTE") inventario.Stock = dto.Cantidad;
        }

        await _context.SaveChangesAsync();
        return Ok(movimiento);
    }
}