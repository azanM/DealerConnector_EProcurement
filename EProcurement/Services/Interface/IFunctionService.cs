using System.Collections.Generic;
using EProcurement.Models;

namespace EProcurement.Services
{
    interface IFunctionService
    {
        List<Master_Menu> GetAll();
        Master_Menu Getdata(string menuId);
        Master_Menu Edit(string menuId, Master_Menu model);
        Master_Menu Add(Master_Menu model);
        Master_Menu Delete(string menuId);
        List<Master_Menu> GetModul();
    }
}
