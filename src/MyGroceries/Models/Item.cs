using Microsoft.EntityFrameworkCore;
using MyGroceries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Unit Unit { get; set; }
        public int UnitID { get; set; }

        //public double PricePerBaseUnit { get; set; }
        //public double PricePerUnit { get; set; }

        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public IList<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();
        public IList<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();

        public Item() { }

        public Item(string name, Unit unit)
        {
            Name = name;
            Unit = unit;
            CreatedBy = "admin";
            DateCreated = DateTime.UtcNow;
        }

        public void AddItemToContext(MyGroceriesDbContext context)
        {

            List<Item> existingItems = context
                .Items
                .Where(i => i.Name == Name && i.UnitID == Unit.ID)
                .Include(i => i.Unit)
                .ToList();
            if (! existingItems.Any())
            {
                context.Items.Add(this);
                context.SaveChanges();
            }

            return;
        }
    }
}
