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
    /// Сервис для работы с сущностью <see cref="Subject"/>,
    /// реализующий операции получения, добавления, обновления и удаления предметов.
    /// </summary>
    public class SubjectService : BaseService<Subject>
    {
        /// <summary>
        /// Получение всех предметов.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая перечисление предметов.</returns>
        public override async Task<IEnumerable<Subject?>> GetEntities()
        {
            return await Task.FromResult(ctx.Subjects.ToList() as IEnumerable<Subject>);
        }

        /// <summary>
        /// Получение предмета по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID предмета.</param>
        /// <returns>Асинхронная задача, возвращающая предмет или null, если не найдено.</returns>
        public override async Task<Subject?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Subjects.SingleOrDefault(a => a.Subject_id == id));
        }

        /// <summary>
        /// Добавление нового предмета.
        /// </summary>
        /// <param name="entity">Объект предмета для добавления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Add(Subject entity)
        {
            if (entity == null) return (false);

            ctx.Subjects.Add(entity);
            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Обновление информации о предмете.
        /// </summary>
        /// <param name="entity">Объект текущего предмета.</param>
        /// <param name="newEntity">Объект с новыми данными.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Update(Subject entity, Subject newEntity)
        {
            if (entity == null || newEntity == null) return (false);

            entity.SubjectName = newEntity.SubjectName;
            entity.Teacher = newEntity.Teacher;

            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Удаление предмета.
        /// </summary>
        /// <param name="entity">Объект предмета для удаления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Remove(Subject entity)
        {
            if (entity == null) return (false);

            ctx.Subjects.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
