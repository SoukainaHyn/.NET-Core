using ClientWebService.Data;
using ClientWebService.Data.Models;
using ClientWebService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientWebService.Repositories.Implementations
{
    public class ClientRepository: DefaultRepository<Client, string>, IClientRepository
    {
        public ClientRepository(ClientWSContext clientWSContext) : base(clientWSContext)
        {
        }

        public  async Task<IEnumerable<Client>> GetAllClientWithAdresseContact()
        {
            return await _clientWSContext.Clients.Include(a => a.Adresses).Include(c => c.Contact).ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            return await _clientWSContext.Clients.ToListAsync();
        }

        
        public async Task InsertList(IEnumerable<Client> clients)
        {
            _clientWSContext.Clients.AddRange(clients);
            await _clientWSContext.SaveChangesAsync();
        }
    }
}
