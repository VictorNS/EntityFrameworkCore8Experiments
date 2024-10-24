﻿using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data;

public partial class MyContext : DbContext
{
	public MyContext()
	{
	}

	public MyContext(DbContextOptions<MyContext> options) : base(options)
	{
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
#if DEBUG
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer("server=(local); database=MyContext; Integrated Security=true; Encrypt=false");
		}
#endif
	}

	public virtual DbSet<Order> Orders { get; set; } = null!;
	public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
	public virtual DbSet<OrderRequest> OrderRequests { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<OrderItem>(entity =>
		{
			entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
			entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

			entity.HasOne(d => d.Order).WithMany(p => p.Items)
				.HasForeignKey(d => d.OrderId)
				.OnDelete(DeleteBehavior.Restrict)
				.HasConstraintName("FK_OrderItems_Orders");
		});

		modelBuilder.Entity<OrderRequest>(entity =>
		{
			entity.Property(e => e.ExpectedRevenue).HasColumnType("decimal(20, 4)");
			entity.OwnsOne(e => e.MetaData, builder =>
			{
				builder.ToJson();
				builder.OwnsOne(m => m.Contact);
			});
		});

		FunctionMapping(modelBuilder);
	}

	#region functions
	public decimal GetOrderRevenue(int orderId)
			=> throw new NotImplementedException();

	protected void FunctionMapping(ModelBuilder modelBuilder)
	{
		var dbFunction = typeof(MyContext).GetRuntimeMethod(nameof(GetOrderRevenue), [typeof(int)])
			?? throw new ArgumentException($"GetOrderRevenue is expected");

		modelBuilder
			.HasDbFunction(dbFunction)
			.HasName("ufn_GetOrderRevenue");
	}
	#endregion functions
}
