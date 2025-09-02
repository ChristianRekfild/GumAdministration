using System.Linq.Expressions;
using GumAdministration.Model;
using GumAdministration.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GumAdministration.Repositories;

public class VisitRepository : IGenericRepository<Visit>
{
    private readonly Context _context;
    private readonly DbSet<Visit> _visits;
    
    public VisitRepository(Context context)
    {
        _context = context;
        _visits = context.Set<Visit>();
    }
    
    public async Task<Visit?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Visit> Add(Visit visit)
    {
        var newPayment = _visits.Add(visit);
        await _context.SaveChangesAsync();
        
        return newPayment.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Visit>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Save()
    {
        throw new NotImplementedException();
    }

    public async Task<Visit?> SelectFirst(Expression<Func<Visit, bool>> predicate)
    {
        return await _context.Visits.FirstOrDefaultAsync(predicate);
    }

    public async Task<IQueryable<Visit>> GetIQueryableByExpression(Expression<Func<Visit, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(Visit entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Visit> GetWithInclude(Guid id)
    {
        throw new NotImplementedException();
    }
}