using Microsoft.AspNetCore.Mvc.Rendering;
using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.ShoppingListViewModels
{
    public class ViewShoppingListViewModel
    {
        public ShoppingList ShoppingList { get; set; }
        public IList<ShoppingListItem> Items { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required]
        public double Quantity { get; set; } = 1;

        [Required]
        [Display(Name = "Unit")]
        public int UnitID { get; set; }

        public List<SelectListItem> Units { get; set; } = new List<SelectListItem>();

        public ViewShoppingListViewModel() { }

        public ViewShoppingListViewModel(IEnumerable<ShoppingListItem> items, IEnumerable<Unit> units)
        {
            AddItems(items, units);
        }

        public void AddItems(IEnumerable<ShoppingListItem> items, IEnumerable<Unit> units)
        {
            Items = (List<ShoppingListItem>)items;

            foreach (Unit unit in units)
            {
                Units.Add(new SelectListItem
                {
                    Value = unit.ID.ToString(),
                    Text = unit.Name
                });
            }

        }
    }
}
