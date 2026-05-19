using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventariosController : ControllerBase
{
    private readonly AppDbContext _context;
    public InventariosController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inventario>>> GetInventarios()
        => await _context.Inventarios
            .Include(i => i.Producto)
                .ThenInclude(p => p.Categoria)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Inventario>> GetInventario(int id)
    {
        var inv = await _context.Inventarios
            .Include(i => i.Producto)
                .ThenInclude(p => p.Categoria)
            .FirstOrDefaultAsync(i => i.IdInventario == id);
        if (inv == null) return NotFound();
        return inv;
    }

    [HttpGet("producto/{idProducto}")]
    public async Task<ActionResult<Inventario>> GetPorProducto(int idProducto)
    {
        var inv = await _context.Inventarios
            .Include(i => i.Producto)
                .ThenInclude(p => p.Categoria)
            .FirstOrDefaultAsync(i => i.IdProducto == idProducto);
        if (inv == null) return NotFound();
        return inv;
    }

    [HttpGet("stock-bajo")]
    public async Task<ActionResult<IEnumerable<Inventario>>> GetStockBajo()
        => await _context.Inventarios
            .Include(i => i.Producto)
                .ThenInclude(p => p.Categoria)
            .Where(i => i.Stock <= i.StockMinimo)
            .ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Inventario>> PostInventario(InventarioDto dto)
    {
        var inventario = new Inventario
        {
            IdProducto = dto.IdProducto,
            Stock = dto.Stock,
            StockMinimo = dto.StockMinimo
        };
        _context.Inventarios.Add(inventario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInventario), new { id = inventario.IdInventario }, inventario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutInventario(int id, InventarioDto dto)
    {
        var inventario = await _context.Inventarios.FindAsync(id);
        if (inventario == null) return NotFound();
        inventario.Stock = dto.Stock;
        inventario.StockMinimo = dto.StockMinimo;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
