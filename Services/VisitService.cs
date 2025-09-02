using System.Linq.Expressions;
using GumAdministration.Model;
using GumAdministration.Repositories;

namespace GumAdministration.Services;

public class VisitService
{
    private readonly VisitRepository _visitRepository;
    
    public VisitService(VisitRepository visitRepository)
    {
        _visitRepository = visitRepository;
    }
    
    // !ВНИМАНИЕ!
    // Ничего не реализовано, кроме Add
    // !ВНИМАНИЕ! //
    
    public async Task<Visit?> Get(Guid id)
        => await _visitRepository.Get(id);

    public async Task<Visit> Add(Visit entity)
        => await _visitRepository.Add(entity);

    public async Task<bool> Delete(Guid id)
        =>  await _visitRepository.Delete(id);

    public async Task<IEnumerable<Visit>> GetAll()
        => await _visitRepository.GetAll();

    public async Task<Visit?> SelectFirst(Expression<Func<Visit, bool>> predicate)
        => await _visitRepository.SelectFirst(predicate);

    public async Task<IQueryable<Visit>> GetIQueryableByExpression(Expression<Func<Visit, bool>> predicate)
        => await _visitRepository.GetIQueryableByExpression(predicate);

    public async Task<bool> Update(Visit entity)
        => await _visitRepository.Update(entity);
}