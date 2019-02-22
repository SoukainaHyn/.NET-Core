using ClientWebService.Data;
using ClientWebService.Data.Models;
using ClientWebService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Repositories.Implementations
{
    public class ContactRepository : DefaultRepository<Contact, string>, IContactRepository
    {
        public ContactRepository(ClientWSContext clientWSContext) : base(clientWSContext)
    {

    }
        public async Task<IEnumerable<Contact>> GetAllContactsWithClient()
        {
            return await _clientWSContext.Contacts.Include(a => a.Client).ToListAsync();
        }
        
        public async Task<int> RemoveContactByClient(string id)
        {
            int res = 0;
            var contact = _clientWSContext.Contacts.FirstOrDefault(x => x.ClientId == id);
            if (contact != null)
            {

                _clientWSContext.Contacts.Remove(contact);
                res =  await _clientWSContext.SaveChangesAsync();
            }
            return res;
        }


        public async Task<ICollection<Contact>> InsertList(IEnumerable<Contact> contacts)
        {
            ICollection<Contact> contactClientNull = new List<Contact>();
            foreach (var contact in contacts)
            {
                if (contact.ClientId != null)
                {
                    _clientWSContext.Add(contact);
                    await _clientWSContext.SaveChangesAsync();
                }
                else
                {

                    contactClientNull.Add(contact);
                }
            }
            return contactClientNull;
        }
    }
}
