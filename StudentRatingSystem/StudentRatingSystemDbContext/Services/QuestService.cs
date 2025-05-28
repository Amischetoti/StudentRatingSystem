using StudentRatingSystemDbContext.Abstraction;
using StudentRatingSystemDbContext.Connections;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemDbContext.Services
{
    /// <summary>
    /// Сервис для работы с сущностью <see cref="Quest"/>,
    /// предоставляет операции получения, добавления, обновления и удаления заданий.
    /// </summary>
    public class QuestService : BaseService<Quest>
    {
        /// <summary>
        /// Получение всех заданий.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая перечисление заданий.</returns>
        public override async Task<IEnumerable<Quest?>> GetEntities()
        {
            return await Task.FromResult(ctx.Quests.ToList() as IEnumerable<Quest>);
        }

        /// <summary>
        /// Получение задания по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID задания.</param>
        /// <returns>Асинхронная задача, возвращающая задание или null, если не найдено.</returns>
        public override async Task<Quest?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Quests.SingleOrDefault(a => a.Quest_id == id));
        }

        /// <summary>
        /// Добавление нового задания.
        /// </summary>
        /// <param name="entity">Объект задания для добавления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Add(Quest entity)
        {
            if (entity == null) return (false);

            ctx.Quests.Add(entity);
            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Обновление существующего задания.
        /// </summary>
        /// <param name="entity">Объект текущего задания.</param>
        /// <param name="newEntity">Объект с новыми данными.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Update(Quest entity, Quest newEntity)
        {
            if (entity == null || newEntity == null) return (false);

            entity.TypeOfTask = newEntity.TypeOfTask;
            entity.DateOfCompletion = newEntity.DateOfCompletion;
            entity.NumberOfPoints = newEntity.NumberOfPoints;

            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Удаление задания.
        /// </summary>
        /// <param name="entity">Объект задания для удаления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Remove(Quest entity)
        {
            if (entity == null) return (false);

            ctx.Quests.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }

}
