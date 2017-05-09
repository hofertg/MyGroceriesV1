using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.ItemViewModels
{
    public class DeleteItemViewModel
    {
        public List<Item> Items;

        public DeleteItemViewModel() { }

        public DeleteItemViewModel(IEnumerable<Item> items)
        {
            Items = (List<Item>)items;
        }
    }
}
