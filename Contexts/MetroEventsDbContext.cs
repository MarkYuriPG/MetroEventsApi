using System;
using System.Collections.Generic;
using MetroEventsApi.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace MetroEventsApi.Contexts;

public partial class MetroEventsDbContext : DbContext
{
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserEvent> UserEvents { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }

    public MetroEventsDbContext()
    {
    }

    public MetroEventsDbContext(DbContextOptions<MetroEventsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEvent>()
            .HasKey(e => new { e.UserId, e.EventId });
    }

}
