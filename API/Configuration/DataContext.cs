using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Configuration;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<LovUser> User { get; set; }
}
