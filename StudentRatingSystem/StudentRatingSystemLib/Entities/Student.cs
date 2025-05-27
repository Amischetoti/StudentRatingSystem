using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemLib.Entities
{
    /// <summary>
    /// Студент, выполняющий задания.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Уникальный идентификатор студента.
        /// </summary>
        [Key]
        public Guid Student_id { get; set; }

        /// <summary>
        /// ФИО студента.
        /// </summary>
        public string? FIO { get; set; }

        /// <summary>
        /// Группа студента.
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// Контактные данные студента.
        /// </summary>
        public string? Contact { get; set; }

        /// <summary>
        /// Идентификатор предмета (внешний ключ).
        /// </summary>
        public Guid SubjectId { get; set; }

        /// <summary>
        /// Навигационное свойство к предмету.
        /// </summary>
        public Subject? Subject { get; set; }

        /// <summary>
        /// Возвращает строковое представление студента.
        /// </summary>
        /// <returns>ФИО студента.</returns>
        public override string ToString()
        {
            return $"{FIO}";
        }
    }
}
