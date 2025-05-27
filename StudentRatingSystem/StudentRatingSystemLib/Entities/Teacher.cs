using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemLib.Entities
{
    /// <summary>
    /// Учитель, ведущий предмет.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Уникальный идентификатор учителя.
        /// </summary>
        [Key]
        public Guid Teacher_id { get; set; }

        /// <summary>
        /// ФИО учителя.
        /// </summary>
        public string? FIO { get; set; }

        /// <summary>
        /// Контактные данные учителя.
        /// </summary>
        public string? Contact { get; set; }

        /// <summary>
        /// Возвращает строковое представление учителя.
        /// </summary>
        /// <returns>ФИО учителя.</returns>
        public override string ToString()
        {
            return $"{FIO}";
        }
    }
}
