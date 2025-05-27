using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentRatingSystemLib.Entities;

namespace StudentRatingSystemDbContext.Connections
{
    /// <summary>
    /// Абстрактный класс, представляющий контекст приложения для работы с базами данных.
    /// Наследуется от DbContext (Entity Framework Core).
    /// </summary>
    public abstract class AppDbContext : DbContext
    {
        /// <summary>
        /// Коллекция объектов оценок.
        /// </summary>
        public DbSet<Grade> Grades { get; set; }

        /// <summary>
        /// Коллекция объектов заданий (Quest).
        /// </summary>
        public DbSet<Quest> Quests { get; set; }

        /// <summary>
        /// Коллекция объектов студентов.
        /// </summary>
        public DbSet<Student> Students { get; set; }

        /// <summary>
        /// Коллекция предметов.
        /// </summary>
        public DbSet<Subject> Subjects { get; set; }

        /// <summary>
        /// Коллекция объектов учителей.
        /// </summary>
        public DbSet<Teacher> Teachers { get; set; }

        /// <summary>
        /// Коллекция объектов пользователей.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Переопределенный метод сохранения изменений в базе данных.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, содержащая количество сохраненных записей.</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
