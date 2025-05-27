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
    /// Логика взаимодействия для SubjectEditorView.xaml
    /// Представляет окно редактирования данных дисциплины.
    /// </summary>
    public partial class SubjectEditorView : Window
    {
        /// <summary>
        /// Модель представления для редактора предмета.
        /// </summary>
        private SubjectEditorViewModel viewModel;

        /// <summary>
        /// Конструктор окна редактора предмета.
        /// Инициализирует компоненты, модель представления и контекст данных.
        /// </summary>
        /// <param name="user">Пользователь, выполняющий редактирование.</param>
        /// <param name="subject">Объект предмета для редактирования, может быть null.</param>
        /// <param name="teachers">Список преподавателей, используемый для выбора у предмета, может быть null.</param>
        public SubjectEditorView(User user, Subject subject = null!, List<Teacher> teachers = null!)
        {
            InitializeComponent();

            var dbContext = DbContextSingleton.Instance.DbContext;

            /// <summary>
            /// Инициализация модели представления с необходимыми параметрами.
            /// </summary>
            viewModel = new(user, subject, new StudentRatingSystemDbContext.Services.SubjectService(), teachers);

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
