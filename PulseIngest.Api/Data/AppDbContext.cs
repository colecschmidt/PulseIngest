using Microsoft.EntityFrameworkCore;
using PulseIngest.Api.Models;

namespace PulseIngest.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Record> Records => Set<Record>();
}