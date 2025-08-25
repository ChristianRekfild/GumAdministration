using System.Linq.Expressions;

namespace GumAdministration.Repositories.Base;

public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Получение сущности с БД (асинхронное)
    /// </summary>
    /// <param name="id">Guid Id сущности</param>
    /// <returns></returns>
    public Task<T?> Get(Guid id);

    /// <summary>
    /// Добавление сущности в БД (асинхронное)
    /// </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <returns></returns>
    public Task<T> Add(T entity);

    /// <summary>
    /// Удаление сущности из БД (асинхронное)
    /// </summary>
    /// <param name="id">Сущность для удаления</param>
    /// <returns>true, если успешно. Иначе - false</returns>
    public Task<bool> Delete(Guid id);

    /// <summary>
    /// Получить все сущности из базы данных. Внимание - они будут получены с модификатором AsNoTracking!
    /// Проще говоря - их изменения не будут отслеживаться!
    /// </summary>
    /// <returns>IEnumerable<T> выбраного типа сущностей</returns>
    Task<IEnumerable<T>> GetAll();

    /// <summary>
    /// Сохранить изменения в сущности в БД
    /// </summary>
    /// <returns>true, если успешно. Иначе - false</returns>
    Task<bool> Save();
    
    /// <summary>
    /// Получить первую сущность, которая подходит по условиям предиката
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<T?> SelectFirst(Expression<Func<T, bool>> predicate);

    Task<IQueryable<T>> GetIQueryableByExpression(Expression<Func<T, bool>> predicate);


    // ------ ! НЕ РЕАЛИЗОВАНО В ДАННОМ КЛАССЕ ! ------ //

    // ----------- Реализуем в наследниках ----------- //

    Task<bool> Update(T entity);

    Task<T> GetWithInclude(Guid id);
}