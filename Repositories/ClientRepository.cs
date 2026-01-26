using System.Linq.Expressions;
using GumAdministration.Model;
using GumAdministration.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GumAdministration.Repositories;

public sealed class ClientRepository : IGenericRepository<Client>
{
    private readonly Context _context;
    private readonly DbSet<Client> _clients;
    
    public ClientRepository(Context context)
    {
        _context = context;
        _clients = context.Set<Client>();
    }
    
    public async Task<Client?> Get(long id)
        => await _clients.FindAsync(id);

    public async Task<Client> Add(Client entity)
    {
        var addedClient = _clients.Add(entity);
        await _context.SaveChangesAsync();
        
        return addedClient.Entity;
    }

    public async Task<bool> Delete(long id)
    {
        var client = await _clients.FindAsync(id);
        if (client is null) return false;
        
        _clients.Remove(client);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Client>> GetAll()
        => await _context.Clients.ToListAsync();

    public async Task<Client?> SelectFirst(Expression<Func<Client, bool>> predicate)
    {
        return await _context.Clients.FirstOrDefaultAsync(predicate);
    }

    public async Task<IQueryable<Client>> GetIQueryableByExpression(Expression<Func<Client, bool>> predicate)
    {
        return _clients.Where(predicate);
    }

    public async Task<bool> Update(Client entity)
    {
        var existClient = _clients.Find(entity.Id);
        if (existClient is null) return false;
        
        existClient.BirthDate = entity.BirthDate;
        existClient.FirstName = entity.FirstName;
        existClient.LastName = entity.LastName;
        existClient.Patronymic = entity.Patronymic;
        existClient.IsPersonalTraining =  entity.IsPersonalTraining;
        existClient.PhoneNumber = entity.PhoneNumber;
        existClient.PaymentRequired = entity.PaymentRequired;
        
        int updatedEntitiesCount = await _context.SaveChangesAsync();
        return updatedEntitiesCount > 0;
    }

    [Obsolete(error:true, message:"Для данного класса нет реализации, так как он вполне самостоятельный. Не юзать!")]
    public async Task<Client> GetWithInclude(Guid id)
    {
        throw new NotImplementedException();
    }
}