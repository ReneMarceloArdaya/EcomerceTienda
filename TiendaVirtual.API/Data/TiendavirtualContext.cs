using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using TiendaVirtual.API.Models;

namespace TiendaVirtual.API.Data;

public partial class TiendavirtualContext : DbContext
{
    public TiendavirtualContext()
    {
    }

    public TiendavirtualContext(DbContextOptions<TiendavirtualContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Barrio> Barrios { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Carritoproducto> Carritoproductos { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Clientenatural> Clientenatural { get; set; }

    public virtual DbSet<Credito> Creditos { get; set; }

    public virtual DbSet<Cuota> Cuota { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Detallepedido> Detallepedidos { get; set; }

    public virtual DbSet<Direccionenvio> Direccionenvios { get; set; }

    public virtual DbSet<Estadopedidohistorial> Estadopedidohistorials { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Juridico> Juridicos { get; set; }

    public virtual DbSet<Notificacionusuario> Notificacionusuarios { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Productotienda> Productotienda { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Proveedorproducto> Proveedorproductos { get; set; }

    public virtual DbSet<Tienda> Tienda { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Barrio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("barrio");

            entity.HasIndex(e => e.ZonaId, "ZonaId");

            entity.Property(e => e.NombreBarrio).HasMaxLength(50);

            entity.HasOne(d => d.Zona).WithMany(p => p.Barrios)
                .HasForeignKey(d => d.ZonaId)
                .HasConstraintName("barrio_ibfk_1");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carrito");

            entity.HasIndex(e => e.UsuarioId, "UsuarioId");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("carrito_ibfk_1");
        });

        modelBuilder.Entity<Carritoproducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carritoproducto");

            entity.HasIndex(e => e.CarritoId, "CarritoId");

            entity.HasIndex(e => e.ProductoId, "ProductoId");

            entity.Property(e => e.PrecioUnitario).HasPrecision(12, 2);

            entity.HasOne(d => d.Carrito).WithMany(p => p.Carritoproductos)
                .HasForeignKey(d => d.CarritoId)
                .HasConstraintName("carritoproducto_ibfk_1");

            entity.HasOne(d => d.Producto).WithMany(p => p.Carritoproductos)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("carritoproducto_ibfk_2");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.Descripcion).HasMaxLength(90);
            entity.Property(e => e.NombreCategoria).HasMaxLength(90);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.BarrioId, "BarrioId");

            entity.HasIndex(e => e.UsuarioId, "UsuarioId");

            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Genero).HasMaxLength(10);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.Barrio).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.BarrioId)
                .HasConstraintName("cliente_ibfk_1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("cliente_ibfk_2");
        });

        modelBuilder.Entity<Clientenatural>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clientenatural");

            entity.HasIndex(e => e.ClienteId, "ClienteId").IsUnique();

