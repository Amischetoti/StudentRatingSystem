using Microsoft.EntityFrameworkCore;
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
    /// Класс сервиса для управления сущностью <see cref="Grade"/>,
    /// реализующий операции получения, добавления, обновления и удаления оценок.
    /// </summary>
    public class GradeService : BaseService<Grade>
    {
        /// <summary>
        /// Получение всех оценок.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая перечисление оценок.</returns>
        public override async Task<IEnumerable<Grade?>> GetEntities()
        {
            return await Task.FromResult(ctx.Grades.ToList() as IEnumerable<Grade>);
        }

        /// <summary>
        /// Получение оценки по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID оценки.</param>
        /// <returns>Асинхронная задача, возвращающая оценку или null, если не найдена.</returns>
        public override async Task<Grade?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Grades.SingleOrDefault(a => a.Grade_id == id));
        }

        /// <summary>
        /// Добавление новой оценки.
        /// </summary>
        /// <param name="entity">Объект оценки для добавления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Add(Grade entity)
        {
            if (entity == null) return false;

            ctx.Grades.Add(entity);
            var success = await SaveChangesAsync();

            if (success)
            {
                // Обновляем позиции оценок по заданию
                await UpdateGradesPositions(entity.QuestId);
            }

            return success;
        }

        /// <summary>
        /// Обновление существующей оценки.
        /// </summary>
        /// <param name="entity">Объект текущей оценки.</param>
        /// <param name="newEntity">Объект с новыми данными.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Update(Grade entity, Grade newEntity)
        {
            if (entity == null || newEntity == null) return false;

            entity.ReceivedPoint = newEntity.ReceivedPoint;
            entity.ExtraPoint = newEntity.ExtraPoint;
            entity.DateOfAssessment = newEntity.DateOfAssessment;
            entity.Quest = newEntity.Quest;

            var success = await SaveChangesAsync();

            if (success)
            {
                // Обновляем позиции для этого задания
                await UpdateGradesPositions(entity.QuestId);
            }

            return success;
        }

        /// <summary>
        /// Удаление оценки.
        /// </summary>
        /// <param name="entity">Объект оценки для удаления.</param>
        /// <returns>Асинхронная задача, возвращающая успех операции.</returns>
        public override async Task<bool> Remove(Grade entity)
        {
            if (entity == null) return false;

            ctx.Grades.Remove(entity);
            var success = await SaveChangesAsync();

            if (success)
            {
                // Обновляем позиции для этого задания
                await UpdateGradesPositions(entity.QuestId);
            }

            return success;
        }

        /// <summary>
        /// Внутренний метод для сохранения изменений в базе данных.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая успех сохранения.</returns>
        private async Task<bool> SaveChangesAsync()
        {
            try
            {
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Обновление позиций оценок для указанного задания.
        /// </summary>
        /// <param name="questId">GUID задания.</param>
        /// <returns>Задача, завершающаяся после обновления.</returns>
        public async Task UpdateGradesPositions(Guid questId)
        {
            var grades = ctx.Grades
        .Where(g => g.QuestId == questId)
        .ToList();

            var sortedGrades = grades
                .OrderByDescending(g => g.FinalScore)
                .ToList();

            
            int position = 1;
            foreach (var grade in sortedGrades)
            {
                grade.Position = position++;
            }
            ctx.SaveChangesAsync();
        }
        /// <summary>
        /// Обновление позиций и получение актуальных оценок для задания.
        /// </summary>
        /// <param name="questId">GUID задания.</param>
        /// <returns>Перечисление оценок для задания, отсортированное по полученным баллам.</returns>
        public async Task<IEnumerable<Grade>> UpdateAndGetGrades(Guid questId)
        {
            await UpdateGradesPositions(questId);

            return ctx.Grades
                .Where(g => g.QuestId == questId)
                .OrderByDescending(g => g.ReceivedPoint + g.ExtraPoint)
                .ToList();

        }
    }
}
