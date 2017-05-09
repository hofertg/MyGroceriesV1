using Microsoft.AspNetCore.Mvc.Rendering;
using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.RecipeViewModels
{
    public class AddToShoppingListRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public IList<RecipeItem> Items { get; set; }

        [Display(Name = "Add to: ")]
        public int ShoppingListID { get; set; }

        public List<SelectListItem> ShoppingLists { get; set; } = new List<SelectListItem>();

        public AddToShoppingListRecipeViewModel() { }

        public AddToShoppingListRecipeViewModel(IEnumerable<RecipeItem> items, IEnumerable<ShoppingList> shoppingLists)
        {
            AddItems(items, shoppingLists);
        }

        public void AddItems(IEnumerable<RecipeItem> items, IEnumerable<ShoppingList> shoppingLists)
        {
            Items = (List<RecipeItem>)items;

            foreach (ShoppingList shoppingList in shoppingLists)
            {
                ShoppingLists.Add(new SelectListItem
                {
                    Value = shoppingList.ID.ToString(),
                    Text = shoppingList.Name
                });
            }

        }
    }
}
