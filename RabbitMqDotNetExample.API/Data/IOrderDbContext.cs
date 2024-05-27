
using Microsoft.EntityFrameworkCore;

public interface IOrderDbContext
{
    DbSet<Order> Orders { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}