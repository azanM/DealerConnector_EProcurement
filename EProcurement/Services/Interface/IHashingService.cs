using System.Collections.Generic;
using EProcurement.Models;
using EProcurement.Models.ViewModel;

namespace EProcurement.Services.Interface
{
    interface IHashingService
    {
        string CreatePasswordHash(string password);
    }
}
