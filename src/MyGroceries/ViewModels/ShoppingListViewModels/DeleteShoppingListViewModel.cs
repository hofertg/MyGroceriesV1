using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.ShoppingListViewModels
{
    public class DeleteShoppingListViewModel
    {
        public List<ShoppingList> ShoppingLists;

        public DeleteShoppingListViewModel() { }

        public DeleteShoppingListViewModel(IEnumerable<ShoppingList> shoppingLists)
        {
            ShoppingLists = (List<ShoppingList>)shoppingLists;
        }

    }
}
