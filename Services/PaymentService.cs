using System.Linq.Expressions;
using GumAdministration.Model;
using GumAdministration.Repositories;

namespace GumAdministration.Services;

public class PaymentService
{
    private readonly PaymentRepository _paymentRepository;
    
    public PaymentService(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    
    // !ВНИМАНИЕ!
    // Ничего не реализовано, кроме Add
    // !ВНИМАНИЕ! //
    
    public async Task<Payment?> Get(long id)
        => await _paymentRepository.Get(id);

    public async Task<Payment> Add(Payment entity)
        => await _paymentRepository.Add(entity);

    public async Task<bool> Delete(long id)
        =>  await _paymentRepository.Delete(id);

    public async Task<IEnumerable<Payment>> GetAll()
        => await _paymentRepository.GetAll();

    public async Task<Payment?> SelectFirst(Expression<Func<Payment, bool>> predicate)
        => await _paymentRepository.SelectFirst(predicate);

    public async Task<IQueryable<Payment>> GetIQueryableByExpression(Expression<Func<Payment, bool>> predicate)
        => await _paymentRepository.GetIQueryableByExpression(predicate);

    public async Task<bool> Update(Payment entity)
        => await _paymentRepository.Update(entity);
}