            entity.Property(e => e.ApellidoMaterno).HasMaxLength(50);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(50);
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);

            entity.HasOne(d => d.Cliente).WithOne(p => p.Clientenatural)
                .HasForeignKey<Clientenatural>(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clientenatural_ibfk_1");
        });

        modelBuilder.Entity<Credito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("credito");

            entity.HasIndex(e => e.IdPedido, "IdPedido");

            entity.Property(e => e.FechaDesembolso).HasColumnType("datetime");
            entity.Property(e => e.InteresMensual).HasPrecision(12, 2);
            entity.Property(e => e.Monto).HasPrecision(12, 2);

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Creditos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("credito_ibfk_1");
        });

        modelBuilder.Entity<Cuota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cuota");

            entity.HasIndex(e => e.IdCredito, "IdCredito");

            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.FechaPagoProgramado).HasColumnType("datetime");
            entity.Property(e => e.Interes).HasPrecision(12, 2);
            entity.Property(e => e.Monto).HasPrecision(12, 2);

            entity.HasOne(d => d.IdCreditoNavigation).WithMany(p => p.Cuota)
                .HasForeignKey(d => d.IdCredito)
                .HasConstraintName("cuota_ibfk_1");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("delivery");

            entity.HasIndex(e => e.PedidoId, "PedidoId");

            entity.Property(e => e.Nombre).HasMaxLength(90);
            entity.Property(e => e.Telefono).HasMaxLength(50);
            entity.Property(e => e.TelefonoDelyvery).HasMaxLength(50);

            entity.HasOne(d => d.Pedido).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("delivery_ibfk_1");
        });

        modelBuilder.Entity<Detallepedido>(entity =>
        {
            entity.HasKey(e => new { e.IdPedido, e.IdProducto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("detallepedido");

            entity.HasIndex(e => e.IdProducto, "IdProducto");

            entity.Property(e => e.Precio).HasPrecision(12, 2);

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Detallepedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallepedido_ibfk_1");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Detallepedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detallepedido_ibfk_2");
        });

        modelBuilder.Entity<Direccionenvio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("direccionenvio");

            entity.HasIndex(e => e.UsuarioId, "UsuarioId");

            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Departamento).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Latitud).HasPrecision(10, 8);
            entity.Property(e => e.Longitud).HasPrecision(11, 8);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Direccionenvios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("direccionenvio_ibfk_1");
        });

        modelBuilder.Entity<Estadopedidohistorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadopedidohistorial");

            entity.HasIndex(e => e.IdPedido, "IdPedido");

            entity.Property(e => e.EstadoAnterior).HasMaxLength(50);
            entity.Property(e => e.EstadoNuevo).HasMaxLength(50);
            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Estadopedidohistorials)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("estadopedidohistorial_ibfk_1");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("factura");

            entity.HasIndex(e => e.IdPedido, "IdPedido");

            entity.Property(e => e.CodigoControl).HasMaxLength(90);
            entity.Property(e => e.FechaEmision).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasPrecision(12, 2);
            entity.Property(e => e.Qr).HasMaxLength(255);

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("factura_ibfk_1");
        });

        modelBuilder.Entity<Juridico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("juridico");

            entity.HasIndex(e => e.ClienteId, "ClienteId").IsUnique();

            entity.Property(e => e.Id).HasMaxLength(50);
            entity.Property(e => e.RazonSocial).HasMaxLength(100);
            entity.Property(e => e.RepresentanteLegal).HasMaxLength(100);

            entity.HasOne(d => d.Cliente).WithOne(p => p.Juridico)
                .HasForeignKey<Juridico>(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("juridico_ibfk_1");
        });

        modelBuilder.Entity<Notificacionusuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notificacionusuario");

            entity.HasIndex(e => e.UsuarioId, "UsuarioId");

            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Leido).HasDefaultValueSql("'0'");
            entity.Property(e => e.Mensaje).HasMaxLength(255);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificacionusuarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("notificacionusuario_ibfk_1");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pago");

            entity.HasIndex(e => e.IdPedido, "IdPedido");

            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasPrecision(12, 2);
            entity.Property(e => e.TipoPago).HasMaxLength(50);

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pago_ibfk_1");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pedido");

            entity.HasIndex(e => e.DireccionEnvioId, "DireccionEnvioId");

            entity.HasIndex(e => e.IdCliente, "IdCliente");

            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Pendiente'");
            entity.Property(e => e.FechaPedido).HasColumnType("datetime");

            entity.HasOne(d => d.DireccionEnvio).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.DireccionEnvioId)
                .HasConstraintName("pedido_ibfk_2");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("pedido_ibfk_1");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.IdCategoria, "IdCategoria");

            entity.Property(e => e.Imagen).HasMaxLength(512);
            entity.Property(e => e.NombreProducto).HasMaxLength(90);
            entity.Property(e => e.PrecioVenta).HasPrecision(12, 2);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("producto_ibfk_1");
        });

        modelBuilder.Entity<Productotienda>(entity =>
        {
            entity.HasKey(e => new { e.IdProducto, e.IdTienda })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("productotienda");

            entity.HasIndex(e => e.IdTienda, "IdTienda");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Productotienda)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productotienda_ibfk_1");

            entity.HasOne(d => d.IdTiendaNavigation).WithMany(p => p.Productotienda)
                .HasForeignKey(d => d.IdTienda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productotienda_ibfk_2");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proveedor");

            entity.Property(e => e.Direccion).HasMaxLength(90);
            entity.Property(e => e.NombreProveedor).HasMaxLength(50);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Proveedorproducto>(entity =>
        {
            entity.HasKey(e => new { e.IdProveedor, e.IdProducto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("proveedorproducto");

            entity.HasIndex(e => e.IdProducto, "IdProducto");

            entity.Property(e => e.NombreEspecifico).HasMaxLength(50);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Proveedorproductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proveedorproducto_ibfk_2");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Proveedorproductos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proveedorproducto_ibfk_1");
        });

        modelBuilder.Entity<Tienda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tienda");

            entity.Property(e => e.Ciudad).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Latitud).HasPrecision(10, 8);
            entity.Property(e => e.Longitud).HasPrecision(11, 8);
            entity.Property(e => e.NombreTienda).HasMaxLength(80);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "Correo").IsUnique();

            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.TipoUsuario).HasMaxLength(20);
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("zona");

            entity.Property(e => e.NombreZona).HasMaxLength(90);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
