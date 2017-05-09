using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.RecipeViewModels
{
    public class ViewRecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public IList<RecipeItem> Ingredients { get; set; }
    }
}
