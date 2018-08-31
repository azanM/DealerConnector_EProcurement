using System;
using System.Collections.Generic;

namespace EProcurement.Models.ViewModel
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            Items = new List<MenuItemViewModel>();
        }

        public List<MenuItemViewModel> Items;
    }
}