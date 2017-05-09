using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MyGroceries.ViewModels.ShoppingListViewModels
{
    public class ManageShoppingListViewModel
    {
        public int ShoppingListID { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public IList<ShoppingListItem> Items { get; set; }

        public List<SelectListItem> Actions { get; set; } = new List<SelectListItem>();

        public ManageShoppingListViewModel()
        {
            Actions.Add(new SelectListItem
            {
                Value = "strikethrough",
                Text = "Cross-off/Un-cross Items"
            });
            Actions.Add(new SelectListItem
            {
                Value = "delete",
                Text = "Delete Items"
            });
            //Actions.Add(new SelectListItem
            //{
            //    Value = "move",
            //    Text = "Move Items"
            //});
        }
    }
}
