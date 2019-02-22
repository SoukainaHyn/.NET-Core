using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Repositories.Interfaces
{
   public interface IContactRepository : IDefaultRepository<Contact, string>
    {

        /// <summary>
        /// GetAllContactsWithClient
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contact>> GetAllContactsWithClient();
        
        /// <summary>
        /// RemoveContactByClient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> RemoveContactByClient(string id);
        
        /// <summary>
        /// ajouter list contacts
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        Task<ICollection<Contact>> InsertList(IEnumerable<Contact> contacts);
    }
}

