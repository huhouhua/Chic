using Chic.Infrastructure.Core;
using ChicAspNetCoreDemo.Domain.OrderAggregate;
using ChicAspNetCoreDemo.Infrastructure.EntityConfigurations;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Infrastructure
{
    public class OrderingContext : EFContext
    {
        //public OrderingContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        //{
        //}

        public OrderingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 注册领域模型与数据库的映射关系

            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());

            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
