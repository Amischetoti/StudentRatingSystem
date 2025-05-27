using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemLib.Entities
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Firstname { get; set; } = null!;

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string Lastname { get; set; } = null!;

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string? Middlename { get; set; } = null!;

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Полное имя пользователя (Фамилия Имя Отчество).
        /// Если отчество отсутствует, возвращается только Фамилия и Имя.
        /// </summary>
        [NotMapped]
        public string Fullname
        {
            get
            {
                if (Middlename != null)
                {
                    return $"{Lastname} {Firstname} {Middlename}";
                }
                else
                {
                    return $"{Lastname} {Firstname}";
                }
            }
        }
    }
}
