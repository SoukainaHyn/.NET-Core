using ClientWebService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Services.Interfaces
{
    public interface IContactService : IDefaultService<Contact, string>
    {
        Task InsertList(IEnumerable<Contact> contacts);
        Task<int> RemoveContactByClient(string id);
    }
}
