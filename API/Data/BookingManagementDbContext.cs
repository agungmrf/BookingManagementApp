using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class BookingManagementDbContext: DbContext
{
    public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options)
    {
        
    }
    
    // Untuk konfigurasi migrasi table database yang akan digunakan.
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>().HasIndex(e => e.Nik).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
        modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique();
        
        // Untuk menghubungkan table yang memiliki relasi.
        
        // One University has many Educations.
        modelBuilder.Entity<University>()
            .HasMany(e => e.Educations)
            .WithOne(u => u.University)
            .HasForeignKey(e => e.UniversityGuid);

        // One Education has one Employee. 
        modelBuilder.Entity<Education>()
            .HasOne(e => e.Employee)
            .WithOne(e => e.Education)
            .HasForeignKey<Education>(e => e.Guid);
        
        // One Account has many AccountRoles.
        modelBuilder.Entity<Account>()
            .HasMany(ar => ar.AccountRoles)
            .WithOne(a => a.Account)
            .HasForeignKey(ar => ar.AccountGuid);
        
        // One Role has many AccountRoles.
        modelBuilder.Entity<Role>()
            .HasMany(ar => ar.AccountRoles)
            .WithOne(r => r.Role)
            .HasForeignKey(ar => ar.RoleGuid);
        
        // One Room has many Bookings.
        modelBuilder.Entity<Room>()
            .HasMany(b => b.Bookings)
            .WithOne(r => r.Room)
            .HasForeignKey(b => b.RoomGuid);
        
        // One Employee has many Bookings.
        modelBuilder.Entity<Employee>()
            .HasMany(b => b.Bookings)
            .WithOne(e => e.Employee)
            .HasForeignKey(b => b.EmployeeGuid);
        
        // One Account has one Employee.
        /*modelBuilder.Entity<Account>()
            .HasOne(e => e.Employee)
            .WithOne(e => e.Account)
            .HasForeignKey<Account>(e => e.Guid);*/
        
        // One Employee has one Account
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Account)
            .WithOne(e => e.Employee)
            .HasForeignKey<Account>(e => e.Guid);
    }
}