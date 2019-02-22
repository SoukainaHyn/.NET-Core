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
    public class AdresseRepository : DefaultRepository<Adresse, string>, IAdresseRepository
    {
        public AdresseRepository(ClientWSContext clientWSContext) : base(clientWSContext)
        {
           
        }
        
        public async Task<IEnumerable<Adresse>> GetAllAdressesWithClient()
        {
            return await _clientWSContext.Adresses.Include(a => a.Client).ToListAsync();
        }
       
        public  async Task<int> RemoveAdresseByClient(string id)
        {
            int res = 0;
            var adresses = _clientWSContext.Adresses.Where(x => x.ClientId == id);
            if (adresses.Count() > 0)
            {
                _clientWSContext.RemoveRange(adresses);
               res = await _clientWSContext.SaveChangesAsync();
            }
            return res  ;
        }
        

        public async Task<ICollection<Adresse>> InsertList(IEnumerable<Adresse> adresses)
        {
            ICollection<Adresse> adressesClientNull = new List<Adresse>();
            foreach (var adresse in adresses)
            {
                if (adresse.ClientId != null)
                {
                    _clientWSContext.Add(adresse);
                    await _clientWSContext.SaveChangesAsync();
                }
                else
                {

                    adressesClientNull.Add(adresse);
                }
            }
            return adressesClientNull;
        }
        
    }
}
