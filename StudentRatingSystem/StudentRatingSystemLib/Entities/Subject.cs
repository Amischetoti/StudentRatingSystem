using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemLib.Entities
{
    /// <summary>
    /// Предмет, который изучает студент.
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Уникальный идентификатор предмета.
        /// </summary>
        [Key]
        public Guid Subject_id { get; set; }

        /// <summary>
        /// Название предмета.
        /// </summary>
        public string? SubjectName { get; set; }

        /// <summary>
        /// Идентификатор учителя (внешний ключ).
        /// </summary>
        public Guid TeacherId { get; set; }

        /// <summary>
        /// Навигационное свойство к учителю.
        /// </summary>
        public Teacher? Teacher { get; set; }

        /// <summary>
        /// Возвращает строковое представление предмета.
        /// </summary>
        /// <returns>Название предмета.</returns>
        public override string ToString()
        {
            return $"{SubjectName}";
        }
    }
}
