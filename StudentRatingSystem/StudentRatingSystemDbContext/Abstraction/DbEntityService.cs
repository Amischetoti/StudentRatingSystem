using StudentRatingSystemDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemDbContext.Abstraction
{
    /// <summary>
    /// Абстрактный базовый класс сервиса для управления сущностями базы данных типа <typeparamref name="T"/>.
    /// Реализует интерфейс <see cref="IBaseManagement{T}"/> и предоставляет базовую реализацию для удаления сущностей.
    /// </summary>
    /// <typeparam name="T">Тип сущности базы данных.</typeparam>
    public abstract class DbEntityServiceBase<T> : IBaseManagement<T>
    {
        /// <summary>
        /// Контекст базы данных для выполнения операций с сущностями.
        /// </summary>
        protected readonly AppDbContext ctx;

        /// <summary>
        /// Конструктор, инициализирующий контекст базы данных через синглтон.
        /// </summary>
        public DbEntityServiceBase()
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
        /// Виртуальный метод для удаления сущности из базы данных.
        /// Выполняет удаление и сохраняет изменения.
        /// При ошибке возвращает false и логирует сообщение с помощью <see cref="Debug.WriteLine"/>.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        /// <returns>Асинхронная задача с результатом успешности операции удаления.</returns>
        public async virtual Task<bool> Remove(T entity)
        {
            if (entity == null)
                return await Task.FromResult(false);

            try
            {
                // Удаление сущности из контекста
                ctx.Remove(entity);
                Debug.WriteLine($"{GetType().Name}: entity removed!");

                // Сохранение изменений в базе данных
                await ctx.SaveChangesAsync();
                Debug.WriteLine($"{GetType().Name}: changes saved!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name}: {ex.Message}");
                return false;
            }

            return await Task.FromResult(true);
        }
    }

}
