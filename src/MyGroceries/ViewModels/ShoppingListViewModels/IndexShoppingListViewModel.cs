using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.ShoppingListViewModels
{
    public class IndexShoppingListViewModel
    {
        [Required]
        [Display(Name = "Shopping List Name")]
        public string Name { get; set; }

        public List<ShoppingList> ShoppingLists;

        public IndexShoppingListViewModel() { }

        public IndexShoppingListViewModel(IEnumerable<ShoppingList> shoppingLists)
        {
            AddShoppingLists(shoppingLists);
        }

        public void AddShoppingLists(IEnumerable<ShoppingList> shoppingLists)
        {
            ShoppingLists = (List<ShoppingList>)shoppingLists;
        }
    }
}
