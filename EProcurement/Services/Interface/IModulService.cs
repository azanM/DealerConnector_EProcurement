using System.Collections.Generic;
using EProcurement.Models;
using System.Data.Entity;

namespace EProcurement.Services
{
    public interface IModulService
    {
        List<Master_Menu> GetAll();
        Master_Menu Add(Master_Menu model);
        Master_Menu Getdata(string menuId);
        Master_Menu Edit(string menuId, Master_Menu model);
        Master_Menu Delete(string menuId);
    }
}