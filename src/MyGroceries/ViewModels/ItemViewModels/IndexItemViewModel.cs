using Microsoft.AspNetCore.Mvc.Rendering;
using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.ItemViewModels
{
    public class IndexItemViewModel
    {

        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Unit")]
        public int UnitID { get; set; }

        public List<Item> Items;
        public List<SelectListItem> Units { get; set; } = new List<SelectListItem>();

        public IndexItemViewModel() { }

        public IndexItemViewModel(IEnumerable<Item> items, IEnumerable<Unit> units)
        {
            AddItems(items, units);
        }

        public void AddItems(IEnumerable<Item> items, IEnumerable<Unit> units)
        {
            Items = (List<Item>)items;
            
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
