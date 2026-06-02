using CatalogoMueblesAPI.Data;
using CatalogoMueblesAPI.DTOs;
using CatalogoMueblesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace CatalogoMueblesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly AppDbContext _context;

    public VentasController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venta>>> GetVentas()
        => await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .Include(v => v.Detalles).ThenInclude(d => d.Producto)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Venta>> GetVenta(int id)
    {
        var venta = await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Usuario)
            .Include(v => v.Detalles).ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(v => v.IdVenta == id);

        if (venta == null) return NotFound();

        return venta;
    }

    [HttpGet("mis-compras/{correo}")]
    public async Task<ActionResult<IEnumerable<object>>> GetMisComprasSeguro(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
        {
            return BadRequest(new { mensaje = "El correo del usuario es requerido." });
        }

        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Correo == correo);

        if (cliente == null)
        {
            return Ok(new List<object>());
        }

        var ventas = await _context.Ventas
            .Where(v => v.IdCliente == cliente.IdCliente)
            .OrderByDescending(v => v.Fecha)
            .Select(v => new
            {
                v.IdVenta,
                v.Fecha,
                v.Total,
                v.EnvioNombre,
                v.EnvioApellido,
                v.EnvioDireccion,
                v.EnvioTelefono,
                Detalles = v.Detalles.Select(d => new
                {
                    d.Cantidad,
                    d.PrecioUnitario,
                    d.Subtotal,
                    Producto = new
                    {
                        d.Producto.IdProducto,
                        d.Producto.Nombre,
                        DetallesTecnicos = _context.ProductoDetalles
                            .Where(pd => pd.IdProducto == d.IdProducto)
                            .Select(pd => new {
                                pd.Alto,
                                pd.Ancho,
                                pd.Profundidad,
                                pd.Color,
                                pd.Caracteristicas
                            }).FirstOrDefault(),
                        Imagen = _context.ProductoImagenes
                            .Where(pi => pi.IdProducto == d.IdProducto && pi.EsPrincipal)
                            .Select(pi => pi.UrlImagen).FirstOrDefault()
                    }
                })
            })
            .ToListAsync();

        if (!ventas.Any())
        {
            return Ok(new List<object>());
        }

        return Ok(ventas);
    }

    [HttpPost]
    public async Task<ActionResult<Venta>> PostVenta(VentaDto dto)
    {
        var venta = new Venta
        {
            IdCliente = dto.IdCliente,
            IdUsuario = dto.IdUsuario,
            Fecha = DateTime.Now,

            FacturacionNombre = dto.FacturacionNombre,
            FacturacionApellido = dto.FacturacionApellido,
            FacturacionCedula = dto.FacturacionCedula,
            FacturacionTelefono = dto.FacturacionTelefono,
            FacturacionDireccion = dto.FacturacionDireccion,

            EnvioNombre = dto.EnvioNombre,
            EnvioApellido = dto.EnvioApellido,
            EnvioTelefono = dto.EnvioTelefono,
            EnvioDireccion = dto.EnvioDireccion,
            EnvioLinkMaps = dto.EnvioLinkMaps,

            UrlComprobante = dto.UrlComprobante,

            Detalles = dto.Detalles.Select(d => new DetalleVenta
            {
                IdProducto = d.IdProducto,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList()
        };

        venta.Total = venta.Detalles.Sum(d => d.Subtotal);

        _context.Ventas.Add(venta);

        foreach (var detalle in venta.Detalles)
        {
            var inventario = await _context.Inventarios
                .FirstOrDefaultAsync(i => i.IdProducto == detalle.IdProducto);
            if (inventario != null)
            {
                inventario.Stock -= detalle.Cantidad;
                _context.MovimientosInventario.Add(new MovimientoInventario
                {
                    IdProducto = detalle.IdProducto,
                    Tipo = "VENTA",
                    Cantidad = detalle.Cantidad,
                    Fecha = DateTime.Now
                });
            }
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVenta), new { id = venta.IdVenta }, venta);
    }
}