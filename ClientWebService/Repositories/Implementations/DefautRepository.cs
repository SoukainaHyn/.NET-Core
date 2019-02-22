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
    public class DefaultRepository<TEntity, TPrimaryKey> : IDefaultRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    {
        public readonly ClientWSContext _clientWSContext;

        public DefaultRepository(ClientWSContext clientWSContext)
        {
            _clientWSContext = clientWSContext;
        }

        public virtual async Task Close()
        {
            await _clientWSContext.SaveChangesAsync();
            _clientWSContext.Dispose();
        }

        public virtual async Task<long> Count()
        {
            return await _clientWSContext.Set<TEntity>().CountAsync();
        }

        public virtual async Task<int> Delete(TEntity entity)
        {
            return await Delete(entity.Id);
        }

        public virtual async Task<int> Delete(TPrimaryKey id)
        {
            int res = 0;
            // chercher l'entité à supprimer
            var entity = await _clientWSContext.Set<TEntity>().FirstOrDefaultAsync(b => b.Id.Equals(id));
            if (entity != null) // Si l'entité est trouvée, la supprimée
            {
                _clientWSContext.Set<TEntity>().Remove(entity);
                res = await _clientWSContext.SaveChangesAsync();
            }

            return res;
        }

        public virtual async Task<bool> Exists(TEntity entity) => await this._clientWSContext.Set<TEntity>().AnyAsync(e => e == entity || e.Id.Equals(entity.Id));

        public virtual async Task<bool> Exists(TPrimaryKey id) => await this._clientWSContext.Set<TEntity>().AnyAsync(e => e.Id.Equals(id));

        public virtual async Task<bool> ExistsByCode(string code) => await this._clientWSContext.Set<TEntity>().AnyAsync(e => e.Code.Equals(code));

        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _clientWSContext.Set<TEntity>().OrderBy(e => e.Code).ToListAsync();

        public virtual async Task<List<TEntity>> GetAllList() => await _clientWSContext.Set<TEntity>().OrderBy(e => e.Code).ToListAsync();

        public virtual IQueryable<TEntity> GetAllQueryable() => _clientWSContext.Set<TEntity>().OrderBy(e => e.Code).AsQueryable();

        public virtual async Task<TEntity> GetByCode(string code)
        {
            // renvoyer NULL si le code est vide
            if (String.IsNullOrEmpty(code) || String.IsNullOrWhiteSpace(code)) return null;

            return await _clientWSContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Code.Equals(code));
        }

        public async virtual Task<TEntity> GetByKey(TPrimaryKey id)
        {
            // renvoyer NULL si l'id est vide
            if (id == null || String.IsNullOrEmpty(id.ToString()) || String.IsNullOrWhiteSpace(id.ToString())) return null;

            return await _clientWSContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async virtual Task<TEntity> Insert(TEntity entity)
        {
            // Ajouter l'entité
            var e = await _clientWSContext.Set<TEntity>().AddAsync(entity);
            int res = await _clientWSContext.SaveChangesAsync();

            // renvoyer l'entité devise nouvellement enregistré si elle est bien enreistrée !
            if (e != null && res > 0)
            {
                return e.Entity;
            }

            return null;
        }

        public async virtual Task<TPrimaryKey> InsertAndGetId(TEntity entity)
        {
            // Ajouter l'entité, ensuite valider les changements dans le Context
            var e = await _clientWSContext.Set<TEntity>().AddAsync(entity);
            int res = await _clientWSContext.SaveChangesAsync();

            // renvoyer l'ID de l'entité devise nouvellement enregistré si elle est bien enregistrée ! Sinon renvoyer NULL !
            if (e != null && res > 0)
            {
                return e.Entity.Id;
            }

            return default(TPrimaryKey);
        }

        public virtual async Task<TEntity> InsertOrUpdate(TEntity entity)
        {
            // Verifier si l'entité porte bien Id non Vide
            if (entity.Id != null && !string.IsNullOrWhiteSpace(entity.Id.ToString()) && !string.IsNullOrEmpty(entity.Id.ToString()))
            {
                // Trouver l'entité portant l'Id de la BD afin que son etat cange en Entité gérée par EF
                var e1 = await _clientWSContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));
                if (e1 != null)
                {
                    // mettre à jour l'entité
                    var ep1 = _clientWSContext.Set<TEntity>().Update(entity);
                    var res1 = await this._clientWSContext.SaveChangesAsync();
                    // renvoyer l'ID de l'entité devise nouvellement enregistré si elle est bien enreistrée ! Sinon renvoyer NULL !
                    return ep1 != null ? ep1.Entity : null;
                }
            }

            // Effecter un Nouveau enregistrement de l'entité, ensuite valider les changements dans le Context
            var e = await _clientWSContext.Set<TEntity>().AddAsync(entity);
            int res = await _clientWSContext.SaveChangesAsync();

            // renvoyer l'entité devise nouvellement enregistré si elle est bien enregistrée !
            if (e != null && res > 0)
            {
                return e.Entity;
            }

            return null;
        }

        public virtual async Task<TPrimaryKey> InsertOrUpdateAndGetId(TEntity entity)
        {
            // Verifier si l'entité porte bien Id non Vide
            if (entity.Id != null && !string.IsNullOrWhiteSpace(entity.Id.ToString()) && !string.IsNullOrEmpty(entity.Id.ToString()))
            {
                // Trouver l'entité portant l'Id de la BD afin que son etat cange en Entité gérée par EF
                var e1 = await _clientWSContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id.Equals(entity.Id));
                if (e1 != null)
                {
                    // mettre à jour l'entité, et valider les changements
                    var ep1 = _clientWSContext.Set<TEntity>().Update(entity);
                    var res1 = await this._clientWSContext.SaveChangesAsync();

                    // renvoyer l'ID de l'entité devise nouvellement enregistré si elle est bien enreistrée ! Sinon renvoyer NULL !
                    return ep1 != null ? ep1.Entity.Id : default(TPrimaryKey);
                }
            }

            // Effecter un Nouveau enregistrement de l'entité, ensuite valider les changements dans le Context
            var e = _clientWSContext.Set<TEntity>().Add(entity);
            int res = _clientWSContext.SaveChanges();

            // renvoyer l'entité devise nouvellement enregistré si elle est bien enregistrée !
            if (e != null && res > 0)
            {
                return e.Entity.Id;
            }

            return default(TPrimaryKey);
        }

        public async virtual Task<TEntity> Update(TEntity entity)
        {
            return await this.Update(entity.Id, entity);
        }

        public async virtual Task<TEntity> Update(TPrimaryKey id, TEntity entity)
        {
            // verifier si l'entité existe
            if (await Exists(id))
            {
                // mettre à jour l'entité
                var e = _clientWSContext.Update(entity);
                await _clientWSContext.SaveChangesAsync();

                // revoyer l'entité si la mise est effectuée sinon renvoyer NULL
                return e != null ? e.Entity : null;
            }

            // si l'entité n'existe pas
            return null;
        }

        //Supprimer :
        public virtual async Task<List<int>> DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            List<int> res = new List<int>();
            int r = 0;
            IEnumerable<TEntity> entities = _clientWSContext.Set<TEntity>().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                _clientWSContext.Set<TEntity>().Remove(entity);
                res.Add(r);
                r = await _clientWSContext.SaveChangesAsync();
            }
            return res;
        }
        

    }
}
