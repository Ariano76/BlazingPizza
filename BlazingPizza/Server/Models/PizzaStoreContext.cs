﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Shared;

namespace BlazingPizza.Server.Models
{
    public class PizzaStoreContext : DbContext
    {
        public DbSet<PizzaSpecial> Specials { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=PizzaStore;Trusted_Connection=True");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;database=PizzaStore;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir la llave primaria de la entidad PizzaTopping
            modelBuilder.Entity<PizzaTopping>()
                .HasKey(pst => new { pst.PizzaId, pst.ToppingId });
            // Una Pizza puede tener muchos Toppings.
            modelBuilder.Entity<PizzaTopping>()
                .HasOne<Pizza>().WithMany(ps => ps.Toppings);
            // Un Topping puede estar en muchas Pizzas.
            modelBuilder.Entity<PizzaTopping>()
                .HasOne(pst => pst.Topping).WithMany();                
        }
    }
}
