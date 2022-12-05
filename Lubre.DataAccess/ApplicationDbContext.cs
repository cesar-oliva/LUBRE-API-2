using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Lubre.Entities;

namespace Lubre.DataAccess;

public class ApplicationDbContext : IdentityDbContext 
{   
    /// <summary>
    /// Create the database context for each of the objects we want
    ///that they persist
    /// </summary> 
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Position> Positions { get; set; }

    /// <summary>
    /// builder apiDbContext
    /// </summary>
    /// <remarks>
    /// the constructor receives a dbcontextoptions object as parameters.This object s necessary to be able 
    /// to obtain the configuration and options that the entity framework needs.The Dbcontext class, which 
    /// is the parent class of this hierarchy, is passed through the constructor.
    /// </remarks>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    /// <summary>
    /// override OModelCreating
    /// </summary>
    /// <remarks>
    ///we override OModelCreating this operation allows us to configure what we want to happen when the database model 
    ///is created when executing the application and creating the migration To prevent tables from being created that we don't need
    /// </remarks>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Entity>();     
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfiguration(new EmployeeConfigurations());
        //modelBuilder.ApplyConfiguration(new GenderConfigurations());

    }

}
