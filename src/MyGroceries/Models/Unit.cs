using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.Models
{
    public class Unit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BaseUnit BaseUnit { get; set; }
        public double ConversionToBaseFactor { get; set; }

        // public IList<Item> Items { get; set; }
        //public IList<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();
        //public IList<RecipeItem> RecipeItems { get; set; } = new List<RecipeItem>();

        public double ConvertToBaseUnit(double quantity) {
            return quantity * ConversionToBaseFactor;
        }
        public double ConvertFromBaseUnit(double quantity)
        {
            return quantity / ConversionToBaseFactor;
        }
    }
}
