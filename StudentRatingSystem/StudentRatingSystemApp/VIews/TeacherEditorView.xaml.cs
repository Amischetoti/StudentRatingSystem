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
    /// Логика взаимодействия для TeacherEditorView.xaml
    /// Представляет окно редактирования данных преподавателя.
    /// </summary>
    public partial class TeacherEditorView : Window
    {
        /// <summary>
        /// Модель представления для редактора преподавателя.
        /// </summary>
        private TeacherEditorViewModel viewModel;

        /// <summary>
        /// Конструктор окна редактора преподавателя.
        /// Инициализирует компоненты, модель представления и контекст данных.
        /// </summary>
        /// <param name="user">Пользователь, выполняющий редактирование.</param>
        /// <param name="teacher">Объект преподавателя для редактирования, может быть null.</param>
        public TeacherEditorView(User user, Teacher teacher = null!)
        {
            InitializeComponent();

            /// <summary>
            /// Инициализация модели представления с необходимыми параметрами.
            /// </summary>
            viewModel = new(user, teacher, new StudentRatingSystemDbContext.Services.TeacherService());

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
