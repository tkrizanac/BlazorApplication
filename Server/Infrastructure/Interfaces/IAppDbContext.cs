using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Infrastructure.Interfaces;

public interface IAppDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
