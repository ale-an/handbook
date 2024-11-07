using Microsoft.EntityFrameworkCore;
using Storage.Models;

namespace Storage.Logic;

public class ApplicationContext : DbContext
{
    public DbSet<Employee>? Employees { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}