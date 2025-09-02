using System.Linq.Expressions;
using GumAdministration.Model;
using GumAdministration.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GumAdministration.Repositories;

public class PaymentRepository : IGenericRepository<Payment>
{
    private readonly Context _context;
    private readonly DbSet<Payment> _payments;
    
    public PaymentRepository(Context context)
    {
        _context = context;
        _payments = context.Set<Payment>();
    }
    
    public async Task<Payment?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Payment> Add(Payment payment)
    {
        var newPayment = _payments.Add(payment);
        await _context.SaveChangesAsync();
        
        return newPayment.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Payment>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Save()
    {
        throw new NotImplementedException();
    }

    public async Task<Payment?> SelectFirst(Expression<Func<Payment, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<IQueryable<Payment>> GetIQueryableByExpression(Expression<Func<Payment, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(Payment entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Payment> GetWithInclude(Guid id)
    {
        throw new NotImplementedException();
    }
}