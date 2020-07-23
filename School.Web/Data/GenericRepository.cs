using Microsoft.EntityFrameworkCore;
using School.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data
{
    //O repositório Genérico vai receber uma classe qualquer(T) herdando os métodos do IGenericRepository e também as propriedades da IEntity
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context) // é necessário inserir o construtor para poder receber o DataContext que opssui as informações e configurações para aceder a BD
        {                                             // é através do DataContext que recebemos também a classe necessária para a operação
            _context = context;
        }


        
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>()
                .AddAsync(entity);

            await SaveAllAsync();
        }



        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }



        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>()
                 .Remove(entity);

            await SaveAllAsync();
        }



        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<T>()
                .AnyAsync(e => e.Id == id);
        }



        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking(); // AsNoTracking() serve para ler os dados apenas... não guarda-los ou editar-los (Verificar informação via Mouse Hover)
        }



        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }



        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>()
                .Update(entity);

            await SaveAllAsync();
        }
    }
}
