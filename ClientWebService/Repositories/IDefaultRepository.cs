using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClientWebService.Repositories.Interfaces
{
    public interface IDefaultRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        /// <summary>
        /// Enregistrer tous les changements, ensuite Fermer le Context de la gestion des Données
        /// </summary>
        Task Close();

        /// <summary>
        /// Compter et Retourner le Nombre d'entités de type TEntity
        /// </summary>
        /// <returns></returns>
        Task<long> Count();

        /// <summary>
        /// Supprimer une entité
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Delete(TEntity entity);

        /// <summary>
        /// Supprimer une entité par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> Delete(TPrimaryKey id);

        /// <summary>
        /// Verifier si une entité existe en précisant l'entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(TEntity entity);

        /// <summary>
        /// Verifier si une entité Devise existe en précisant son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(TPrimaryKey id);

        /// <summary>
        /// Verifier si une entité Devise existe en précisant son Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ExistsByCode(string code);

        /// <summary>
        /// Retourner la liste des Entités TEntity ou Format Set<TEntity>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Retourner la liste des entités TEntity ou Format List<TEntity>
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllList();

        /// <summary>
        /// Retourner la liste des enités TEntity ou Format IQueryable<TEntity>
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAllQueryable();

        /// <summary>
        /// Retrouver une entité TEntity par son Code (si elle existe !)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> GetByCode(string code);

        /// <summary>
        /// Retrouver une entité TEntity par son Id (si elle existe !)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> GetByKey(TPrimaryKey id);

        /// <summary>
        /// Insérer une entité de type TEntity, ensuite retourner l'ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Insert(TEntity entity);

        /// <summary>
        /// Insérer une entité de type TEntity, ensuite retourner l'ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TPrimaryKey> InsertAndGetId(TEntity entity);

        /// <summary>
        /// Insérer ou Mettre à jour une entité de type TEntity si elle existe
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertOrUpdate(TEntity entity);

        /// <summary>
        /// Insérer ou Mettre à jour une entité TEntity si elle existe, ensuite retourner l'ID
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
        /// Mettre à jour une entité Devise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Update(TPrimaryKey id, TEntity entity);

        /// <summary>
        /// Delete Generic
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<int>> DeleteWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
