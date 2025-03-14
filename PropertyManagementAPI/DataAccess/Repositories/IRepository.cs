﻿// PropertyManagement.DataAccess/Repositories/IRepository.cs
namespace PropertyManagement.DataAccess.Repositories
{
    //Basic CRUD that most repos will use
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}