using System;
using System.Collections.Generic;

namespace EProcurement.Models.ViewModel
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel()
        {
            this.ChildMenuItems = new List<MenuItemViewModel>();
        }

        public string MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuItemPath { get; set; }
        public string ParentItemId { get; set; }
        public string ParentIconClass { get; set; }
        public string ChildIconClass { get; set; }
        public virtual ICollection<MenuItemViewModel> ChildMenuItems { get; set; }

    }
}