using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.RecipeViewModels
{
    public class IndexRecipeViewModel
    {
        [Required]
        [Display(Name = "Recipe Name")]
        public string Name { get; set; }

        public List<Recipe> Recipes;

        public IndexRecipeViewModel() { }

        public IndexRecipeViewModel(IEnumerable<Recipe> recipes)
        {
            AddRecipes(recipes);
        }

        public void AddRecipes(IEnumerable<Recipe> recipes)
        {
            Recipes = (List<Recipe>)recipes;
        }


    }
}
