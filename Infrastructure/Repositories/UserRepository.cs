using System;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(AppDbContext appDbContext)
        : base(appDbContext) { }
}
