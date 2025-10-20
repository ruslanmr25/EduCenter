using System;
using Application.Abstracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options, IPasswordHasher passwordHasher)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSeeding(
                (context, _) =>
                {
                    var superAdmin = context
                        .Set<User>()
                        .FirstOrDefault(c => c.FullName == "Super Admin");

                    superAdmin ??= context
                        .Set<User>()
                        .Add(
                            new User
                            {
                                FullName = "Super Admin",
                                Username = "superAdmin",
                                Role = Role.SuperAdmin,
                                Password =
                                    "$2a$11$QbiQHQTv47aIbaXNhX.G3.QW3OzCU/AnTkK6EUmqFbxAzBN4J74V.",
                            }
                        )
                        .Entity;

                    var centerAdmin = context
                        .Set<User>()
                        .FirstOrDefault(c => c.FullName == "centerAdmin");

                    if (centerAdmin is null)
                    {
                        centerAdmin = context
                            .Set<User>()
                            .Add(
                                new User
                                {
                                    FullName = "Center Admin",
                                    Username = "centerAdmin",
                                    Role = Role.CenterAdmin,
                                    Password =
                                        "$2a$11$QbiQHQTv47aIbaXNhX.G3.QW3OzCU/AnTkK6EUmqFbxAzBN4J74V.",
                                }
                            )
                            .Entity;

                        context.SaveChanges();
                        context
                            .Set<Center>()
                            .Add(
                                new Center { Name = "O'quv markaz", CenterAdminId = centerAdmin.Id }
                            );
                    }

                    context.SaveChanges();
                }
            )
            .UseAsyncSeeding(
                async (context, _, cancellationToken) =>
                {
                    var superAdmin = await context
                        .Set<User>()
                        .FirstOrDefaultAsync(c => c.FullName == "Super Admin");

                    await context
                        .Set<User>()
                        .AddAsync(
                            new User
                            {
                                FullName = "Super Admin",
                                Username = "superAdmin",
                                Role = Role.SuperAdmin,
                                Password =
                                    "$2a$11$QbiQHQTv47aIbaXNhX.G3.QW3OzCU/AnTkK6EUmqFbxAzBN4J74V.",
                            }
                        );

                    var centerAdmin = await context
                        .Set<User>()
                        .FirstOrDefaultAsync(c => c.FullName == "centerAdmin");

                    if (centerAdmin is null)
                    {
                        centerAdmin = (
                            await context
                                .Set<User>()
                                .AddAsync(
                                    new User
                                    {
                                        FullName = "Center Admin",
                                        Username = "centerAdmin",
                                        Role = Role.CenterAdmin,
                                        Password =
                                            "$2a$11$QbiQHQTv47aIbaXNhX.G3.QW3OzCU/AnTkK6EUmqFbxAzBN4J74V.",
                                    }
                                )
                        ).Entity;
                        await context.SaveChangesAsync();

                        await context
                            .Set<Center>()
                            .AddAsync(
                                new Center { Name = "O'quv markaz", CenterAdminId = centerAdmin.Id }
                            );
                    }

                    await context.SaveChangesAsync();
                }
            );
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Center>()
            .HasOne(c => c.CenterAdmin)
            .WithOne(u => u.Center)
            .HasForeignKey<Center>(c => c.CenterAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        // Center ↔ Teacher (Many-to-Many)
        modelBuilder
            .Entity<Center>()
            .HasMany(c => c.Teachers)
            .WithMany(u => u.Centers)
            .UsingEntity<Dictionary<string, object>>(
                "CenterUser", // ko‘prik jadval nomi
                j =>
                    j.HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId") // aniq nom berildi
                        .OnDelete(DeleteBehavior.Cascade),
                j =>
                    j.HasOne<Center>()
                        .WithMany()
                        .HasForeignKey("CenterId") // aniq nom berildi
                        .OnDelete(DeleteBehavior.Cascade)
            );
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is BaseEntity
                && (e.State == EntityState.Added || e.State == EntityState.Modified)
            );

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;

            entity.UpdatedAt = DateTime.UtcNow.AddHours(5);
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow.AddHours(5);
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Center> Centers { get; set; }

    public DbSet<Science> Sciences { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Student> Students { get; set; }
}
