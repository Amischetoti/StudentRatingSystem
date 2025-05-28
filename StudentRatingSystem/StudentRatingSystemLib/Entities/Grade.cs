using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRatingSystemLib.Entities
{
    /// <summary>
    /// Оценка за выполнение задания.
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// Уникальный идентификатор оценки.
        /// </summary>
        [Key]
        public Guid Grade_id { get; set; }

        /// <summary>
        /// Полученное количество баллов.
        /// </summary>
        public decimal ReceivedPoint { get; set; }

        /// <summary>
        /// Дополнительные баллы.
        /// </summary>
        public decimal ExtraPoint { get; set; }

        /// <summary>
        /// Дата оценки (в строковом формате).
        /// </summary>
        public string? DateOfAssessment { get; set; }

        /// <summary>
        /// Идентификатор задания (внешний ключ).
        /// </summary>
        public Guid QuestId { get; set; }

        /// <summary>
        /// Навигационное свойство к заданию.
        /// </summary>
        public Quest? Quest { get; set; }

        /// <summary>
        /// Идентификатор студента (внешний ключ).
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// Навигационное свойство к студенту.
        /// </summary>
        public Student? Student { get; set; }

        /// <summary>
        /// Позиция (порядковый номер).
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Возвращает строковое представление оценки.
        /// </summary>
        /// <returns>Строка с информацией о баллах.</returns>
        public override string ToString()
        {
            return $"{ReceivedPoint}, {ExtraPoint} - {Quest}. - {Student}";
        }

        /// <summary>
        /// Итоговый балл (сумма полученных и дополнительных баллов).
        /// </summary>
        [NotMapped]
        public decimal FinalScore => ReceivedPoint + ExtraPoint;
    }
}
