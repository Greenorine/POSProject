using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POSProject.Models;

namespace POSProject.DataAccess
{
    public sealed class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<AppDbContext> logger;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor, ILogger<AppDbContext> logger) :
            base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseAudit && e.State is EntityState.Added or EntityState.Modified);
            
            foreach (var entityEntry in entries)
            {
                var entity = (BaseAudit) entityEntry.Entity;
                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = httpContextAccessor?.HttpContext?.User.Identity?.Name ?? "Admin";
                }
                else
                {
                    Entry(entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry(entity).Property(p => p.CreatedBy).IsModified = false;
                }
                
                entity.ModifiedAt = DateTime.UtcNow;
                entity.ModifiedBy =
                    httpContextAccessor?.HttpContext?.User.Identity?.Name ?? "Admin";
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}