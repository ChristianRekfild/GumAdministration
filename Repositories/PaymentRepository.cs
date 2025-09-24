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
        => await _payments.FindAsync(id);

    public async Task<Payment> Add(Payment payment)
    {
        var newPayment = _payments.Add(payment);
        await _context.SaveChangesAsync();
        
        return newPayment.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var payment = await this.Get(id);
        if (payment is null) return false;
        
        _payments.Remove(payment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Payment>> GetAll()
        => await _context.Payments.ToListAsync();

    public async Task<Payment?> SelectFirst(Expression<Func<Payment, bool>> predicate)
    {
        return await _context.Payments.FirstOrDefaultAsync(predicate);
    }

    public async Task<IQueryable<Payment>> GetIQueryableByExpression(Expression<Func<Payment, bool>> predicate)
        => _payments.Where(predicate);

    public async Task<bool> Update(Payment entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Payment> GetWithInclude(Guid id)
    {
        throw new NotImplementedException();
    }
}