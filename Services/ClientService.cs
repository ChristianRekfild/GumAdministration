using System.Linq.Expressions;
using GumAdministration.Model;
using GumAdministration.Repositories;

namespace GumAdministration.Services;

public class ClientService
{
    private readonly ClientRepository _clientRepository;
    
    public ClientService(ClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    
    public async Task<Client?> Get(Guid id)
        => await _clientRepository.Get(id);

    public async Task<Client> Add(Client entity)
        => await _clientRepository.Add(entity);

    public async Task<bool> Delete(Guid id)
        =>  await _clientRepository.Delete(id);

    public async Task<IEnumerable<Client>> GetAll()
        => await _clientRepository.GetAll();

    public async Task<Client?> SelectFirst(Expression<Func<Client, bool>> predicate)
        => await _clientRepository.SelectFirst(predicate);

    public async Task<IQueryable<Client>> GetIQueryableByExpression(Expression<Func<Client, bool>> predicate)
        => await _clientRepository.GetIQueryableByExpression(predicate);

    public async Task<bool> Update(Client entity)
        => await _clientRepository.Update(entity);
}