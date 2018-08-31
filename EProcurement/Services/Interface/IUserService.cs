using System.Collections.Generic;
using EProcurement.Models;
using EProcurement.Models.ViewModel;

namespace EProcurement.Services
{
    public interface IUserService
    {
        Master_User Login(string username, string password);
        List<Master_User> GetAll();
        Master_User Getdata(string userId);
        DashboardViewModel GetDashboard();
        Master_User Edit(string userId, Master_User model);
        Master_User ChangedPassword(Master_User model, string NewPassword);
        Master_User Add(Master_User model);
        MenuViewModel GetMenu();
        Master_User Reset(string UserID, Master_User model);
        Log_Error AddLogError(Log_Error model);
        Log_Login AddLogLogin(Log_Login model);
    }
}