using System.Linq.Expressions;
using GumAdministration.Model;
using Microsoft.EntityFrameworkCore;

namespace GumAdministration.Repositories.Base;

public class GenericRepository<T> : IGenericRepository<T> where T:class 
{
    protected readonly Context _context;

    /// <summary>
    /// Generic'овый репозиторий, чтобы не писать лишний раз методы получения, добавления и бла бла бла
    /// </summary>
    /// <param name="context">Контекст данных</param>
    public GenericRepository(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение сущности с БД (асинхронное)
    /// </summary>
    /// <param name="id">Guid Id сущности</param>
    /// <returns></returns>
    public virtual async Task<T?> Get(long id)
        => await _context.FindAsync<T>(id);

    /// <summary>
    /// Добавление сущности в БД (асинхронное)
    /// </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <returns></returns>
    public virtual async Task<T> Add(T entity)
    {
        var objToReturn = await _context.AddAsync<T>(entity);
        await _context.SaveChangesAsync();

        return objToReturn.Entity;
    }

    /// <summary>
    /// Удаление сущности из БД (асинхронное)
    /// </summary>
    /// <param name="id">Сущность для удаления</param>
    /// <returns>true, если успешно. Иначе - false</returns>
    public async Task<bool> Delete(long id)
    {
        var entity = await _context.FindAsync<T>(id);
        if (entity is null) return false;
        
        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<T>> GetAll()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<bool> Save()
    {
        if (await _context.SaveChangesAsync(true) > 0)
            return true;

        return false;
    }

    public async Task<T?> SelectFirst(Expression<Func<T, bool>> predicate)
    {
        T? first = await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();

        return first;
    }

    /// <summary>
    /// Получаем коллекцию по условию. Заработает - я офигею! (Да, Кирилл, как с колонками!)
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<IQueryable<T>> GetIQueryableByExpression(Expression<Func<T, bool>> predicate)
    {
        IQueryable<T> result = _context.Set<T>().Where(predicate).AsQueryable();

        return result;
    }


    // ------ ! НЕ РЕАЛИЗОВАНО В ДАННОМ КЛАССЕ ! ------ //

    // ----------- Реализуем в наследниках ----------- //
    
    public virtual Task<bool> Update(T entity)
        => throw new NotImplementedException();
    
    /// <summary>
    /// ! Метод не реализован, так как нужно понимать - что подтягивать !
    /// По идее - это получение объекта со вложенными зависимостями
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual Task<T> GetWithInclude(Guid id)
        => throw new NotImplementedException();
    
}