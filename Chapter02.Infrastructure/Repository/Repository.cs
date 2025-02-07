using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Chapter02.Core.Interfaces;
using Chapter02.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter02.Core.Entities;
using Org.BouncyCastle.Asn1;

namespace Chapter02.Infrastructure.Repository
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(AppDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }
        public async Task Delete(object id)
        {
            TEntity? entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete != null)
            {
                await Delete(entityToDelete);
            }
        }

        public async Task Delete(TEntity entityToDelete)
        {
            await Task.Run(
                () =>
                {
                    if (_context.Entry(entityToDelete).State == EntityState.Detached)
                    {
                        _dbSet.Attach(entityToDelete);
                    }
                    _dbSet.Remove(entityToDelete);
                });
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByID(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_dbSet, specification);
        }

        public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task Insert(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCount()
           => await _dbSet.CountAsync();
        
        public async Task Update(TEntity ententityToUpdate)
        {
            if (ententityToUpdate is Book)
            {
                await DeleteOldBookReferences((ententityToUpdate as Book)!);
            }

             await Task.Run(() =>
             {
                 _dbSet.Attach(ententityToUpdate);
                 _context.Entry(ententityToUpdate).State = EntityState.Modified;
             });
        }
        private async Task DeleteOldBookReferences(Book book)
        {
            var authors = await _context.BookAuthor.Where(b => b.BookId == book.Id).ToListAsync();
            if (authors.Count != 0)
            {
                foreach (BookAuthor item in authors)
                {
                    _context.BookAuthor.Remove(item);
                }
            }
            var categories = await _context.BookCategory.Where(b => b.BookId == book.Id).ToListAsync();
            if (categories.Count != 0)
            {
                foreach (var item in categories)
                {
                    _context.BookCategory.Remove(item);
                }
            }
            
        }
    }
}
