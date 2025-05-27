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
    /// Модель представления для редактора студента.
    /// Управляет данными студента и командами для сохранения и закрытия.
    /// </summary>
    public class StudentEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор модели, инициализация данных и команд.
        /// </summary>
        /// <param name="user">Текущий пользователь (используется, при необходимости).</param>
        /// <param name="student">Объект студента для редактирования. Может быть null для добавления нового.</param>
        /// <param name="studentService">Сервис для операций с студентами.</param>
        /// <param name="subjects">Список дисциплин для выбора.</param>
        public StudentEditorViewModel(User user, Student student, StudentService studentService, List<Subject> subjects)
        {
            Student = student;
            _studentService = studentService;
            _subjects = subjects;

            /// <summary>
            /// Если студент не равен null, заполняем поля с существующими данными.
            /// </summary>
            if (student != null)
            {
                FIO = student.FIO;
                Group = student.Group;
                Contact = student.Contact;
                SelectedSubject = student.Subject;
            }

            /// <summary>
            /// Команда сохранения изменений студента.
            /// Валидация данных и добавление или обновление студента в базе.
            /// </summary>
            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;

                // Если студент новый (null), добавляем.
                if (student == null)
                {
                    _studentService.Add(new Student() { Student_id = Guid.NewGuid(), FIO = this.FIO, Group = this.Group, Contact = this.Contact, SubjectId = Guid.NewGuid(), Subject = this.SelectedSubject });
                    MessageBox.Show("Студент добавлен!");
                }
                // Иначе обновляем существующего.
                else
                {
                    _studentService.Update(student, new Student() { Student_id = Guid.NewGuid(), FIO = this.FIO, Group = this.Group, Contact = this.Contact, SubjectId = Guid.NewGuid(), Subject = this.SelectedSubject });
                    MessageBox.Show("Студент изменён!");
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

        private string fio = string.Empty;
        private string group = string.Empty;
        private string contact = string.Empty;
        private Subject selectedSubject;
        private List<Subject> _subjects;
        private StudentService _studentService;
        private Student student;

        /// <summary>
        /// Объект студента для редактирования.
        /// </summary>
        public Student Student { get => student; set => Set(ref student, value, nameof(student)); }

        /// <summary>
        /// Поле "ФИО" студента.
        /// </summary>
        public string FIO
        {
            get => fio; set
            {
                // Проверка на пустое значение.
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                    return;
                }
                Set(ref fio, value, nameof(FIO));
            }
        }

        /// <summary>
        /// Поле "Группа".
        /// </summary>
        public string Group
        {
            get => group; set
            {
                // Проверка на пустое значение.
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Группа' не может быть пустым.");
                    return;
                }
                Set(ref group, value, nameof(Group));
            }
        }

        /// <summary>
        /// Контактные данные студента.
        /// </summary>
        public string Contact { get => contact; set => Set(ref contact, value, nameof(contact)); }
        // Выбранная дисциплина.
        /// </summary>
        public Subject SelectedSubject
        {
            get => selectedSubject; set
            {
                // Проверка, что выбран предмет.
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать дисциплину.");
                    return;
                }
                Set(ref selectedSubject, value, nameof(SelectedSubject));
            }
        }

        /// <summary>
        /// Список доступных дисциплин.
        /// </summary>
        public List<Subject> Subjects { get => _subjects; set => Set(ref _subjects, value, nameof(Subjects)); }

        /// <summary>
        /// Команда для сохранения изменений студента.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда для закрытия окна.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Проверяет корректность данных перед сохранением.
        /// </summary>
        /// <returns>Истина, если все проверки прошли успешно; иначе — ложь.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(FIO))
            {
                MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(Group))
            {
                MessageBox.Show("Поле 'Группа' не может быть пустым.");
                return false;
            }
            if (SelectedSubject == null)
            {
                MessageBox.Show("Необходимо выбрать дисциплину.");
                return false;
            }
            return true;
        }
    }
}
