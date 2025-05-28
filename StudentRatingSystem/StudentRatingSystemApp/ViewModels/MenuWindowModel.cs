using StudentRatingSystemApp.Commands;
using StudentRatingSystemApp.Commands.StudentRatingSystemApp.Commands;
using StudentRatingSystemApp.VIews;
using StudentRatingSystemDbContext.Services;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace StudentRatingSystemApp.ViewModels
{
    /// <summary>
    /// Модель представления для основного меню приложения.
    /// Обеспечивает взаимодействие с данными пользователей, оценок, заданий, студентов, предметов и преподавателей.
    /// </summary>
    public class MenuWindowModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор класса MenuWindowModel.
        /// Инициализирует сервисы, текущего пользователя и загружает начальные данные.
        /// Также инициализирует команды для работы с оценками.
        /// </summary>
        /// <param name="user">Текущий пользователь приложения.</param>
        /// <param name="gradeService">Сервис для работы с оценками.</param>
        /// <param name="questService">Сервис для работы с заданиями (квестами).</param>
        /// <param name="studentService">Сервис для работы со студентами.</param>
        /// <param name="subjectService">Сервис для работы с предметами.</param>
        /// <param name="teacherService">Сервис для работы с преподавателями.</param>
        public MenuWindowModel(User user, GradeService gradeService, QuestService questService, StudentService studentService, SubjectService subjectService, TeacherService teacherService)
        {
            CurrentUser = user;
            _gradeService = gradeService;
            _questService = questService;
            _studentService = studentService;
            _subjectService = subjectService;
            _teacherService = teacherService;
            UpdateLists();

            /// <summary>
            /// Асинхронная команда добавления новой оценки.
            /// Открывает окно редактирования новой оценки и обновляет список оценок по выбранному квесту.
            /// </summary>
            GAddCommand = new AsyncRelayCommand(async () =>
            {
                var gradeEditorView = new GradeEditorView(CurrentUser, quests: Quests, students: Students);
                gradeEditorView.ShowDialog();

                // Получаем выбранный квест из ViewModel GradeEditorView
                var vm = gradeEditorView.DataContext as GradeEditorViewModel;

                if (vm != null && vm.SelectedQuest != null)
                {
                    Grades = (List<Grade>)await _gradeService.UpdateAndGetGrades(vm.SelectedQuest.Quest_id);
                }
                else
                {
                    UpdateLists();
                }
            });

            /// <summary>
            /// Асинхронная команда обновления существующей оценки.
            /// Проверяет выбранную оценку, открывает окно редактирования и обновляет список оценок.
            /// </summary>
            GUpdateCommand = new AsyncRelayCommand(async () =>
            {
                /// <summary>
                /// Проверка наличия выбранной оценки.
                /// </summary>
                if (SelectedGrade != null)
                {
                    /// <summary>
                    /// Создание и отображение окна редактора оценки для выбранной оценки.
                    /// </summary>
                    var gradeEditorView = new GradeEditorView(CurrentUser, SelectedGrade, quests: Quests, students: Students);
                    gradeEditorView.ShowDialog();

                    /// <summary>
                    /// Обновляем список оценок по идентификатору квеста.
                    /// </summary>
                    Grades = (List<Grade>)await _gradeService.UpdateAndGetGrades(SelectedGrade.QuestId);
                }
                else
                {
                    /// <summary>
                    /// Сообщение при отсутствии выбранной оценки.
                    /// </summary>
                    MessageBox.Show("Выберите оценку для обновления");
                }
            });

            /// <summary>
            /// Асинхронная команда для удаления выбранной оценки.
            /// Проверяет, выбран ли оценка, удаляет ее и обновляет список.
            /// </summary>
            GRemoveCommand = new AsyncRelayCommand(async () =>
            {
                /// <summary>
                /// Проверка наличия выбранной оценки.
                /// </summary>
                if (SelectedGrade != null)
                {
                    /// <summary>
                    /// Удаление оценки через сервис.
                    /// </summary>
                    _gradeService.Remove(SelectedGrade);

                    /// <summary>
                    /// Обновление списка оценок по идентификатору квеста.
                    /// </summary>
                    Grades = (List<Grade>)await _gradeService.UpdateAndGetGrades(SelectedGrade.QuestId);
                }
                else
                {
                    /// <summary>
                    /// Сообщение, если оценка не выбрана.
                    /// </summary>
                    MessageBox.Show("Выберите оценку для удаления");
                }
            });

            /// <summary>
/// Команда для добавления нового задания (квеста).
/// Создает диалоговое окно редактора задания и обновляет список.
/// </ summary >
QAddCommand = new RelayCommand(o =>
{
    // Открывает окно редактора для создания нового задания, передавая текущего пользователя и список студентов.
    OpenWindowDialog(new QuestEditorView(CurrentUser));
    // Обновляет все списки после добавления.
    UpdateLists();
});

/// <summary>
/// Команда для редактирования выбранного задания (квеста).
/// Проверяет, выбран ли квест, и в случае успеха открывает редактор для редактирования.
/// В случае ошибки выводит сообщение.
/// </ summary >
QUpdateCommand = new RelayCommand(o =>
{
    // Проверка, что выбран квест.
    if (SelectedQuest != null)
    {
        try
        {
            // Открывает окно редактора с выбранным заданием для редактирования.
            OpenWindowDialog(new QuestEditorView(CurrentUser, SelectedQuest));
            // Обновляет списки после редактирования.
            UpdateLists();
        }
        catch (Exception ex)
        {
            // Если возникла ошибка, выводит информацию в Debug и сообщение пользователю.
            Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
            MessageBox.Show($"{ex.GetType()} - {ex.Message}");
        }
    }
    else
    {
        // Сообщение, если пользователь не выбрал задание для редактирования.
        MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
    }
});

/// <summary>
/// Команда для удаления выбранного задания (квеста).
/// Проверяет, выбран ли квест, и при наличии удаляет его через сервис.
/// В случае ошибок — выводит сообщение.
/// </ summary >
QRemoveCommand = new RelayCommand(o =>
{
    // Проверка, что выбран квест.
    if (SelectedQuest != null)
    {
        try
        {
            // Удаляет выбранный квест через сервис.
            _questService.Remove(SelectedQuest);
            // Обновляет списки после удаления.
            UpdateLists();
        }
        catch (Exception ex)
        {
            // Обработка ошибок: вывод в Debug и сообщение.
            Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
            MessageBox.Show($"{ex.GetType()} - {ex.Message}");
        }
    }
    else
    {
        // Сообщение, если не выбран квест для удаления.
        MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
    }
});

            /// <summary>
            /// Команда для добавления нового студента.
            /// Создает диалоговое окно редактора студента и обновляет списки.
            /// </summary>
            StAddCommand = new RelayCommand(o =>
            {
                // Открывает окно редактора для нового студента, передавая текущего пользователя и список предметов.
                OpenWindowDialog(new StudentEditorView(CurrentUser, subjects: Subjects));
                // Обновляет списки после добавления.
                UpdateLists();
            });

            /// <summary>
            /// Команда для редактирования выбранного студента.
            /// Проверяет, выбран ли студент, и в случае успеха открывает редактор.
            /// В случае ошибок — выводит сообщение.
            /// </summary>
            StUpdateCommand = new RelayCommand(o =>
            {
                // Проверка, что выбран студент.
                if (SelectedStudent != null)
                {
                    try
                    {
                        // Открывает окно редактора с выбранным студентом для редактирования.
                        OpenWindowDialog(new StudentEditorView(CurrentUser, SelectedStudent, subjects: Subjects));
                        // Обновляет списки после редактирования.
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранного студента.
            /// Проверяет, выбран ли студент, и при наличии вызывает сервис на удаление.
            /// В случае ошибок — выводит сообщение.
            /// </summary>
            StRemoveCommand = new RelayCommand(o =>
            {
                // Проверка, что выбран студент.
                if (SelectedStudent != null)
                {
                    try
                    {
                        // Удаляет выбранного студента через сервис.
                        _studentService.Remove(SelectedStudent);
                        // Обновляет списки после удаления.
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок.
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    // Сообщение о необходимости выбрать студента для удаления.
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для добавления нового предмета.
            /// Открывает окно редактирования предмета и обновляет списки после закрытия.
            /// </summary>
            SuAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new SubjectEditorView(CurrentUser, teachers: Teachers));
                UpdateLists();
            });

            /// <summary>
            /// Команда для обновления выбранного предмета.
            /// Проверяет, выбран ли предмет, открывает окно редактирования с текущими данными предмета и обновляет списки.
            /// В случае ошибки выводит сообщение и записывает в отладочный вывод.
            /// </summary>
            SuUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedSubject != null)
                {
                    try
                    {
                        OpenWindowDialog(new SubjectEditorView(CurrentUser, SelectedSubject, teachers: Teachers));
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранного предмета.
            /// Проверяет, выбран ли предмет, удаляет его через сервис и обновляет списки.
            /// В случае ошибки выводит сообщение и записывает в отладочный вывод.
            /// </summary>
            SuRemoveCommand = new RelayCommand(o =>
            {
                if (SelectedSubject != null)
                {
                    try
                    {
                        _subjectService.Remove(SelectedSubject);
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для добавления нового преподавателя.
            /// Открывает окно редактирования преподавателя и обновляет списки после закрытия.
            /// </summary>
            TAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new TeacherEditorView(CurrentUser));
                UpdateLists();
            });

            /// <summary>
            /// Команда для обновления выбранного преподавателя.
            /// Проверяет, выбран ли преподаватель, открывает окно редактирования и обновляет списки.
            /// В случае ошибки выводит сообщение и записывает в отладочный вывод.
            /// </summary>
            TUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedTeacher != null)
                {
                    try
                    {
                        OpenWindowDialog(new TeacherEditorView(CurrentUser, SelectedTeacher));
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранного преподавателя.
            /// Проверяет, выбран ли преподаватель, удаляет его через сервис и обновляет списки.
            /// В случае ошибки выводит сообщение и записывает в отладочный вывод.
            /// </summary>
            TRemoveCommand = new RelayCommand(o =>
            {
                if (SelectedTeacher != null)
                {
                    try
                    {
                        _teacherService.Remove(SelectedTeacher);
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });
        }
       /// <summary>
       /// Обновляет списки сущностей, получая их из соответствующих сервисов и сортируя/преобразуя по необходимости.
       /// Использует синхронные вызовы Result, что может блокировать UI.
       /// </summary>
        void UpdateLists()
        {
            /// <summary>
            /// Обновление списка оценок, сортировка по убыванию FinalScore.
            /// </summary>
            Grades = _gradeService.GetEntities().Result.OrderByDescending(g => g.FinalScore).ToList();

            /// <summary>
            /// Обновление списка заданий.
            /// </summary>
            Quests = _questService.GetEntities().Result.ToList();

            /// <summary>
            /// Обновление списка студентов.
            /// </summary>
            Students = _studentService.GetEntities().Result.ToList();

            /// <summary>
            /// Обновление списка предметов.
            /// </summary>
            Subjects = _subjectService.GetEntities().Result.ToList();

            /// <summary>
            /// Обновление списка преподавателей.
            /// </summary>
            Teachers = _teacherService.GetEntities().Result.ToList();
        }

        /// <summary>
        /// Текущий пользователь системы.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Сервис для работы с оценками.
        /// </summary>
        private GradeService _gradeService;

        /// <summary>
        /// Сервис заданий (квестов).
        /// </summary>
        private QuestService _questService;

        /// <summary>
        /// Сервис студентов.
        /// </summary>
        private StudentService _studentService;

        /// <summary>
        /// Сервис предметов.
        /// </summary>
        private SubjectService _subjectService;

        /// <summary>
        /// Сервис преподавателей.
        /// </summary>
        private TeacherService _teacherService;

        /// <summary>
        /// Выбранная оценка.
        /// </summary>
        private Grade selectedGrade;

        /// <summary>
        /// Выбранное задание.
        /// </summary>
        private Quest selectedQuest;

        /// <summary>
        /// Выбранный студент.
        /// </summary>
        private Student selectedStudent;

        /// <summary>
        /// Выбранная дисциплина.
        /// </summary>
        private Subject selectedSubject;

        /// <summary>
        /// Выбранный преподаватель.
        /// </summary>
        private Teacher selectedTeacher;

        /// <summary>
        /// Список оценок.
        /// </summary>
        private List<Grade> _grades;

        /// <summary>
        /// Список заданий.
        /// </summary>
        private List<Quest> _quests;

        /// <summary>
        /// Список студентов.
        /// </summary>
        private List<Student> _students;

        /// <summary>
        /// Список предметов.
        /// </summary>
        private List<Subject> _subjects;

        /// <summary>
        /// Список преподавателей.
        /// </summary>
        private List<Teacher> _teachers;

        /// <summary>
        /// Свойство текущего пользователя.
        /// </summary>
        public User CurrentUser
        {
            get => currentUser;
            set => Set(ref currentUser, value, nameof(CurrentUser));
        }

        /// <summary>
        /// Свойство выбранной оценки.
        /// </summary>
        public Grade SelectedGrade { get => selectedGrade; set => Set(ref selectedGrade, value, nameof(SelectedGrade)); }

        /// <summary>
        /// Свойство выбранного задания (квеста).
        /// </summary>
        public Quest SelectedQuest { get => selectedQuest; set => Set(ref selectedQuest, value, nameof(SelectedQuest)); }

        /// <summary>
        /// Свойство выбранного студента.
        /// </summary>
        public Student SelectedStudent { get => selectedStudent; set => Set(ref selectedStudent, value, nameof(SelectedStudent)); }

        /// <summary>
        /// Свойство выбранного предмета.
        /// </summary>
        public Subject SelectedSubject { get => selectedSubject; set => Set(ref selectedSubject, value, nameof(SelectedSubject)); }

        /// <summary>
        /// Свойство выбранного преподавателя.
        /// </summary>
        public Teacher SelectedTeacher { get => selectedTeacher; set => Set(ref selectedTeacher, value, nameof(SelectedTeacher)); }

        /// <summary>
        /// Свойство списка оценок.
        /// </summary>
        public List<Grade> Grades { get => _grades; set => Set(ref _grades, value, nameof(Grades)); }

        /// <summary>
        /// Свойство списка заданий.
        /// </summary>
        public List<Quest> Quests { get => _quests; set => Set(ref _quests, value, nameof(Quests)); }

        /// <summary>
        /// Свойство списка студентов.
        /// </summary>
        public List<Student> Students { get => _students; set => Set(ref _students, value, nameof(Students)); }

        /// <summary>
        /// Свойство списка предметов.
        /// </summary>
        public List<Subject> Subjects { get => _subjects; set => Set(ref _subjects, value, nameof(Subjects)); }

        /// <summary>
        /// Свойство списка преподавателей.
        /// </summary>
        public List<Teacher> Teachers { get => _teachers; set => Set(ref _teachers, value, nameof(Teachers)); }


        /// <summary>
        /// Команды для работы с оценками.
        /// </summary>
        public AsyncRelayCommand GAddCommand { get; }
        public AsyncRelayCommand GUpdateCommand { get; }
        public AsyncRelayCommand GRemoveCommand { get; }

        /// <summary>
        /// Команды для работы с заданиями (квестами).
        /// </summary>
        public RelayCommand QAddCommand { get; }
        public RelayCommand QUpdateCommand { get; }
        public RelayCommand QRemoveCommand { get; }

        /// <summary>
        /// Команды для работы с результатами (оценки или итоговые баллы).
        /// </summary>
        public RelayCommand RAddCommand { get; }
        public RelayCommand RUpdateCommand { get; }
        public RelayCommand RRemoveCommand { get; }

        /// <summary>
        /// Команды для работы с студентами.
        /// </summary>
        public RelayCommand StAddCommand { get; }
        public RelayCommand StUpdateCommand { get; }
        public RelayCommand StRemoveCommand { get; }

        /// <summary>
        /// Команды для работы с предметами.
        /// </summary>
        public RelayCommand SuAddCommand { get; }
        public RelayCommand SuUpdateCommand { get; }
        public RelayCommand SuRemoveCommand { get; }

        /// <summary>
        /// Команды для работы с преподавателями.
        /// </summary>
        public RelayCommand TAddCommand { get; }
        public RelayCommand TUpdateCommand { get; }
        public RelayCommand TRemoveCommand { get; }
    }
}
