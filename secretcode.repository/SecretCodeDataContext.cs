﻿using Microsoft.EntityFrameworkCore;
using SecretCode.Model;

namespace SecretCode.Repository;

public class SecretCodeDataContext: DbContext
{
    #region Constructors
    public SecretCodeDataContext(DbContextOptions<SecretCodeDataContext> options): base(options)
    {
    }

    #endregion

    #region Entities
    DbSet<User> Users { get; set; }
    #endregion

    #region Override Methods
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .Property(_ => _.Name)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .HasKey(_ => _.Id);
    }
    #endregion    
}


