using StudentRatingSystemApp.ViewModels;
using StudentRatingSystemDbContext.Connections;
using StudentRatingSystemDbContext.Services;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentRatingSystemApp
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// Представляет главное окно меню приложения.
    /// </summary>
    public partial class MenuWindow : Window
    {
        /// <summary>
        /// Модель представления для окна меню.
        /// </summary>
        private MenuWindowModel viewModel;

        /// <summary>
        /// Конструктор окна меню.
        /// Инициализирует сервисы и устанавливает модель представления и контекст данных.
        /// </summary>
        /// <param name="user">Пользователь, вошедший в систему.</param>
        public MenuWindow(User user)
        {
            InitializeComponent();

            // Инициализация сервисов для работы с данными
            var gradeService = new StudentRatingSystemDbContext.Services.GradeService();
            var questService = new StudentRatingSystemDbContext.Services.QuestService();
            var studentService = new StudentRatingSystemDbContext.Services.StudentService();
            var subjectService = new StudentRatingSystemDbContext.Services.SubjectService();
            var teacherService = new StudentRatingSystemDbContext.Services.TeacherService();

            /// <summary>
            /// Создание модели представления с переданными сервисами.
            /// </summary>
            viewModel = new MenuWindowModel(
                user,
                gradeService,
                questService,
                studentService,
                subjectService,
                teacherService
            );

            /// <summary>
            /// Установка контекста данных для взаимодействия с UI.
            /// </summary>
            DataContext = viewModel;

            /// <summary>
            /// Установка заголовка окна с именем пользователя.
            /// </summary>
            Title = $"Окно пользователя: {user.Fullname}";
        }

    }
}
