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
                                Password = "123456789",
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
                                    Password = "123456789",
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
            .WithMany(u => u.Centers)
            .HasForeignKey(c => c.CenterAdminId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Center>().HasMany(c => c.Teachers).WithMany();
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Center> Centers { get; set; }

    public DbSet<Science> Sciences { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Student> Students { get; set; }
}
