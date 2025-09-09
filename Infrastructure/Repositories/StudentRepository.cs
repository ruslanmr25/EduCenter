using System;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class StudentRepository : BaseRepository<Student>
{
    public StudentRepository(AppDbContext appDbContext)
        : base(appDbContext) { }
}
