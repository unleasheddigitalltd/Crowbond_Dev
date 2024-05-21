﻿using System.Data.Common;
using Crowbond.Modules.Ticketing.Application.Abstractions.Data;
using Crowbond.Modules.Ticketing.Domain.Customers;
using Crowbond.Modules.Ticketing.Domain.Events;
using Crowbond.Modules.Ticketing.Domain.Orders;
using Crowbond.Modules.Ticketing.Domain.Payments;
using Crowbond.Modules.Ticketing.Domain.Tickets;
using Crowbond.Common.Infrastructure.Inbox;
using Crowbond.Common.Infrastructure.Outbox;
using Crowbond.Modules.Ticketing.Infrastructure.Customers;
using Crowbond.Modules.Ticketing.Infrastructure.Events;
using Crowbond.Modules.Ticketing.Infrastructure.Orders;
using Crowbond.Modules.Ticketing.Infrastructure.Payments;
using Crowbond.Modules.Ticketing.Infrastructure.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Crowbond.Modules.Ticketing.Infrastructure.Database;

public sealed class TicketingDbContext(DbContextOptions<TicketingDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Customer> Customers { get; set; }

    internal DbSet<Event> Events { get; set; }

    internal DbSet<TicketType> TicketTypes { get; set; }

    internal DbSet<Order> Orders { get; set; }

    internal DbSet<OrderItem> OrderItems { get; set; }

    internal DbSet<Ticket> Tickets { get; set; }

    internal DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Ticketing);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new TicketConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
