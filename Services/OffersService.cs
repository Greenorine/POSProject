using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POSProject.DataAccess;
using POSProject.Models;

namespace POSProject.Services;

public class OffersService
{
    private readonly AppDbContext appDbContext;
    public OffersService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    
    public async Task<List<Offer>> GetAllOffersAsync()
    {
        return await appDbContext.Offers.AsNoTracking().ToListAsync();
    }
    
    public async Task<bool> InsertOfferAsync(Offer offer)
    {
        await appDbContext.Offers.AddAsync(offer);
        await appDbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<Offer> GetOfferAsync(int id)
    {
        var offer = await appDbContext.Offers.FirstOrDefaultAsync(c => c.Id.Equals(id));
        return offer;
    }
    
    public async Task<bool> UpdateOfferAsync(Offer offer)
    {
        var find = await appDbContext.Offers.FirstAsync(c => c.Id == offer.Id);
        appDbContext.Entry(find).CurrentValues.SetValues(offer);
        await appDbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteOfferAsync(Offer offer)
    {
        appDbContext.Remove(offer);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}