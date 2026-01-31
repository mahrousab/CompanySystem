using CompanySystem.Application.Interfaces;
using CompanySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CompanySystem.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly CompanySystemDbContext _company;
        protected RepositoryBase(CompanySystemDbContext company)
        {
            _company = company;
        }
        public IEnumerable<T> GetAll()
        {
            return _company.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _company.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            return _company.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _company.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _company.Set<T>();
            if (includes != null)
            {
                foreach (string incluse in includes)
                {
                    query = query.Include(incluse);
                }
            }
            return query.SingleOrDefault(criteria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _company.Set<T>();
            if (includes != null)
            {
                foreach (string incluse in includes)
                {
                    query = query.Include(incluse);
                }
            }
            return await query.SingleOrDefaultAsync(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _company.Set<T>();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.Where(criteria).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return _company.Set<T>().Where(criteria).Skip(skip)
                .Take(take)
                .ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _company.Set<T>().Where(criteria);
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            if (orderBy != null)
            {
                query = ((!(orderByDirection == "ASC")) ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy));
            }
            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _company.Set<T>();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await _company.Set<T>().Where(criteria).Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _company.Set<T>().Where(criteria);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (orderBy != null)
            {
                query = ((!(orderByDirection == "ASC")) ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy));
            }
            return await query.ToListAsync();
        }

        public T Add(T entity)
        {
            _company.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _company.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _company.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _company.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _company.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _company.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _company.Set<T>().AttachRange(entities);
        }

        public int Count()
        {
            return _company.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _company.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _company.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _company.Set<T>().CountAsync(criteria);
        }

        public void Update(T entity)
       => _company.Update(entity);
        public void Create(T entity)
       => _company.Set<T>().Add(entity);

        public void Delete(T entity)
        => _company.Set<T>().Remove(entity);
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    =>
!trackChanges ?
_company.Set<T>()
.Where(expression)
.AsNoTracking() :
_company.Set<T>()
.Where(expression);

        public IQueryable<T> FindAll(bool trackChanges)
    => !trackChanges ?
_company.Set<T>()
.AsNoTracking() :
_company.Set<T>();
    }
}
