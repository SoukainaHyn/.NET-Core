using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Services.Interfaces
{
   public interface IAdresseService : IDefaultService<Adresse, string>
    {
        Task InsertList(IEnumerable<Adresse> entity);
        Task<int> RemoveAdresseByClient(string id);
    }
}
