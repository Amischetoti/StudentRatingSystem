using StudentRatingSystemApp.Commands;
using StudentRatingSystemDbContext.Services;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentRatingSystemApp.ViewModels
{
    /// <summary>
    /// Модель представления для редактора преподавателя.
    /// Управляет данными преподавателя и командами для сохранения и закрытия.
    /// </summary>
    public class TeacherEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор модели, инициализация данных и команд.
        /// </summary>
        /// <param name="user">Текущий пользователь (при необходимости).</param>
        /// <param name="teacher">Объект преподавателя для редактирования, может быть null для добавления нового.</param>
        /// <param name="teacherService">Сервис для операций с преподавателями.</param>
        public TeacherEditorViewModel(User user, Teacher teacher, TeacherService teacherService)
        {
            Teacher = teacher;
            _teacherService = teacherService;

            /// <summary>
            /// Если преподаватель передан - инициализируем поля соответствующими значениями.
            /// </summary>
            if (teacher != null)
            {
                FIO = teacher.FIO;
                Contact = teacher.Contact;
            }

            /// <summary>
            /// Команда сохранения изменений преподавателя.
            /// Проверяет валидность данных, затем добавляет нового или обновляет существующего преподавателя.
            /// </summary>
            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;

                // Добавление нового преподавателя, если не задан исходный объект.
                if (teacher == null)
                {
                    _teacherService.Add(new Teacher() { Teacher_id = Guid.NewGuid(), FIO = this.FIO, Contact = this.Contact });
                    MessageBox.Show("Преподаватель добавлен!");
                }
                // Обновление существующего преподавателя.
                else
                {
                    _teacherService.Update(teacher, new Teacher() { Teacher_id = Guid.NewGuid(), FIO = this.FIO, Contact = this.Contact });
                    MessageBox.Show("Преподаватель изменён!");
                }
            });

            /// <summary>
            /// Команда для закрытия окна редактора.
            /// </summary>
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string fio = string.Empty;
        private string contact = string.Empty;
        private TeacherService _teacherService;
        private Teacher teacher;

        /// <summary>
        /// Редактируемый преподаватель.
        /// </summary>
        public Teacher Teacher { get => teacher; set => Set(ref teacher, value, nameof(teacher)); }

        /// <summary>
        /// ФИО преподавателя.
        /// </summary>
        public string FIO
        {
            get => fio; set
            {
                // Проверка, что поле не пустое.
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                    return;
                }
                Set(ref fio, value, nameof(FIO));
            }
        }

        /// <summary>
        /// Контактные данные преподавателя.
        /// </summary>
        public string Contact { get => contact; set => Set(ref contact, value, nameof(Contact)); }

        /// <summary>
        /// Команда сохранения преподавателя.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда закрытия окна.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Проверяет корректность введённых данных.
        /// </summary>
        /// <returns>True, если данные валидны; иначе false.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(FIO))
            {
                MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                return false;
            }
            return true;
        }

    }
}
