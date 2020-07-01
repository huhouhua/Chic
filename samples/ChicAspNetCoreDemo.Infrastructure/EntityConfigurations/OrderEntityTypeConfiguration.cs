using ChicAspNetCoreDemo.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(q => q.Id);
            builder.ToTable("order");
            builder.Property(q => q.UserId).HasMaxLength(20).HasComment("");
            builder.Property(q => q.UserName).HasMaxLength(30);
            builder.OwnsOne(q => q.Address, a =>
            {
                a.WithOwner();
                a.Property(w => w.City).HasMaxLength(20);
                a.Property(w => w.Street).HasMaxLength(50);
                a.Property(w => w.ZipCode).HasMaxLength(10);
            });


        }

    }
}
