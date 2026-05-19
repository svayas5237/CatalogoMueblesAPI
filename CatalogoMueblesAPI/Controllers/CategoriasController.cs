using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriasController(AppDbContext context) => _context = context;

    // GET: api/Categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        => await _context.Categorias
            .Include(c => c.CategoriaPadre)
            .Include(c => c.SubCategorias)
            .ToListAsync();

    // GET: api/Categorias/padres
    [HttpGet("padres")]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasPadre()
        => await _context.Categorias
            .Include(c => c.SubCategorias)
            .Where(c => c.IdCategoriaPadre == null)
            .ToListAsync();

    // GET: api/Categorias/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.CategoriaPadre)
            .Include(c => c.SubCategorias)
            .FirstOrDefaultAsync(c => c.IdCategoria == id);
        if (categoria == null) return NotFound();
        return categoria;
    }

    // POST: api/Categorias
    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(CategoriaDto dto)
    {
        var categoria = new Categoria
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            IdCategoriaPadre = dto.IdCategoriaPadre
        };
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategoria), new { id = categoria.IdCategoria }, categoria);
    }

    // PUT: api/Categorias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(int id, CategoriaDto dto)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();
        categoria.Nombre = dto.Nombre;
        categoria.Descripcion = dto.Descripcion;
        categoria.IdCategoriaPadre = dto.IdCategoriaPadre;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/Categorias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}