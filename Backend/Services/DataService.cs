using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POSProject.Backend.DataAccess;
using POSProject.Backend.Models;
using POSProject.Backend.Services.Interfaces;

namespace POSProject.Backend.Services;

public class DataService<T> : IDataService<T> where T : BaseAudit
{
    private readonly AppDbContext appDbContext;

    public DataService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await appDbContext.Set<T>().AsNoTracking().Where(x => x.IsActive).ToListAsync();
    }

    public async Task<bool> InsertAsync(T value)
    {
        await appDbContext.AddAsync(value);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<T> GetByIdAsync(Guid id) => await appDbContext.FindAsync<T>(id);

    public async Task<List<T>> GetByQueryAsync(Expression<Func<T, bool>> predicate) =>
        await appDbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();

    public async Task<T> GetFirstOrDefaultByQueryAsync(Expression<Func<T, bool>> predicate) =>
        (await GetByQueryAsync(predicate)).FirstOrDefault();

    public async Task<bool> UpdateAsync(T value)
    {
        var find = await appDbContext.FindAsync<T>(value.Id);
        if (find == null) return false;
        appDbContext.Entry(find).CurrentValues.SetValues(value);
        await appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var find = appDbContext.Find<T>(id);
        if (find == null) return false;
        find.IsActive = false;
        await appDbContext.SaveChangesAsync();
        return true;
    }
}