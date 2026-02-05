using System.Linq.Expressions;
using GumAdministration.Model;
using GumAdministration.Repositories;

namespace GumAdministration.Services;

public class VisitService(VisitRepository visitRepository, ClientRepository clientRepository)
{
    public async Task<Visit?> Get(long id)
        => await visitRepository.Get(id);

    public async Task<Visit?> Add(long clientId, DateTime start)
    {
        Client? client = await clientRepository.Get(clientId);
        if (client is null)
            return null;

        Visit visit = new Visit() { Client = client, Start = start };
        
        return await visitRepository.Add(visit);
    }

    public async Task<bool> Delete(long id)
        =>  await visitRepository.Delete(id);

    public async Task<IEnumerable<Visit>> GetAll()
        => await visitRepository.GetAll();

    public async Task<Visit?> SelectFirst(Expression<Func<Visit, bool>> predicate)
        => await visitRepository.SelectFirst(predicate);

    public async Task<IQueryable<Visit>> GetIQueryableByExpression(Expression<Func<Visit, bool>> predicate)
        => await visitRepository.GetIQueryableByExpression(predicate);

    public async Task<bool> Update(Visit entity)
        => await visitRepository.Update(entity);
}