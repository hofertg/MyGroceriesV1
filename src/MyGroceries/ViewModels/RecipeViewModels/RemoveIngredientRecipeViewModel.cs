using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.RecipeViewModels
{
    public class RemoveIngredientRecipeViewModel
    {
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public IList<RecipeItem> Items { get; set; }

    }
}
