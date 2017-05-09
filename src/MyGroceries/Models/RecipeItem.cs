using Microsoft.EntityFrameworkCore;
using MyGroceries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.Models
{
    public class RecipeItem
    {
        public Recipe Recipe { get; set; }
        public int RecipeID { get; set; }

        public Item Item { get; set; }
        public int ItemID { get; set; }

        public double Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public RecipeItem() { }

        public RecipeItem(Item item,
            Recipe recipe,
            double quantity
            )
        {
            Item = item;
            Recipe = recipe;
            Quantity = quantity;
            DateCreated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
        }

        public void AddRecipeItemToContext(MyGroceriesDbContext context)
        {
            List<RecipeItem> existingRecipeItems = context
                .RecipeItems
                .Where(ri => ri.RecipeID == Recipe.ID)
                .Where(ri => ri.ItemID == Item.ID)
                .ToList();

            if (existingRecipeItems.Any())
            {
                existingRecipeItems[0].Quantity += Quantity;
            }
            else
            {
                context.RecipeItems.Add(this);
            }

            Recipe.DateUpdated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
            context.SaveChanges();
        }

    }

    
}
