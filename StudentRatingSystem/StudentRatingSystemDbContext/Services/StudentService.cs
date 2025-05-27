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
    /// Сервис для работы с сущностью <see cref="Student"/>,
    /// реализующий операции получения, добавления, обновления и удаления студентов.
    /// </summary>
    public class StudentService : BaseService<Student>
    {
        /// <summary>
        /// Получение всех студентов.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая перечисление студентов.</returns>
        public override async Task<IEnumerable<Student?>> GetEntities()
        {
            return await Task.FromResult(ctx.Students.ToList() as IEnumerable<Student>);
        }

        /// <summary>
        /// Получение студента по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID студента.</param>
        /// <returns>Асинхронная задача, возвращающая студента или null, если не найдено.</returns>
        public override async Task<Student?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Students.SingleOrDefault(a => a.Student_id == id));
        }

        /// <summary>
        /// Добавление нового студента.
        /// </summary>
        /// <param name="entity">Объект студента для добавления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Add(Student entity)
        {
            if (entity == null) return (false);

            ctx.Students.Add(entity);
            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Обновление информации о студенте.
        /// </summary>
        /// <param name="entity">Объект текущего студента.</param>
        /// <param name="newEntity">Объект с новыми данными.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Update(Student entity, Student newEntity)
        {
            if (entity == null || newEntity == null) return (false);

            entity.FIO = newEntity.FIO;
            entity.Group = newEntity.Group;
            entity.Contact = newEntity.Contact;
            entity.Subject = newEntity.Subject;

            await ctx.SaveChangesAsync();
            return (true);
        }

        /// <summary>
        /// Удаление студента.
        /// </summary>
        /// <param name="entity">Объект студента для удаления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Remove(Student entity)
        {
            if (entity == null) return (false);

            ctx.Students.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
