using Microsoft.EntityFrameworkCore;
using StudentRatingSystemDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemDbContext.Connections
{
    /// <summary>
    /// Синглтон для управления единственным экземпляром <see cref="SQLServerDbContext"/>.
    /// Обеспечивает глобальную точку доступа к контексту базы данных.
    /// </summary>
    public sealed class DbContextSingleton
    {
        /// <summary>
        /// Единственный экземпляр синглтона.
        /// </summary>
        private static DbContextSingleton instance = null;

        /// <summary>
        /// Контекст базы данных SQL Server.
        /// </summary>
        public SQLServerDbContext DbContext { get; private set; }

        /// <summary>
        /// Приватный конструктор, инициализирующий контекст базы данных.
        /// Выводит сообщение в отладчик о создании экземпляра.
        /// </summary>
        private DbContextSingleton()
        {
            DbContext = new SQLServerDbContext();
            Debug.WriteLine($"{this.GetType().Name} was created!");
        }

        /// <summary>
        /// Получает единственный экземпляр синглтона.
        /// Если экземпляр еще не создан, создаёт новый.
        /// </summary>
        public static DbContextSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbContextSingleton();
                }
                return instance;
            }
        }
    }

}
