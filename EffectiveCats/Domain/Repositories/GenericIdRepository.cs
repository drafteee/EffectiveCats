using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public class GenericIdRepository<T> : IDisposable where T : class, IId
    {
        private readonly DbContext _dbContext;

        protected DbContext DbContext => _dbContext;

        public GenericIdRepository(DbContext context)
        {
            _dbContext = context;
        }

        public async Task<T> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public async Task<T> FindByFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public IQueryable<T> FindAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<T> GetByIdNoTrackingAsync(long id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> EditAndSaveAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
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
        public async Task<T> EditByIdAsync(T entity, long id, List<Expression<Func<T, object>>> properties = null)
        {
            var entityBd = await GetByIdAsync(id);
            if (entityBd == null)
                throw new KeyNotFoundException($"NotFound {typeof(T).Name}  Id={id}");

            var entityType = typeof(T);
            if (properties != null)
                foreach (var property in properties)
                {
                    var propertyName = (property.Body as MemberExpression ?? (MemberExpression)((UnaryExpression)property.Body).Operand).Member.Name;
                    var propertyInfo = entityType.GetProperty(propertyName);
                    var value = propertyInfo?.GetValue(entity, null);
                    var valueBd = propertyInfo?.GetValue(entityBd, null);
                    if ((value != null && !value.Equals(valueBd)) || (valueBd != null && !valueBd.Equals(value)))
                    {
                        propertyInfo.SetValue(entityBd, value, null);
                    }
                }
            else
            {
                var declaredProperties = ObjectContext.GetObjectType(entityType).GetProperties();
                var propertyInfos =
                    entityType.GetProperties()
                        .Where(
                            p =>
                                !p.PropertyType.IsAbstract && declaredProperties.Contains(p) &&
                                p.CustomAttributes.Count(a => a.AttributeType.Name == "NotMappedAttribute") == 0); // Без Collection<T> и навигационных свойств и без [NotMapped]
                foreach (var propertyInfo in propertyInfos)
                {
                    var value = propertyInfo.GetValue(entity, null);
                    var valueBd = propertyInfo.GetValue(entityBd, null);
                    var type = propertyInfo.PropertyType;

                    if (value != null && propertyInfo.PropertyType.IsArray && ((Array)value).Length > 0)
                    {
                        propertyInfo.SetValue(entityBd, value, null);
                        continue;
                    }

                    if (value != null && (!value.Equals(valueBd) || !valueBd.Equals(value)))
                    {
                        propertyInfo.SetValue(entityBd, value, null);
                    }
                }
            }
            return entityBd;
        }

        public virtual async Task<long> DeleteByIdAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Not found {typeof(T).Name} Id={id}");
            return await DeleteAndSaveAsync(entity);
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Добавляет к контексту и сохраняет в БД
        /// </summary>
        /// <param name="entity"> Cущность </param>
        public virtual async Task<bool> AddAndSaveAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public async Task<long> DeleteAndSaveAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return entity.Id;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}