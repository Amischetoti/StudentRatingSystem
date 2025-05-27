using StudentRatingSystemDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemDbContext.Abstraction
{
    /// <summary>
    /// Абстрактный базовый сервис для операций с сущностями типа <typeparamref name="T"/>.
    /// Реализует интерфейс <see cref="IService{T}"/> и предоставляет общие методы для работы с базой данных.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает сервис.</typeparam>
    public abstract class BaseService<T> : IService<T>
    {
        /// <summary>
        /// Контекст базы данных для доступа к данным.
        /// </summary>
        protected readonly AppDbContext ctx;

        /// <summary>
        /// Конструктор, инициализирующий контекст базы данных через синглтон.
        /// </summary>
        public BaseService()
        {
            ctx = DbContextSingleton.Instance.DbContext;
        }

        /// <summary>
        /// Абстрактный метод получения всех сущностей типа <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Асинхронная задача с перечислением сущностей или null-значениями.</returns>
        public abstract Task<IEnumerable<T?>> GetEntities();

        /// <summary>
        /// Абстрактный метод получения одной сущности по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Асинхронная задача с найденной сущностью или null.</returns>
        public abstract Task<T?> GetEntity(Guid id);

        /// <summary>
        /// Абстрактный метод добавления новой сущности.
        /// </summary>
        /// <param name="entity">Сущность для добавления.</param>
        /// <returns>Асинхронная задача, возвращающая результат операции добавления.</returns>
        public abstract Task<bool> Add(T entity);

        /// <summary>
        /// Абстрактный метод обновления существующей сущности.
        /// </summary>
        /// <param name="entity">Существующая сущность для обновления.</param>
        /// <param name="newEntity">Новая сущность с обновленными данными.</param>
        /// <returns>Асинхронная задача, возвращающая результат операции обновления.</returns>
        public abstract Task<bool> Update(T entity, T newEntity);

        /// <summary>
        /// Абстрактный метод удаления сущности.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        /// <returns>Асинхронная задача, возвращающая результат операции удаления.</returns>
        public abstract Task<bool> Remove(T entity);
    }
}
