using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using POSProject.Backend.Models;

namespace POSProject.Backend.Services.Interfaces;

public interface IDataService<T>
{
    public Task<List<T>> GetAllAsync();

    public Task<bool> InsertAsync(T value);

    public Task<T> GetByIdAsync(Guid id);

    public Task<List<T>> GetByQueryAsync(Expression<Func<T, bool>> predicate);
    public Task<T> GetFirstOrDefaultByQueryAsync(Expression<Func<T, bool>> predicate);

    public Task<bool> UpdateAsync(T client);

    public Task<bool> DeleteAsync(Guid id);
}