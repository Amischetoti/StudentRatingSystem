using StudentRatingSystemApp.Commands;
using StudentRatingSystemLib.Entities;
using StudentRatingSystemDbContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using Microsoft.IdentityModel.Tokens;
using Azure;
using System.Windows.Controls;

namespace StudentRatingSystemApp.ViewModels
{
    /// <summary>
    /// Модель представления для редактирования оценки (Grade).
    /// </summary>
    public class GradeEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор класса GradeEditorViewModel.
        /// Инициализирует модель данными пользователя, оценки, сервиса оценок и списка квестов.
        /// </summary>
        /// <param name="user">Пользователь, связанный с оценкой.</param>
        /// <param name="grade">Редактируемая оценка (может быть null для создания новой).</param>
        /// <param name="gradeService">Сервис для работы с оценками.</param>
        /// <param name="quests">Список квестов.</param>
        /// <param name="students">Список студентов.</param>
        public GradeEditorViewModel(User user, Grade grade, GradeService gradeService, List<Quest> quests, List<Student> students)
        {
            Grade = grade;
            _gradeService = gradeService;
            _quests = quests;
            _students = students;
            if (grade != null)
            {
                ReceivedPoint = grade.ReceivedPoint;
                ExtraPoint = grade.ExtraPoint;
                DateOfAssessment = grade.DateOfAssessment;
                SelectedQuest = grade.Quest;
                SelectedStudent = grade.Student;
            }
            else 
            {
                DateOfAssessment = DateTime.Now;
            }
                /// <summary>
                /// Команда для сохранения оценки — добавляет новую или обновляет существующую.
                /// Проверяет корректность введенных данных и вызывает методы сервиса.
                /// </summary>
                SaveCommand = new RelayCommand(o =>
                {
                    if (!ValidateData()) return;
                    if (grade == null)
                    {
                        var newGrade = new Grade()
                        {
                            Grade_id = Guid.NewGuid(),
                            ReceivedPoint = this.ReceivedPoint,
                            ExtraPoint = this.ExtraPoint,
                            DateOfAssessment = this.DateOfAssessment,
                            QuestId = Guid.NewGuid(),
                            Quest = this.SelectedQuest,
                            StudentId = Guid.NewGuid(),
                            Student = this.SelectedStudent
                        };
                        _gradeService.Add(newGrade);
                        MessageBox.Show("Оценка добавлена");
                    }
                    else
                    {
                        _gradeService.Update(grade, new Grade()
                        {
                            Grade_id = Guid.NewGuid(),
                            ReceivedPoint = this.ReceivedPoint,
                            ExtraPoint = this.ExtraPoint,
                            DateOfAssessment = this.DateOfAssessment,
                            QuestId = Guid.NewGuid(),
                            Quest = this.SelectedQuest,
                            StudentId = Guid.NewGuid(),
                            Student = this.SelectedStudent
                        });
                        MessageBox.Show("Оценка изменена");
                    }
                });

            /// <summary>
            /// Команда для закрытия приложения.
            /// </summary>
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private decimal receivedPoint;
        private decimal extraPoint;
        private DateTime dateOfAssessment;
        private Quest selectedQuest;
        private Student selectedStudent;

        private GradeService _gradeService;
        private List<Quest> _quests;
        private List<Student> _students;
        private Grade grade;

        /// <summary>
        /// Свойство модели оценок.
        /// </summary>
        public Grade Grade { get => grade; set => Set(ref grade, value, nameof(grade)); }

        /// <summary>
        /// Полученные очки за выполнение задания.
        /// Не может быть отрицательным или равным нулю.
        /// </summary>
        public decimal ReceivedPoint
        {
            get => receivedPoint;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Поле 'Полученные очки' не может быть отрицательным.");
                    return;
                }
                if (value == 0)
                {
                    MessageBox.Show("Поле 'Полученные очки' не может быть пустым.");
                    return;
                }
                Set(ref receivedPoint, value, nameof(receivedPoint));
            }
        }

        /// <summary>
        /// Дополнительные очки за выполнение задания.
        /// Не могут быть отрицательными или равными нулю.
        /// </summary>
        public decimal ExtraPoint
        {
            get => extraPoint;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Поле 'Дополнительные очки' не может быть пустым.");
                    return;
                }
                if (value == 0)
                {
                    MessageBox.Show("Поле 'Дополнительные очки' не может быть отрицательным.");
                    return;
                }
                Set(ref extraPoint, value, nameof(extraPoint));
            }
        }

        /// <summary>
        /// Дата сдачи задания в виде строки.
        /// Не может быть пустой или null.
        /// </summary>
        public DateTime DateOfAssessment
        {
            get => dateOfAssessment;
            set
            {
                if (value == null)
                {
                    MessageBox.Show("Поле 'Дата сдачи' не может быть пустым.");
                    return;
                }
                Set(ref dateOfAssessment, value, nameof(dateOfAssessment));
            }
        }

        /// <summary>
        /// Выбранный квест (задание).
        /// Не может быть null.
        /// </summary>
        public Quest SelectedQuest
        {
            get => selectedQuest;
            set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать задание.");
                    return;
                }
                Set(ref selectedQuest, value, nameof(selectedQuest));
            }
        }

        /// <summary>
        /// Список доступных квестов.
        /// </summary>
        public List<Quest> Quests { get => _quests; set => Set(ref _quests, value, nameof(_quests)); }

        /// <summary>
        /// Получает или задает выбранного студента для задания.
        /// </summary>
        public Student SelectedStudent
        {
            get => selectedStudent; set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать студента.");
                    return;
                }
                Set(ref selectedStudent, value, nameof(selectedStudent));
            }
        }

        /// <summary>
        /// Получает или задает список студентов.
        /// </summary>
        public List<Student> Students { get => _students; set => Set(ref _students, value, nameof(_students)); }

        /// <summary>
        /// Команда сохранения оценки.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда закрытия окна или приложения.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Проверяет корректность введённых данных перед сохранением.
        /// Проверяет все поля на пустоту и отрицательные значения.
        /// В случае ошибки выводит соответствующие сообщения.
        /// </summary>
        /// <returns>True, если данные валидны, иначе false.</returns>
        private bool ValidateData()
        {
            if (ReceivedPoint == 0)
            {
                MessageBox.Show("Поле 'Полученные очки' не может быть пустым.");
                return false;
            }

            if (ReceivedPoint < 0)
            {
                MessageBox.Show("Поле 'Полученные очки' не может быть отрицательным.");
                return false;
            }

            if (ExtraPoint < 0)
            {
                MessageBox.Show("Поле 'Дополнительные очки' не может быть пустым.");
                return false;
            }

            if (ExtraPoint == 0)
            {
                MessageBox.Show("Поле 'Дополнительные очки' не может быть отрицательным.");
                return false;
            }

            if (DateOfAssessment == null)
            {
                MessageBox.Show("Поле 'Дата сдачи' не может быть пустым.");
                return false;
            }

            if (SelectedQuest == null)
            {
                MessageBox.Show("Необходимо выбрать задание.");
                return false;
            }
            if (SelectedStudent == null)
            {
                MessageBox.Show("Необходимо выбрать студента.");
                return false;
            }
            return true;
        }
    }
}
