using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoImagenesController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductoImagenesController(AppDbContext context) => _context = context;

    [HttpGet("producto/{idProducto}")]
    public async Task<ActionResult<IEnumerable<ProductoImagen>>> GetPorProducto(int idProducto)
        => await _context.ProductoImagenes
            .Where(i => i.IdProducto == idProducto)
            .ToListAsync();

    [HttpPost]
    public async Task<ActionResult<ProductoImagen>> PostImagen(ProductoImagenDto dto)
    {
        var imagen = new ProductoImagen
        {
            IdProducto = dto.IdProducto,
            UrlImagen = dto.UrlImagen,
            EsPrincipal = dto.EsPrincipal
        };
        _context.ProductoImagenes.Add(imagen);
        await _context.SaveChangesAsync();
        return Ok(imagen);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImagen(int id)
    {
        var imagen = await _context.ProductoImagenes.FindAsync(id);
        if (imagen == null) return NotFound();
        _context.ProductoImagenes.Remove(imagen);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}