﻿using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public partial class ApiGardensContext : DbContext
{
    public ApiGardensContext()
    {
    }

    public ApiGardensContext(DbContextOptions<ApiGardensContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<GamaProducto> GamaProductos { get; set; }

    public virtual DbSet<Oficina> Oficinas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        /* #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263. */
        => optionsBuilder.UseMySql("server=localhost;user=root;password=123456;database=gardensdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CodigoCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.CodigoEmpleadoRepVentas, "FK_cliente_codigo_empleado_rep_ventas_empleado_codigo_empleado");

            entity.Property(e => e.CodigoCliente)
                .ValueGeneratedNever()
                .HasColumnName("codigo_cliente");
            entity.Property(e => e.ApellidoContacto)
                .HasMaxLength(30)
                .HasColumnName("apellido_contacto");
            entity.Property(e => e.Ciudad)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoEmpleadoRepVentas).HasColumnName("codigo_empleado_rep_ventas");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Fax)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("fax");
            entity.Property(e => e.LimiteCredito)
                .HasPrecision(15, 2)
                .HasColumnName("limite_credito");
            entity.Property(e => e.LineaDireccion1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("linea_direccion1");
            entity.Property(e => e.LineaDireccion2)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion2");
            entity.Property(e => e.NombreCliente)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(30)
                .HasColumnName("nombre_contacto");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .HasColumnName("pais");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("telefono");

            entity.HasOne(d => d.CodigoEmpleadoRepVentasNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CodigoEmpleadoRepVentas)
                .HasConstraintName("FK_cliente_codigo_empleado_rep_ventas_empleado_codigo_empleado");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => new { e.CodigoPedido, e.CodigoProducto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("detalle_pedido");

            entity.HasIndex(e => e.CodigoProducto, "FK_detalle_pedido_codigo_producto_producto_codigo_producto");

            entity.Property(e => e.CodigoPedido).HasColumnName("codigo_pedido");
            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(15)
                .HasColumnName("codigo_producto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.NumeroLinea).HasColumnName("numero_linea");
            entity.Property(e => e.PrecioUnidad)
                .HasPrecision(15, 2)
                .HasColumnName("precio_unidad");

            entity.HasOne(d => d.CodigoPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodigoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalle_pedido_codigo_pedido_pedido_codigo_pedido");

            entity.HasOne(d => d.CodigoProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodigoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_detalle_pedido_codigo_producto_producto_codigo_producto");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.CodigoEmpleado).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.CodigoJefe, "FK_empleado_codigo_jefe_empleado_codigo_empleado");

            entity.HasIndex(e => e.CodigoOficina, "FK_empleado_codigo_oficina_oficina_codigo_oficina");

            entity.Property(e => e.CodigoEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("codigo_empleado");
            entity.Property(e => e.Apellido1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .HasColumnName("apellido2");
            entity.Property(e => e.CodigoJefe).HasColumnName("codigo_jefe");
            entity.Property(e => e.CodigoOficina)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("codigo_oficina");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .HasColumnName("puesto");

            entity.HasOne(d => d.CodigoJefeNavigation).WithMany(p => p.InverseCodigoJefeNavigation)
                .HasForeignKey(d => d.CodigoJefe)
                .HasConstraintName("FK_empleado_codigo_jefe_empleado_codigo_empleado");

            entity.HasOne(d => d.CodigoOficinaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CodigoOficina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleado_codigo_oficina_oficina_codigo_oficina");
        });

        modelBuilder.Entity<GamaProducto>(entity =>
        {
            entity.HasKey(e => e.Gama).HasName("PRIMARY");

            entity.ToTable("gama_producto");

            entity.Property(e => e.Gama)
                .HasMaxLength(50)
                .HasColumnName("gama");
            entity.Property(e => e.DescripcionHtml)
                .HasColumnType("text")
                .HasColumnName("descripcion_html");
            entity.Property(e => e.DescripcionTexto)
                .HasColumnType("text")
                .HasColumnName("descripcion_texto");
            entity.Property(e => e.Imagen)
                .HasMaxLength(256)
                .HasColumnName("imagen");
        });

        modelBuilder.Entity<Oficina>(entity =>
        {
            entity.HasKey(e => e.CodigoOficina).HasName("PRIMARY");

            entity.ToTable("oficina");

            entity.Property(e => e.CodigoOficina)
                .HasMaxLength(10)
                .HasColumnName("codigo_oficina");
            entity.Property(e => e.Ciudad)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.LineaDireccion1)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("linea_direccion1");
            entity.Property(e => e.LineaDireccion2)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion2");
            entity.Property(e => e.Pais)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("pais");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => new { e.CodigoCliente, e.IdTransaccion })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("pago");

            entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");
            entity.Property(e => e.IdTransaccion)
                .HasMaxLength(50)
                .HasColumnName("id_transaccion");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.FormaPago)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("forma_pago");
            entity.Property(e => e.Total)
                .HasPrecision(15, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.CodigoClienteNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.CodigoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_codigo_cliente_cliente_codigo_cliente");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.CodigoPedido).HasName("PRIMARY");

            entity.ToTable("pedido");

            entity.HasIndex(e => e.CodigoCliente, "FK_pedido_codigo_cliente_cliente_codigo_cliente");

            entity.Property(e => e.CodigoPedido)
                .ValueGeneratedNever()
                .HasColumnName("codigo_pedido");
            entity.Property(e => e.CodigoCliente).HasColumnName("codigo_cliente");
            entity.Property(e => e.Comentarios)
                .HasColumnType("text")
                .HasColumnName("comentarios");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega");
            entity.Property(e => e.FechaEsperada).HasColumnName("fecha_esperada");
            entity.Property(e => e.FechaPedido).HasColumnName("fecha_pedido");

            entity.HasOne(d => d.CodigoClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CodigoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedido_codigo_cliente_cliente_codigo_cliente");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.CodigoProducto).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.Gama, "FK_producto_gama_gama_producto_gama");

            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(15)
                .HasColumnName("codigo_producto");
            entity.Property(e => e.CantidadEnStock).HasColumnName("cantidad_en_stock");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Dimensiones)
                .HasMaxLength(25)
                .HasColumnName("dimensiones");
            entity.Property(e => e.Gama)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("gama");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(70)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioProveedor)
                .HasPrecision(15, 2)
                .HasColumnName("precio_proveedor");
            entity.Property(e => e.PrecioVenta)
                .HasPrecision(15, 2)
                .HasColumnName("precio_venta");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .HasColumnName("proveedor");

            entity.HasOne(d => d.GamaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Gama)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_producto_gama_gama_producto_gama");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
