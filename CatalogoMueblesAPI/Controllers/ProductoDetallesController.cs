using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoDetallesController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductoDetallesController(AppDbContext context) => _context = context;

    // GET: api/ProductoDetalles/producto/1
    [HttpGet("producto/{idProducto}")]
    public async Task<ActionResult<ProductoDetalle>> GetPorProducto(int idProducto)
    {
        var detalle = await _context.ProductoDetalles
            .Include(pd => pd.Producto)
            .FirstOrDefaultAsync(pd => pd.IdProducto == idProducto);

        if (detalle == null) return NotFound();
        return detalle;
    }

    // POST: api/ProductoDetalles
    [HttpPost]
    public async Task<ActionResult<ProductoDetalle>> PostDetalle(ProductoDetalleDto dto)
    {
        var detalle = new ProductoDetalle
        {
            IdProducto = dto.IdProducto,
            Alto = dto.Alto,
            Ancho = dto.Ancho,
            Profundidad = dto.Profundidad,
            Material = dto.Material,
            Color = dto.Color,
            Duracion = dto.Duracion,
            Caracteristicas = dto.Caracteristicas
        };
        _context.ProductoDetalles.Add(detalle);
        await _context.SaveChangesAsync();
        return Ok(detalle);
    }

    // PUT: api/ProductoDetalles/1
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetalle(int id, ProductoDetalleDto dto)
    {
        var detalle = await _context.ProductoDetalles.FindAsync(id);
        if (detalle == null) return NotFound();

        detalle.Alto = dto.Alto;
        detalle.Ancho = dto.Ancho;
        detalle.Profundidad = dto.Profundidad;
        detalle.Material = dto.Material;
        detalle.Color = dto.Color;
        detalle.Duracion = dto.Duracion;
        detalle.Caracteristicas = dto.Caracteristicas;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}