using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioPermisosController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsuarioPermisosController(AppDbContext context) => _context = context;

    // GET: api/UsuarioPermisos/usuario/2
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<IEnumerable<string>>> GetPermisos(int idUsuario)
    {
        var permisos = await _context.UsuarioPermisos
            .Where(p => p.IdUsuario == idUsuario)
            .Select(p => p.Permiso)
            .ToListAsync();
        return Ok(permisos);
    }

    // POST: api/UsuarioPermisos/usuario/2
    [HttpPost("usuario/{idUsuario}")]
    public async Task<IActionResult> GuardarPermisos(int idUsuario, [FromBody] List<string> permisos)
    {
        // Eliminar permisos anteriores
        var anteriores = _context.UsuarioPermisos.Where(p => p.IdUsuario == idUsuario);
        _context.UsuarioPermisos.RemoveRange(anteriores);

        // Agregar nuevos permisos
        foreach (var permiso in permisos)
        {
            _context.UsuarioPermisos.Add(new UsuarioPermiso
            {
                IdUsuario = idUsuario,
                Permiso = permiso,
            });
        }

        await _context.SaveChangesAsync();
        return Ok(new { mensaje = "Permisos guardados correctamente" });
    }
}