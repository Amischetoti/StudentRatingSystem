using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemLib.Entities
{
    /// <summary>
    /// Задание для студента.
    /// </summary>
    public class Quest
    {
        /// <summary>
        /// Уникальный идентификатор задания.
        /// </summary>
        [Key]
        public Guid Quest_id { get; set; }

        /// <summary>
        /// Тип задания.
        /// </summary>
        public string? TypeOfTask { get; set; }

        /// <summary>
        /// Дата выполнения задания.
        /// </summary>
        public string? DateOfCompletion { get; set; }

        /// <summary>
        /// Количество баллов, максимально возможных за задание.
        /// </summary>
        public string? NumberOfPoints { get; set; }


        /// <summary>
        /// Возвращает строковое представление задания.
        /// </summary>
        /// <returns>Строка с типом задания и количеством баллов.</returns>
        public override string ToString()
        {
            return $"{TypeOfTask}, {NumberOfPoints}";
        }
    }
}
