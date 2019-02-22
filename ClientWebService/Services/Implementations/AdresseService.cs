using ClientWebService.Data.Models;
using ClientWebService.Repositories.Interfaces;
using ClientWebService.Services.Interfaces;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebService.Services.Implementations
{
    public class AdresseService : DefaultService<Adresse, string>,IAdresseService
    {
        private readonly IAdresseRepository adresseRepository;
        public AdresseService(
            IAdresseRepository adresseRepository,
            IConverter converter) : base(adresseRepository, converter)
        {
            this.adresseRepository = adresseRepository;
        }

        /// <summary>
        /// GetAllAdressesWithClient
        /// </summary>
        /// <returns></returns>
        public override Task<IEnumerable<Adresse>> GetAll()
        {
            return this.adresseRepository.GetAllAdressesWithClient();
        }

        /// <summary>
        /// RemoveAdresseByClient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> RemoveAdresseByClient(string id)
        {
            return this.adresseRepository.Delete(id) ;
        }

        /// <summary>
        /// inserer list adresses
        /// </summary>
        /// <param name="adresses"></param>
        /// <returns></returns>
        public Task InsertList(IEnumerable<Adresse> entity)
        {
            return this.adresseRepository.InsertList(entity);
        }
    }
}
