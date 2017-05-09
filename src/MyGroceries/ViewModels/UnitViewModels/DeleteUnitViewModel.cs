using MyGroceries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGroceries.ViewModels.UnitViewModels
{
    public class DeleteUnitViewModel
    {
        public List<Unit> Units;

        public DeleteUnitViewModel() { }

        public DeleteUnitViewModel(IEnumerable<Unit> units)
        {
            Units = (List<Unit>)units;
        }
    }
}
