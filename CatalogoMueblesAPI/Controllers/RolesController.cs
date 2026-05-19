using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly AppDbContext _context;
    public RolesController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        => await _context.Roles
            .Include(r => r.Permisos)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Rol>> GetRol(int id)
    {
        var rol = await _context.Roles
            .Include(r => r.Permisos)
            .FirstOrDefaultAsync(r => r.IdRol == id);
        if (rol == null) return NotFound();
        return rol;
    }

    [HttpPost]
    public async Task<ActionResult<Rol>> PostRol(RolDto dto)
    {
        var rol = new Rol
        {
            Nombre = dto.Nombre.ToLower().Trim(),
            Descripcion = dto.Descripcion,
        };
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();

        foreach (var permiso in dto.Permisos)
        {
            _context.RolPermisos.Add(new RolPermiso
            {
                IdRol = rol.IdRol,
                Permiso = permiso,
            });
        }
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRol), new { id = rol.IdRol }, rol);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRol(int id, RolDto dto)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null) return NotFound();

        rol.Nombre = dto.Nombre.ToLower().Trim();
        rol.Descripcion = dto.Descripcion;

        var anteriores = _context.RolPermisos.Where(p => p.IdRol == id);
        _context.RolPermisos.RemoveRange(anteriores);

        foreach (var permiso in dto.Permisos)
        {
            _context.RolPermisos.Add(new RolPermiso
            {
                IdRol = id,
                Permiso = permiso,
            });
        }
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRol(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol == null) return NotFound();

        if (rol.Nombre == "gerente" || rol.Nombre == "vendedor")
            return BadRequest(new { mensaje = "No se pueden eliminar los roles base del sistema" });

        var permisos = _context.RolPermisos.Where(p => p.IdRol == id);
        _context.RolPermisos.RemoveRange(permisos);
        _context.Roles.Remove(rol);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}