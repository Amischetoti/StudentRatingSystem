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
    /// Сервис для работы с сущностью <see cref="Teacher"/>,
    /// реализующий операции получения, добавления, обновления и удаления учителей.
    /// </summary>
    public class TeacherService : BaseService<Teacher>
    {
        /// <summary>
        /// Получение всех учителей.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая перечисление учителей.</returns>
        public override async Task<IEnumerable<Teacher?>> GetEntities()
        {
            return await Task.FromResult(ctx.Teachers.ToList() as IEnumerable<Teacher>);
        }

        /// <summary>
        /// Получение учителя по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID учителя.</param>
        /// <returns>Асинхронная задача, возвращающая учителя или null, если не найдено.</returns>
        public override async Task<Teacher?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Teachers.SingleOrDefault(a => a.Teacher_id == id));
        }

        /// <summary>
        /// Добавление нового учителя.
        /// </summary>
        /// <param name="entity">Объект учителя для добавления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Add(Teacher entity)
        {
            if (entity == null) return (false);

            ctx.Teachers.Add(entity);
            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Обновление информации об учителе.
        /// </summary>
        /// <param name="entity">Объект текущего учителя.</param>
        /// <param name="newEntity">Объект с новыми данными.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Update(Teacher entity, Teacher newEntity)
        {
            if (entity == null || newEntity == null) return (false);

            entity.FIO = newEntity.FIO;
            entity.Contact = newEntity.Contact;

            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Удаление учителя.
        /// </summary>
        /// <param name="entity">Объект учителя для удаления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Remove(Teacher entity)
        {
            if (entity == null) return (false);

            ctx.Teachers.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
