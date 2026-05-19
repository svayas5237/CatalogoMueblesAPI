using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;
    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioResponseDto>> Login(LoginDto dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);
        if (usuario == null)
            return Unauthorized(new { mensaje = "Credenciales incorrectas" });
        return Ok(new UsuarioResponseDto
        {
            IdUsuario = usuario.IdUsuario,
            Nombre = usuario.Nombre ?? "",
            Email = usuario.Email ?? "",
            Rol = usuario.Rol ?? "vendedor"
        });
    }

    [HttpPost("registro")]
    public async Task<ActionResult<UsuarioResponseDto>> Registro(UsuarioDto dto)
    {
        var existe = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
        if (existe)
            return BadRequest(new { mensaje = "El email ya está registrado" });

        // Buscar el rol en la tabla Rol para obtener el id_rol
        var rolEntity = await _context.Roles
            .FirstOrDefaultAsync(r => r.Nombre == dto.Rol);

        var usuario = new Usuario
        {
            Nombre = dto.Nombre,
            Email = dto.Email,
            Password = dto.Password,
            Rol = dto.Rol,
            IdRol = rolEntity?.IdRol
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return Ok(new UsuarioResponseDto
        {
            IdUsuario = usuario.IdUsuario,
            Nombre = usuario.Nombre ?? "",
            Email = usuario.Email ?? "",
            Rol = usuario.Rol ?? "vendedor"
        });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuarios()
    {
        var usuarios = await _context.Usuarios
            .Include(u => u.RolNavigation)
            .ToListAsync();
        return Ok(usuarios.Select(u => new UsuarioResponseDto
        {
            IdUsuario = u.IdUsuario,
            Nombre = u.Nombre ?? "",
            Email = u.Email ?? "",
            Rol = u.Rol ?? "vendedor"
        }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        var permisos = _context.UsuarioPermisos.Where(p => p.IdUsuario == id);
        _context.UsuarioPermisos.RemoveRange(permisos);
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}/rol")]
    public async Task<IActionResult> CambiarRol(int id, [FromBody] string rol)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        var rolEntity = await _context.Roles
            .FirstOrDefaultAsync(r => r.Nombre == rol);

        usuario.Rol = rol;
        usuario.IdRol = rolEntity?.IdRol;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, UsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        var rolEntity = await _context.Roles
            .FirstOrDefaultAsync(r => r.Nombre == dto.Rol);

        usuario.Nombre = dto.Nombre;
        usuario.Email = dto.Email;
        if (!string.IsNullOrEmpty(dto.Password))
            usuario.Password = dto.Password;
        usuario.Rol = dto.Rol;
        usuario.IdRol = rolEntity?.IdRol;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}