
using System.Collections.Generic;

namespace EProcurement.Models.ViewModel
{
    public class AksesGroupViewModel
    {
        public Master_Group AksesGroup { get; set; }
        public IEnumerable<Master_Menu> Menu { get; set; }
    }
}