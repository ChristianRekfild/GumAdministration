using System.Diagnostics;
using System.Linq.Expressions;
using GumAdministration.Dto;
using GumAdministration.Extensions;
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

    public async Task<ClientDto?> Get(long id)
    {
        var client = await _clientRepository.Get(id);
        return client?.ToDto();
    }

    public async Task<ClientDto> Add(ClientDto clientDto)
    {
        var client = clientDto.ToClient();
        var addedClient = await _clientRepository.Add(client);
        
        return addedClient.ToDto();
    }

    public async Task<bool> Delete(long id)
        =>  await _clientRepository.Delete(id);

    public async Task<IEnumerable<ClientDto>> GetAll()
    {
        try
        {
            var allClients = await _clientRepository.GetAll();
            return allClients.Select(c => c.ToDto());
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }
    }

    public async Task<Client?> SelectFirst(Expression<Func<Client, bool>> predicate)
        => await _clientRepository.SelectFirst(predicate);

    public async Task<IQueryable<Client>> GetIQueryableByExpression(Expression<Func<Client, bool>> predicate)
        => await _clientRepository.GetIQueryableByExpression(predicate);

    public async Task<bool> Update(ClientDto clientDto)
    {
        var client = clientDto.ToClient();
        var successful = await _clientRepository.Update(client);
        
        return successful;
    }
}