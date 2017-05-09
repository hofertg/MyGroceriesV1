using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyGroceries.Models;
using Microsoft.EntityFrameworkCore;

namespace MyGroceries.Data
{
    public class MyGroceriesDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeItem> RecipeItems { get; set;}
        public DbSet<Unit> Units { get; set; }

        public MyGroceriesDbContext(DbContextOptions<MyGroceriesDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingListItem>()
                .HasKey(c => new { c.ShoppingListID, c.ItemID });
            modelBuilder.Entity<RecipeItem>()
                .HasKey(c => new { c.RecipeID, c.ItemID });
        }

    }
}



