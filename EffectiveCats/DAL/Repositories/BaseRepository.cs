using DAL.Interfaces;
using DAL.Interfaces.Finders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class BaseRepository<T, K> : IRepository<T, K>
        where T : class, IId<K>
    {
        private readonly MainContext _dbContext;

        protected DbSet<T> DbContext => _dbContext.Set<T>();

        public BaseRepository(MainContext context)
        {
            _dbContext = context;
        }

        public T Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        /// Читает сущность по long Id из БД и корректирует все свойства или заданные списком properties значениями в сущности T entity.
        /// Ограничения - навигационные свойства не корректируются.
        /// Только те заданные свойства корректируются, которые отличаются по значению с указанными в entity.
        /// </summary>
        /// <param name="entity"> Сущность, в которой находятся свойства для их корректировки </param>
        /// <param name="id"> Id сущности, которую нужно скорректировать </param>
        /// <param name="properties"> Необязательный список свойств, которые подлежат корректировке </param>
        public async Task<T> EditByIdAsync(T entity, K id, List<string> properties = null)
        {
            var entityBd = await _dbContext.Set<T>().FirstOrDefaultAsync(x=> x.Id.Equals(id));
            if (entityBd == null)
                throw new KeyNotFoundException($"NotFound {typeof(T).Name} Id={id}");

            var entityType = typeof(T);
            if (properties != null)
                foreach (var property in properties)
                {
                    var propertyInfo = entityType.GetProperty(property);
                    var value = propertyInfo?.GetValue(entity, null);
                    var valueBd = propertyInfo?.GetValue(entityBd, null);
                    if ((value != null && !value.Equals(valueBd)) || (valueBd != null && !valueBd.Equals(value)))
                    {
                        propertyInfo.SetValue(entityBd, value, null);
                        _dbContext.Entry(entityBd).Property(property).IsModified = true;
                    }
                }
            else
            {
                var propertyInfos = entityType
                    .GetProperties()
                    .Where(p => !p.PropertyType.IsAbstract && p.CustomAttributes.Count(a => a.AttributeType.Name == "NotMappedAttribute") == 0); // Без Collection<T> и навигационных свойств и без [NotMapped]
                
                foreach (var propertyInfo in propertyInfos)
                {
                    var value = propertyInfo.GetValue(entity, null);
                    var valueBd = propertyInfo.GetValue(entityBd, null);
                    var type = propertyInfo.PropertyType;

                    if (value != null && propertyInfo.PropertyType.IsArray && ((Array)value).Length > 0)
                    {
                        propertyInfo.SetValue(entityBd, value, null);
                        _dbContext.Entry(entityBd).Property(propertyInfo.Name).IsModified = true;

                        continue;
                    }

                    if (value != null && (!value.Equals(valueBd) || !valueBd.Equals(value)))
                    {
                        propertyInfo.SetValue(entityBd, value, null);
                        _dbContext.Entry(entityBd).Property(propertyInfo.Name).IsModified = true;
                    }
                }
            }
            return entityBd;
        }

        public async Task<K> DeleteByIdAsync(K id)
        {
            var entityBd = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (entityBd == null)
                throw new KeyNotFoundException($"Not found {typeof(T).Name} Id={id}");
            return Delete(entityBd);
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public K Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return entity.Id;
        }
    }
}
