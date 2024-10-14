﻿using Microsoft.EntityFrameworkCore;
using gcapi.Models;
using gcapi.DataBaseModels;
using gcapi.Interfaces;

namespace gcapi.DataBase
{
    public class gContext : DbContext
    {
        public gContext(DbContextOptions<gContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EventModel> EventTable { get; set; }
        public DbSet<UserModel> UserTable { get; set; }
        public DbSet<GroupModel> GroupsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReactionModel>()
                .HasNoKey();
            modelBuilder.Entity<IEventStyle>()
                .HasNoKey();

            modelBuilder.Entity<EventModel>()
                .HasOne(e => e.Group)
                .WithMany(g => g.GroupEvents)
                .HasForeignKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}