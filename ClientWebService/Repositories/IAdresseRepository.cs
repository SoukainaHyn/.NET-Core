using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Repositories.Interfaces
{
    public interface IAdresseRepository : IDefaultRepository<Adresse, string>
    {

        /// <summary>
        /// GetAllAdressesWithClientAsync
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Adresse>> GetAllAdressesWithClient();

        /// <summary>
        /// RemoveAdresseByClient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> RemoveAdresseByClient(string id);


        /// <summary>
        /// inserer list adresses
        /// </summary>
        /// <param name="adresses"></param>
        /// <returns></returns>
        Task<ICollection<Adresse>> InsertList(IEnumerable<Adresse> adresses);
    }
}
