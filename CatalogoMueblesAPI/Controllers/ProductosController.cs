using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductosController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        => await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Imagenes)
            .Include(p => p.Inventario)
            .Where(p => p.Activo)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Imagenes)
            .Include(p => p.Inventario)
            .FirstOrDefaultAsync(p => p.IdProducto == id);
        if (producto == null) return NotFound();
        return producto;
    }

    [HttpGet("categoria/{idCategoria}")]
    public async Task<ActionResult<IEnumerable<Producto>>> GetPorCategoria(int idCategoria)
        => await _context.Productos
            .Include(p => p.Imagenes)
            .Include(p => p.Inventario)
            .Where(p => p.IdCategoria == idCategoria && p.Activo)
            .ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(ProductoDto dto)
    {
        var producto = new Producto
        {
            IdCategoria = dto.IdCategoria,
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Activo = dto.Activo
        };
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducto), new { id = producto.IdProducto }, producto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, ProductoDto dto)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        producto.IdCategoria = dto.IdCategoria;
        producto.Nombre = dto.Nombre;
        producto.Descripcion = dto.Descripcion;
        producto.Precio = dto.Precio;
        producto.Activo = dto.Activo;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return NotFound();
        producto.Activo = false;
        await _context.SaveChangesAsync();
        return NoContent();
    }

   

   
}