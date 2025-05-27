using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSupplyIStudentRatingSystemDbContextnventoryManagementDBContext.Abstraction
{
    /// <summary>
    /// Интерфейс для управления статусами поставки.
    /// </summary>
    /// <typeparam name="T">Тип управляемого объекта поставки.</typeparam>
    public interface ISupplyManagment<T>
    {
        /// <summary>
        /// Установка статуса "Ожидает".
        /// </summary>
        /// <param name="entity">Объект поставки.</param>
        /// <returns>Асинхронная задача с результатом операции.</returns>
        Task<bool> SetStatusWaiting(T entity);

        /// <summary>
        /// Установка статуса "В процессе".
        /// </summary>
        /// <param name="entity">Объект поставки.</param>
        /// <returns>Асинхронная задача с результатом операции.</returns>
        Task<bool> SetStatusInProgress(T entity);

        /// <summary>
        /// Установка статуса "Завершена".
        /// </summary>
        /// <param name="entity">Объект поставки.</param>
        /// <returns>Асинхронная задача с результатом операции.</returns>
        Task<bool> SetStatusCompleted(T entity);

        /// <summary>
        /// Установка статуса "Отменена".
        /// </summary>
        /// <param name="entity">Объект поставки.</param>
        /// <returns>Асинхронная задача с результатом операции.</returns>
        Task<bool> SetStatusCanceled(T entity);
    }
}
