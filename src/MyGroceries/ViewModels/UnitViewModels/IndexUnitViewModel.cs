using Microsoft.AspNetCore.Mvc.Rendering;
using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.UnitViewModels
{
    public class IndexUnitViewModel
    {
        [Required]
        [Display(Name = "Unit Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Base Unit for this Unit")]
        public BaseUnit BaseUnit { get; set; }

        [Required]
        [Display(Name = "Conversion factor to Base Unit")]
        public double ConversionToBaseFactor { get; set; } = 1;

        public List<Unit> Units;
        public List<SelectListItem> BaseUnits { get; set; } = new List<SelectListItem>();

        public IndexUnitViewModel() {}

        public IndexUnitViewModel(IEnumerable<Unit> units)
        {
            AddUnits(units);
        }

        public void AddUnits(IEnumerable<Unit> units)
        {
            Units = (List<Unit>)units;
            foreach (var baseUnit in Enum.GetValues(typeof(BaseUnit)))
            {
                int index = (int)baseUnit;
                BaseUnits.Add(new SelectListItem
                {
                    Value = index.ToString(),
                    Text = baseUnit.ToString()
                });
            }
        }
    }
}
