using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data
{
    
    //O repositório genérico serve para fazer o CRUD de qualquer objecto
    public interface IGenericRepository<T> where T : class //Como pode se utilizar qualquer objecto nesta interface, ao ser chamada basta injectar a entidade pretendida.
    {

        IQueryable<T> GetAll(); //Por se tratar de uma interface genérica, usa-se o nome GetAll por não sabermos qual classe estamos a receber, logo o nome tb deve ser genérico

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistsAsync(int id);

    }
}
