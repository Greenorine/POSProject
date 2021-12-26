using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using POSProject.DataAccess;
using POSProject.Models;

namespace POSProject.Services;

public class ClientsService
{
    private readonly AppDbContext appDbContext;
    
    public ClientsService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    
    public async Task<List<Client>> GetAllClientsAsync()
    {
        return await appDbContext.Clients.AsNoTracking().ToListAsync();
    }
    
    public async Task<bool> InsertClientAsync(Client client)
    {
        await appDbContext.Clients.AddAsync(client);
        await appDbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Client> GetClientAsync(int id)
    {
        var client = await appDbContext.Clients.FirstOrDefaultAsync(c => c.Id.Equals(id));
        return client;
    }
    
    public async Task<bool> UpdateClientAsync(Client client)
    {
        var find = await appDbContext.Clients.FirstAsync(c => c.Id == client.Id);
        appDbContext.Entry(find).CurrentValues.SetValues(client);
        await appDbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteClientAsync(Client client)
    {
        appDbContext.Remove(client);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}