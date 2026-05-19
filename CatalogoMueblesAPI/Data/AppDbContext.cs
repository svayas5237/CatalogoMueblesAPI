using CatalogoMueblesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMueblesAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Inventario> Inventarios { get; set; }
    public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
    public DbSet<ProductoImagen> ProductoImagenes { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<CarritoDetalle> CarritoDetalles { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }
    public DbSet<ProductoDetalle> ProductoDetalles { get; set; }
    public DbSet<ProductoDetalleImagen> ProductoDetalleImagenes { get; set; }
    public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<RolPermiso> RolPermisos { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Inventario → relación 1 a 1 con Producto
        modelBuilder.Entity<Inventario>()
            .HasIndex(i => i.IdProducto)
            .IsUnique();

        // Usuario email único
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // USUARIO
        modelBuilder.Entity<Usuario>()
            .Property(u => u.IdUsuario).HasColumnName("id_usuario");
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Nombre).HasColumnName("nombre");
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Email).HasColumnName("email");
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Password).HasColumnName("password");

        // CATEGORIA
        modelBuilder.Entity<Categoria>()
            .Property(c => c.IdCategoria).HasColumnName("id_categoria");
        modelBuilder.Entity<Categoria>()
            .Property(c => c.Nombre).HasColumnName("nombre");
        modelBuilder.Entity<Categoria>()
            .Property(c => c.Descripcion).HasColumnName("descripcion");

        // PRODUCTO
        modelBuilder.Entity<Producto>()
            .Property(p => p.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<Producto>()
            .Property(p => p.IdCategoria).HasColumnName("id_categoria");
        modelBuilder.Entity<Producto>()
            .Property(p => p.Nombre).HasColumnName("nombre");
        modelBuilder.Entity<Producto>()
            .Property(p => p.Descripcion).HasColumnName("descripcion");
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio).HasColumnName("precio");
        modelBuilder.Entity<Producto>()
            .Property(p => p.Activo).HasColumnName("activo");

        // INVENTARIO
        modelBuilder.Entity<Inventario>()
            .Property(i => i.IdInventario).HasColumnName("id_inventario");
        modelBuilder.Entity<Inventario>()
            .Property(i => i.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<Inventario>()
            .Property(i => i.Stock).HasColumnName("stock");
        modelBuilder.Entity<Inventario>()
            .Property(i => i.StockMinimo).HasColumnName("stock_minimo");

        // MOVIMIENTO INVENTARIO
        modelBuilder.Entity<MovimientoInventario>()
            .Property(m => m.IdMovimiento).HasColumnName("id_movimiento");
        modelBuilder.Entity<MovimientoInventario>()
            .Property(m => m.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<MovimientoInventario>()
            .Property(m => m.Tipo).HasColumnName("tipo");
        modelBuilder.Entity<MovimientoInventario>()
            .Property(m => m.Cantidad).HasColumnName("cantidad");
        modelBuilder.Entity<MovimientoInventario>()
            .Property(m => m.Fecha).HasColumnName("fecha");

        // PRODUCTO IMAGEN
        modelBuilder.Entity<ProductoImagen>()
            .Property(pi => pi.IdImagen).HasColumnName("id_imagen");
        modelBuilder.Entity<ProductoImagen>()
            .Property(pi => pi.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<ProductoImagen>()
            .Property(pi => pi.UrlImagen).HasColumnName("url_imagen");
        modelBuilder.Entity<ProductoImagen>()
            .Property(pi => pi.EsPrincipal).HasColumnName("es_principal");

        // CLIENTE
        modelBuilder.Entity<Cliente>()
            .Property(c => c.IdCliente).HasColumnName("id_cliente");
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Nombre).HasColumnName("nombre");
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Apellido).HasColumnName("apellido");
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Telefono).HasColumnName("telefono");
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Direccion).HasColumnName("direccion");
        modelBuilder.Entity<Cliente>()
            .Property(c => c.Correo).HasColumnName("correo");

        // CARRITO
        modelBuilder.Entity<Carrito>()
            .Property(c => c.IdCarrito).HasColumnName("id_carrito");
        modelBuilder.Entity<Carrito>()
            .Property(c => c.IdCliente).HasColumnName("id_cliente");
        modelBuilder.Entity<Carrito>()
            .Property(c => c.FechaCreacion).HasColumnName("fecha_creacion");
        modelBuilder.Entity<Carrito>()
            .Property(c => c.Activo).HasColumnName("activo");

        // CARRITO DETALLE
        modelBuilder.Entity<CarritoDetalle>()
            .Property(cd => cd.IdDetalle).HasColumnName("id_detalle");
        modelBuilder.Entity<CarritoDetalle>()
            .Property(cd => cd.IdCarrito).HasColumnName("id_carrito");
        modelBuilder.Entity<CarritoDetalle>()
            .Property(cd => cd.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<CarritoDetalle>()
            .Property(cd => cd.Cantidad).HasColumnName("cantidad");
        modelBuilder.Entity<CarritoDetalle>()
            .Property(cd => cd.PrecioUnitario).HasColumnName("precio_unitario");

        // VENTA
        modelBuilder.Entity<Venta>()
            .Property(v => v.IdVenta).HasColumnName("id_venta");
        modelBuilder.Entity<Venta>()
            .Property(v => v.IdCliente).HasColumnName("id_cliente");
        modelBuilder.Entity<Venta>()
            .Property(v => v.IdUsuario).HasColumnName("id_usuario");
        modelBuilder.Entity<Venta>()
            .Property(v => v.Fecha).HasColumnName("fecha");
        modelBuilder.Entity<Venta>()
            .Property(v => v.Total).HasColumnName("total");

        // DETALLE VENTA
        modelBuilder.Entity<DetalleVenta>()
            .Property(dv => dv.IdDetalle).HasColumnName("id_detalle");
        modelBuilder.Entity<DetalleVenta>()
            .Property(dv => dv.IdVenta).HasColumnName("id_venta");
        modelBuilder.Entity<DetalleVenta>()
            .Property(dv => dv.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<DetalleVenta>()
            .Property(dv => dv.Cantidad).HasColumnName("cantidad");
        modelBuilder.Entity<DetalleVenta>()
            .Property(dv => dv.PrecioUnitario).HasColumnName("precio_unitario");
        modelBuilder.Entity<DetalleVenta>()
            .Property(dv => dv.Subtotal).HasColumnName("subtotal");

        // PRODUCTO DETALLE
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.IdDetalleProducto).HasColumnName("id_detalle_producto");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.Alto).HasColumnName("alto");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.Ancho).HasColumnName("ancho");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.Profundidad).HasColumnName("profundidad");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.Material).HasColumnName("material");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.Color).HasColumnName("color");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.Duracion).HasColumnName("duracion");
        modelBuilder.Entity<ProductoDetalle>()
            .Property(pd => pd.Caracteristicas).HasColumnName("caracteristicas");

        // PRODUCTO DETALLE IMAGEN
        modelBuilder.Entity<ProductoDetalleImagen>()
            .Property(pdi => pdi.IdImagenDetalle).HasColumnName("id_imagen_detalle");
        modelBuilder.Entity<ProductoDetalleImagen>()
            .Property(pdi => pdi.IdProducto).HasColumnName("id_producto");
        modelBuilder.Entity<ProductoDetalleImagen>()
            .Property(pdi => pdi.UrlImagen).HasColumnName("url_imagen");
        modelBuilder.Entity<ProductoDetalleImagen>()
            .Property(pdi => pdi.EsPrincipal).HasColumnName("es_principal");
        modelBuilder.Entity<ProductoDetalleImagen>()
            .Property(pdi => pdi.Orden).HasColumnName("orden");
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Rol).HasColumnName("rol");

        modelBuilder.Entity<Categoria>()
            .Property(c => c.IdCategoriaPadre)
            .HasColumnName("id_categoria_padre");

        modelBuilder.Entity<Categoria>()
            .HasOne(c => c.CategoriaPadre)
            .WithMany(c => c.SubCategorias)
            .HasForeignKey(c => c.IdCategoriaPadre)
            .IsRequired(false);

        modelBuilder.Entity<Carrito>()
            .HasMany(c => c.Detalles)
            .WithOne(d => d.Carrito)
            .HasForeignKey(d => d.IdCarrito);

        modelBuilder.Entity<UsuarioPermiso>()
             .Property(u => u.IdUsuario)
             .HasColumnName("id_usuario");

        modelBuilder.Entity<UsuarioPermiso>()
            .Property(u => u.Permiso)
            .HasColumnName("permiso");

        modelBuilder.Entity<UsuarioPermiso>()
            .Property(u => u.IdPermiso)
            .HasColumnName("id_permiso");

        //ROL
        modelBuilder.Entity<Rol>()
            .Property(r => r.IdRol)
            .HasColumnName("id_rol");

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.RolNavigation)
            .WithMany()
            .HasForeignKey(u => u.IdRol)
            .IsRequired(false);

        modelBuilder.Entity<Usuario>()
            .Property(u => u.IdRol)
            .HasColumnName("id_rol");

        modelBuilder.Entity<Rol>()
            .Property(r => r.Nombre)
            .HasColumnName("nombre");

        modelBuilder.Entity<Rol>()
            .Property(r => r.Descripcion)
            .HasColumnName("descripcion");

        modelBuilder.Entity<RolPermiso>()
            .Property(r => r.IdRolPermiso)
            .HasColumnName("id_rol_permiso");

        modelBuilder.Entity<RolPermiso>()
            .Property(r => r.IdRol)
            .HasColumnName("id_rol");

        modelBuilder.Entity<RolPermiso>()
            .Property(r => r.Permiso)
            .HasColumnName("permiso");

        modelBuilder.Entity<Rol>()
            .HasMany(r => r.Permisos)
            .WithOne(p => p.Rol)
            .HasForeignKey(p => p.IdRol);
    }

}