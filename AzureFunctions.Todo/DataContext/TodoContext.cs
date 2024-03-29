﻿using AzureFunctions.Todo.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureFunctions.Todo.DataContext
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        { }

        public DbSet<Todos> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todos>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}