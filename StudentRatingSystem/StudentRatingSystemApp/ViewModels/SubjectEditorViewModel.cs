using Microsoft.IdentityModel.Tokens;
using StudentRatingSystemApp.Commands;
using StudentRatingSystemDbContext.Services;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace StudentRatingSystemApp.ViewModels
{
    /// <summary>
    /// Модель представления для редактора предмета (дисциплины).
    /// Управляет данными предмета и командами для сохранения и закрытия.
    /// </summary>
    public class SubjectEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор модели, инициализация данных и команд.
        /// </summary>
        /// <param name="user">Текущий пользователь (не используется, при необходимости).</param>
        /// <param name="subject">Объект предмета для редактирования. Может быть null для добавления нового.</param>
        /// <param name="subjectService">Сервис для операций с предметами.</param>
        /// <param name="teachers">Список преподавателей для выбора.</param>
        public SubjectEditorViewModel(User user, Subject subject, SubjectService subjectService, List<Teacher> teachers)
        {
            Subject = subject;
            _subjectService = subjectService;
            _teachers = teachers;

            /// <summary>
            /// Если предмет не равен null, заполняем поля с существующими данными.
            /// </summary>
            if (subject != null)
            {
                SubjectName = subject.SubjectName;
                SelectedTeacher = subject.Teacher;
            }

            /// <summary>
            /// Команда сохранения изменений предмета.
            /// Валидация данных и добавление или обновление записи.
            /// </summary>
            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;

                // Если предмет новый (null), добавляем.
                if (subject == null)
                {
                    _subjectService.Add(new Subject()
                    {
                        Subject_id = Guid.NewGuid(),
                        SubjectName = this.SubjectName,
                        TeacherId = Guid.NewGuid(),
                        Teacher = this.SelectedTeacher
                    });
                    MessageBox.Show("Дисциплина добавлена!");
                }
                // Иначе обновляем существующий.
                else
                {
                    _subjectService.Update(subject, new Subject()
                    {
                        Subject_id = Guid.NewGuid(),
                        SubjectName = this.SubjectName,
                        TeacherId = Guid.NewGuid(),
                        Teacher = this.SelectedTeacher
                    });
                    MessageBox.Show("Дисциплина изменена!");
                }
            });

            /// <summary>
            /// Команда закрытия окна.
            /// </summary>
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string subjectName = string.Empty;
        private Teacher selectedTeacher;
        private List<Teacher> _teachers;
        private SubjectService _subjectService;
        private Subject subject;

        /// <summary>
        /// Объект предмета для редактирования.
        /// </summary>
        public Subject Subject { get => subject; set => Set(ref subject, value, nameof(subject)); }

        /// <summary>
        /// Название дисциплины.
        /// </summary>
        public string SubjectName
        {
            get => subjectName; set
            {
                // Проверка, что поле не пустое.
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Название дисциплины' не может быть пустым.");
                    return;
                }
                Set(ref subjectName, value, nameof(subjectName));
            }
        }
        /// <summary>
        /// Выбранный преподаватель.
        /// </summary>
        public Teacher SelectedTeacher
        {
            get => selectedTeacher; set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать преподавателя.");
                    return;
                }
                Set(ref selectedTeacher, value, nameof(selectedTeacher));
            }
        }
        /// <summary>
        /// Список преподавателей для выбора.
        /// </summary>
        public List<Teacher> Teachers { get => _teachers; set => Set(ref _teachers, value, nameof(Teachers)); }

        /// <summary>
        /// Команда для сохранения изменений.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда для закрытия окна.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Проверка корректности данных перед сохранением.
        /// </summary>
        /// <returns>Истина, если все проверки прошли успешно; иначе — ложь.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(SubjectName))
            {
                MessageBox.Show("Поле 'Название дисциплины' не может быть пустым.");
                return false;
            }
            if (SelectedTeacher == null)
            {
                MessageBox.Show("Необходимо выбрать преподавателя.");
                return false;
            }
            return true;
        }
    }
}