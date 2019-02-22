using ClientWebService.Data.Models;
using ClientWebService.Repositories.Implementations;
using ClientWebService.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientWebService.Services.Interfaces
{
    public interface IClientService : IDefaultService<Client, string>
    {
        /// <summary>
        /// InsertList Of Clients
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertList(IEnumerable<Client> entity);

    }
}
