using System.Collections.Generic;
using EProcurement.Models;
using System.Threading.Tasks;

namespace EProcurement.Services
{
    public interface IAksesGroupService
    {
        List<Master_Group> GetAll();
        Master_Group Getdata(string groupId);
        Master_Group Edit(string groupId, Master_Group model);
        Master_Group Add(Master_Group model);
        IEnumerable<Master_Menu> GetAllMenu();
        Master_Group Delete(string groupId);
    }
}