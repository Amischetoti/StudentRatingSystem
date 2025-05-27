using StudentRatingSystemDbContext.Connections;
using StudentRatingSystemApp.ViewModels;
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

namespace StudentRatingSystemApp.VIews
{
    /// <summary>
    /// Логика взаимодействия для StudentEditorView.xaml
    /// Представляет окно редактирования данных студента.
    /// </summary>
    public partial class StudentEditorView : Window
    {
        /// <summary>
        /// Модель представления для редактора студента.
        /// </summary>
        private StudentEditorViewModel viewModel;

        /// <summary>
        /// Конструктор окна редактора студента.
        /// Инициализирует компоненты, модель представления и контекст данных.
        /// </summary>
        /// <param name="user">Пользователь, выполняющий редактирование.</param>
        /// <param name="student">Объект студента для редактирования, может быть null.</param>
        /// <param name="subjects">Список предметов, используемый для выбора у студента, может быть null.</param>
        public StudentEditorView(User user, Student student = null!, List<Subject> subjects = null!)
        {
            InitializeComponent();

            var dbContext = DbContextSingleton.Instance.DbContext;

            /// <summary>
            /// Инициализация модели представления с необходимыми параметрами.
            /// </summary>
            viewModel = new(user, student, new StudentRatingSystemDbContext.Services.StudentService(), subjects);

            /// <summary>
            /// Установка контекста данных для связывания с UI.
            /// </summary>
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик нажатия кнопки закрытия окна.
        /// Закрывает окно.
        /// </summary>
        /// <param name="sender">Объект, отправивший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
