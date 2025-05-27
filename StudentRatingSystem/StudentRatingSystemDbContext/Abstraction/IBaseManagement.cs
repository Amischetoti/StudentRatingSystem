using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemDbContext.Abstraction
{
    /// <summary>
    /// Интерфейс базового управления сущностями, реализующий основные операции CRUD.
    /// </summary>
    /// <typeparam name="T">Тип управляемой сущности.</typeparam>
    public interface IBaseManagement<T>
    {
        /// <summary>
        /// Получение всех сущностей.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая перечисление сущностей или null.</returns>
        Task<IEnumerable<T?>> GetEntities();

        /// <summary>
        /// Получение сущности по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID идентификатор.</param>
        /// <returns>Асинхронная задача, возвращающая сущность или null.</returns>
        Task<T?> GetEntity(Guid id);

        /// <summary>
        /// Добавление новой сущности.
        /// </summary>
        /// <param name="entity">Сущность для добавления.</param>
        /// <returns>Асинхронная задача, возвращающая результат операции.</returns>
        Task<bool> Add(T entity);

        /// <summary>
        /// Обновление существующей сущности.
        /// </summary>
        /// <param name="entity">Исходная сущность.</param>
        /// <param name="newEntity">Обновленная сущность.</param>
        /// <returns>Асинхронная задача, возвращающая результат операции.</returns>
        Task<bool> Update(T entity, T newEntity);

        /// <summary>
        /// Удаление сущности.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        /// <returns>Асинхронная задача, возвращающая результат операции.</returns>
        Task<bool> Remove(T entity);
    }
}
