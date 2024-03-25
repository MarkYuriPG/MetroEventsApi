using System;
using System.Collections.Generic;
using MetroEventsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MetroEventsApi.Contexts;

public partial class MetroEventsDbContext : DbContext
{
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public MetroEventsDbContext()
    {
    }

    public MetroEventsDbContext(DbContextOptions<MetroEventsDbContext> options)
        : base(options)
    {
    }
}
