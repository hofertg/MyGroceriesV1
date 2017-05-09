using Microsoft.EntityFrameworkCore;
using MyGroceries.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.Models
{
    public class ShoppingListItem
    {
        public ShoppingList ShoppingList { get; set; }
        public int ShoppingListID { get; set; }

        public Item Item { get; set; }
        public int ItemID { get; set; }

        public double Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool StrikeThrough { get; set; }

        public ShoppingListItem() { }

        public ShoppingListItem(Item item,
            ShoppingList shoppingList,
            double quantity
            )
        {
            Item = item;
            ShoppingList = shoppingList;
            Quantity = quantity;
            StrikeThrough = false;
            DateCreated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
        }

        public void AddShoppingListItemToContext(MyGroceriesDbContext context)
        {
            BaseUnit baseUnit = context.Units.Where(u => u.ID == Item.UnitID).ToList()[0].BaseUnit;

            List<ShoppingListItem> existingShoppingListItems = context
                .ShoppingListItems
                .Where(sl => sl.ShoppingListID == ShoppingList.ID)
                .Include(sl => sl.Item)
                .Where(sl => sl.Item.Name == Item.Name)
                .Include(sl => sl.Item.Unit)
                .Where(sl => sl.Item.Unit.BaseUnit == baseUnit)
                .ToList();

            if (existingShoppingListItems.Any())
            {
                //if(existingShoppingListItems[0].StrikeThrough == true)
                //{
                //    context.ShoppingListItems.Remove(existingShoppingListItems[0]);
                //    context.ShoppingListItems.Add(this);
                //}
                //else
                {

                    if (existingShoppingListItems[0].Item.Unit.ConversionToBaseFactor >= Item.Unit.ConversionToBaseFactor)
                    {
                        existingShoppingListItems[0].Quantity += existingShoppingListItems[0]
                            .Item
                            .Unit
                            .ConvertFromBaseUnit(Item.Unit.ConvertToBaseUnit(Quantity));
                        existingShoppingListItems[0].DateUpdated = DateTime.UtcNow;

                    }
                    else
                    {
                        Quantity += Item.Unit
                            .ConvertFromBaseUnit(existingShoppingListItems[0]
                            .Item
                            .Unit
                            .ConvertToBaseUnit(existingShoppingListItems[0].Quantity));
                        context.ShoppingListItems.Remove(existingShoppingListItems[0]);
                        context.ShoppingListItems.Add(this);
                    }

                }
            }
            else
            {
                context.ShoppingListItems.Add(this);
            }

            ShoppingList.DateUpdated = DateTime.UtcNow;
            DateUpdated = DateTime.UtcNow;
            context.SaveChanges();
        }
    }
}
