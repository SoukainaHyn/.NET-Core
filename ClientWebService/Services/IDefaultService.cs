using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientWebService.Services
{
    public interface IDefaultService<TEntity, TPrimaryKey> where TEntity : class
    {
        /// <summary>
        /// Compter le Nombre des entités
        /// </summary>
        /// <returns></returns>
        Task<long> Count();

        /// <summary>
        /// Supprimer une entité
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Delete(TEntity entity);

        /// <summary>
        /// Supprimer une entité TEntity par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(TPrimaryKey id);

        /// <summary>
        /// Verifier si une entité TEntity existe en précisant l'entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(TEntity entity);

        /// <summary>
        /// Verifier si une entité TEntity existe en précisant son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(TPrimaryKey id);

        /// <summary>
        /// Verifier si une entité TEntity existe en précisant son Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ExistsByCode(string code);

        /// <summary>
        /// Retourner la liste des TEntity ou Format Set<TEntity>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Retrouver une TEntity par son Code (si elle existe !)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> GetByCode(string code);

        /// <summary>
        /// Retrouver une TEntity par son Id (si elle existe !)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> GetByKey(TPrimaryKey id);

        /// <summary>
        /// Insérer une TEntity si elle existe, ensuite retourner l'ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Insérer une TEntity si elle existe, ensuite retourner l'ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TPrimaryKey> InsertAndGetId(TEntity entity);

        /// <summary>
        /// Insérer ou Mettre à jour une TEntity si elle existe
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertOrUpdate(TEntity entity);

        /// <summary>
        /// Insérer ou Mettre à jour une TEntity si elle existe, ensuite retourner l'ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TPrimaryKey> InsertOrUpdateAndGetId(TEntity entity);

        /// <summary>
        /// Mettre à jour une entité TEntity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Mettre à jour une entité TEntity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Update(TPrimaryKey id, TEntity entity);

        /// <summary>
        /// Générer un Fichier PDF en binaire(byte[]) pour la liste des entités de type TEntity
        /// </summary>
        /// <returns></returns>
        Task<byte[]> GetInPDFBinaryFileAsync();

        /// <summary>
        /// Delete Generic
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<int>> DeleteWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
