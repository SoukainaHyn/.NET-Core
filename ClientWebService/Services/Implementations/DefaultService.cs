using ClientWebService.Data.Models;
using ClientWebService.Repositories.Interfaces;
using DinkToPdf;
using DinkToPdf.Contracts;
using PaysWebService.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientWebService.Services.Implementations
{
    public class DefaultService<TEntity, TPrimaryKey> : IDefaultService<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    {
        // Pour l'Accès aux DAO (Repository)
        protected readonly IDefaultRepository<TEntity, TPrimaryKey> defaultRepository;
        // Pour Convertir les Fichiers PDF en fichiers binaires 
        protected readonly IConverter converter;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="defaultRepository"></param>
        /// <param name="converter"></param>
        public DefaultService(IDefaultRepository<TEntity, TPrimaryKey> defaultRepository, IConverter converter)
        {
            this.defaultRepository = defaultRepository;
            this.converter = converter;
        }
        /// <summary>
        /// compter
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> Count()
        {
            return await this.defaultRepository.Count();
        }

        /// <summary>
        /// Supprimer (entity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(TEntity entity)
        {
            return await this.defaultRepository.Delete(entity) > 0 ? true : false; ;
        }

        /// <summary>
        /// Supprimer (id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(TPrimaryKey id)
        {
            return await this.defaultRepository.Delete(id) > 0 ? true : false; ;
        }

        /// <summary>
        /// exist (entity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> Exists(TEntity entity)
        {
            return await this.defaultRepository.Exists(entity);
        }

        /// <summary>
        /// exist (id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Exists(TPrimaryKey id)
        {
            return await this.defaultRepository.Exists(id);
        }

        /// <summary>
        /// exist (code)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistsByCode(string code)
        {
            return await this.defaultRepository.ExistsByCode(code);
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await this.defaultRepository.GetAll();
        }

        /// <summary>
        /// Get (Code)
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByCode(string code)
        {
            return await this.defaultRepository.GetByCode(code);
        }

        /// <summary>
        /// Get (Key)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByKey(TPrimaryKey id)
        {
            return await this.defaultRepository.GetByKey(id);
        }

        /// <summary>
        /// Ajouter (entity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            return await this.defaultRepository.Insert(entity);
        }

        /// <summary>
        /// Ajouter et get id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TPrimaryKey> InsertAndGetId(TEntity entity)
        {
            return await this.defaultRepository.InsertAndGetId(entity);
        }

        /// <summary>
        /// ajouter ou modifier
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertOrUpdate(TEntity entity)
        {
            return await this.defaultRepository.InsertOrUpdate(entity);
        }

        /// <summary>
        /// ajouter ou modifier and get id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TPrimaryKey> InsertOrUpdateAndGetId(TEntity entity)
        {
            return await this.defaultRepository.InsertOrUpdateAndGetId(entity);
        }

        /// <summary>
        /// modifier (entity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Update(TEntity entity)
        {
            return await this.defaultRepository.Update(entity);
        }

        /// <summary>
        /// update (id, entite)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> Update(TPrimaryKey id, TEntity entity)
        {
            return await this.defaultRepository.Update(id,entity);
        }


        /// <summary>
        /// Générer un Fichier PDF en binaire(byte[]) pour la liste des entités de type TEntity
        /// </summary>
        /// <returns></returns>
        private async Task<HtmlToPdfDocument> CreatePDFFromHTML()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 25, Bottom = 20 },
                DocumentTitle = "Liste des Entités"
            };

            var entities = await defaultRepository.GetAll();
            string htmlContent = "";
            if (entities.First() is Client)
            {
                htmlContent = HTMLGeneratorClient.GetHTMLString((IEnumerable<Client>)entities);
            }
            else if (entities.First() is Adresse)
            {
                htmlContent = HTMLGeneratorAdresses.GetHTMLString((IEnumerable<Adresse>)entities);
            }
            else if (entities.First() is Contact)
            {
                htmlContent = HTMLGeneratorContacts.GetHTMLString((IEnumerable<Contact>)entities);
            }
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Ressources", "style.css") },
                /*HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] sur [toPage]", Line = true },*/
                FooterSettings = { FontName = "Arial", FontSize = 8, HtmUrl = Path.Combine(Directory.GetCurrentDirectory(), "Ressources", "PiedsPages.html"),
                    Line = false, Right = "Page [page] / [toPage]\n" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return pdf;
        }


        /// <summary>
        /// Générer un Fichier PDF en binaire(byte[]) pour la liste des Devises
        /// </summary>
        /// <returns></returns>
        public virtual async Task<byte[]> GetInPDFBinaryFileAsync()
        {
            // 1. créer le pdf à partir du Html
            var pdf = await CreatePDFFromHTML();

            // 2. convertir le pdf en fichier binaire
            var file = converter.Convert(pdf);

            // renvoyer le fichier binaire
            return file;
        }

        /// <summary>
        /// Delete Generic
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<List<int>> DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.defaultRepository.DeleteWhere(predicate);
        }
    }
}
