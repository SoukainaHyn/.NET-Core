using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientWebService.Repositories.Interfaces
{
    public interface IClientRepository : IDefaultRepository<Client, string>
    {
        /// <summary>
        /// GetAllClientWithAdresseContact
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Client>> GetAllClientWithAdresseContact();

        /// <summary>
        /// InsertList client
        /// </summary>
        /// <param name = "clients" ></ param >
        /// < returns ></ returns >
        Task InsertList(IEnumerable<Client> clients);

        
    }
}
