using Microsoft.AspNetCore.Mvc.Rendering;
using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.RecipeViewModels
{
    public class AddEditRecipeViewModel
    {
        [Required]
        [Display(Name = "Recipe Name")]
        public string Name { get; set; }

        [Required]
        public string Directions { get; set; }

        public int RecipeID { get; set; }
        public IList<RecipeItem> Ingredients { get; set; }

        

        public AddEditRecipeViewModel() { }

        public AddEditRecipeViewModel(IEnumerable<RecipeItem> items)
        {
            AddItems(items);
        }

        public void AddItems(IEnumerable<RecipeItem> items)
        {
            Ingredients = (List<RecipeItem>)items;
        }
    }
}
