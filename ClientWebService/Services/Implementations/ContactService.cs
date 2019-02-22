using ClientWebService.Data.Models;
using ClientWebService.Services.Interfaces;
using ClientWebService.Repositories.Interfaces;
using DinkToPdf.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientWebService.Services.Implementations
{
    public class ContactService : DefaultService<Contact, string>, IContactService
    {
        private readonly IContactRepository contactRepository;
        public ContactService(
            IContactRepository contactRepository,
            IConverter converter) : base(contactRepository, converter)
        {
            this.contactRepository = contactRepository;
        }

        /// <summary>
        /// GetAllContactsWithClient
        /// </summary>
        /// <returns></returns>
        public override Task<IEnumerable<Contact>> GetAll()
        {
            return this.contactRepository.GetAllContactsWithClient();
        }
        
        /// <summary>
        /// RemoveContactByClient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> RemoveContactByClient(string id)
        {
            return this.contactRepository.RemoveContactByClient(id);
        }

        /// <summary>
        /// ajouter list contacts
        /// </summary>
        /// <param name="contacts"></param>
        /// <returns></returns>
        public Task InsertList(IEnumerable<Contact> contacts)
        {
            return this.contactRepository.InsertList(contacts);
        }
    }
}
