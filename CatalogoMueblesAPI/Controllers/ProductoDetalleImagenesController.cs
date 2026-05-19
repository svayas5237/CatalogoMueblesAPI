using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoDetalleImagenesController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductoDetalleImagenesController(AppDbContext context) => _context = context;

    // GET: api/ProductoDetalleImagenes/producto/1
    [HttpGet("producto/{idProducto}")]
    public async Task<ActionResult<IEnumerable<ProductoDetalleImagen>>> GetPorProducto(int idProducto)
    {
        return await _context.ProductoDetalleImagenes
            .Where(i => i.IdProducto == idProducto)
            .OrderBy(i => i.Orden)
            .ToListAsync();
    }

    // POST: api/ProductoDetalleImagenes
    [HttpPost]
    public async Task<ActionResult<ProductoDetalleImagen>> PostImagen(ProductoDetalleImagenDto dto)
    {
        var imagen = new ProductoDetalleImagen
        {
            IdProducto = dto.IdProducto,
            UrlImagen = dto.UrlImagen,
            EsPrincipal = dto.EsPrincipal,
            Orden = dto.Orden
        };
        _context.ProductoDetalleImagenes.Add(imagen);
        await _context.SaveChangesAsync();
        return Ok(imagen);
    }

    // DELETE: api/ProductoDetalleImagenes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImagen(int id)
    {
        var imagen = await _context.ProductoDetalleImagenes.FindAsync(id);
        if (imagen == null) return NotFound();
        _context.ProductoDetalleImagenes.Remove(imagen);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}