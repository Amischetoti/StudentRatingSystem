using StudentRatingSystemDbContext.Abstraction;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemDbContext.Services
{
    /// <summary>
    /// Сервис для управления пользователями, реализующий операции добавления, получения и обновления пользователей.
    /// </summary>
    public class UserService : DbEntityServiceBase<User>, IAccountManagement<User>
    {
        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <param name="entity">Объект пользователя, которого необходимо добавить.</param>
        /// <returns>Асинхронная задача, возвращающая true при успешном добавлении, иначе false.</returns>
        public override async Task<bool> Add(User entity)
        {
            // Проверка null
            if (entity == null) return await Task.FromResult(false);

            // Проверка на пустой Guid
            if (entity.Id == Guid.Empty) return await Task.FromResult(false);

            // проверка ввода ФИО
            if (string.IsNullOrEmpty(entity.Firstname)) return await Task.FromResult(false);
            if (string.IsNullOrEmpty(entity.Lastname)) return await Task.FromResult(false);

            // проверка логина и пароля
            if (string.IsNullOrEmpty(entity.Login)) return await Task.FromResult(false);
            if (string.IsNullOrEmpty(entity.Password)) return await Task.FromResult(false);

            // добавление в бд
            try
            {
                await ctx.AddAsync(entity);
                Debug.WriteLine($"{GetType().Name}: пользователь добавлен!");
                ctx.SaveChanges();
                Debug.WriteLine($"{GetType().Name}: пользователь сохранён!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name}: {ex.Message}");
                return await Task.FromResult(false);
            }

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns>Асинхронная задача, возвращающая коллекцию пользователей.</returns>
        public override async Task<IEnumerable<User?>> GetEntities() => await Task.FromResult(ctx.Users.ToList() as IEnumerable<User>);

        /// <summary>
        /// Получение пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Асинхронная задача, возвращающая пользователя или null, если не найдено.</returns>
        public async Task<User?> GetAccount(string login, string password) => await Task.FromResult(ctx.Users.SingleOrDefault(c => c.Login == login && c.Password == password));

        /// <summary>
        /// Получение пользователя по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID пользователя.</param>
        /// <returns>Асинхронная задача, возвращающая пользователя или null при отсутствии.</returns>
        public override async Task<User?> GetEntity(Guid id) => await Task.FromResult(ctx.Users.Single(c => c.Id == id));

        /// <summary>
        /// Обновление данных пользователя.
        /// </summary>
        /// <param name="entity">Объект пользователя для обновления.</param>
        /// <param name="newEntity">Объект с новыми данными.</param>
        /// <returns>Асинхронная задача, возвращающая true при успешном обновлении, иначе false.</returns>
        public override async Task<bool> Update(User entity, User newEntity)
        {
            if (entity == null) return await Task.FromResult(false);
            if (newEntity == null) return await Task.FromResult(false);
            // проверка новых значений
            // Проверка на пустой Guid
            if (newEntity.Id == Guid.Empty) return await Task.FromResult(false);

            // проверка на ввод ФИО
            if (string.IsNullOrEmpty(newEntity.Firstname)) return await Task.FromResult(false);
            if (string.IsNullOrEmpty(newEntity.Lastname)) return await Task.FromResult(false);


            // проверка логина и пароля
            if (string.IsNullOrEmpty(newEntity.Login)) return await Task.FromResult(false);
            if (string.IsNullOrEmpty(newEntity.Password)) return await Task.FromResult(false);


            // обновление данных
            try
            {
                // Изменение данных
                entity.Firstname = newEntity.Firstname;
                entity.Lastname = newEntity.Lastname;
                entity.Middlename = newEntity.Middlename;
                entity.Login = newEntity.Login;
                entity.Password = newEntity.Password;

                // Сохранение в бд
                ctx.Update(entity);
                Debug.WriteLine($"{GetType().Name}: пользователь обновлен!");
                ctx.SaveChanges();
                Debug.WriteLine($"{GetType().Name}: изменения сохранены!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name}: {ex.Message}");
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }
    }
}
