using System.Collections.Generic;
using EProcurement.Models;
using System.Data.Entity;

namespace EProcurement.Services
{
    public interface IMaterialService
    {
        List<MSMATERIAL> GetAll();
        MSMATERIAL Getdata(string materialNumber);
        MSMATERIAL Edit(string materialNumber, MSMATERIAL model);
        MSMATERIAL Add(MSMATERIAL model);
    }
}