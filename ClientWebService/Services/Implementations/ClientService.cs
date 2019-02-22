using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ClientWebService.Data.Models;
using ClientWebService.Repositories.Interfaces;
using ClientWebService.Services.Interfaces;
using DinkToPdf.Contracts;

namespace ClientWebService.Services.Implementations
{
    public class ClientService : DefaultService<Client,string>, IClientService
    {
        private readonly IClientRepository clientRepository;
        public ClientService(
            IClientRepository clientRepository, 
            IConverter converter):base(clientRepository, converter)
        {
            this.clientRepository = clientRepository;
        }

        /// <summary>
        /// GetAllClientWithAdresseContact
        /// </summary>
        /// <returns></returns>
        public override Task<IEnumerable<Client>> GetAll()
        {
            return this.clientRepository.GetAllClientWithAdresseContact();
        }

        public Task InsertList(IEnumerable<Client> entity)
        {
            return this.clientRepository.InsertList(entity);
        }
    }
}
