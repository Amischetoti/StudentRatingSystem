using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemDbContext.Abstraction
{
    /// <summary>
    /// Интерфейс для управления аккаунтами, предоставляющий метод получения аккаунта по логину и паролю.
    /// </summary>
    /// <typeparam name="T">Тип модели аккаунта.</typeparam>
    public interface IAccountManagement<T>
    {
        /// <summary>
        /// Получение аккаунта по логину и паролю.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Асинхронная задача, возвращающая аккаунт или null, если не найдено.</returns>
        Task<T?> GetAccount(string login, string password);
    }
}
