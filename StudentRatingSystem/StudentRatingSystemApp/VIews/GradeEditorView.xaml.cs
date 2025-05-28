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
    /// Логика взаимодействия для GradeEditorView.xaml
    /// Представляет окно редактора оценок. 
    /// </summary>
    public partial class GradeEditorView : Window
    {
        /// <summary>
        /// Объект модели представления для редактора оценки.
        /// </summary>
        private GradeEditorViewModel viewModel;

        /// <summary>
        /// Конструктор окна редактора оценки.
        /// Инициализирует компоненты и устанавливает контекст данных.
        /// </summary>
        /// <param name="user">Пользователь, связанный с текущим действием.</param>
        /// <param name="grade">Объект оценки для редактирования. Может быть null для добавления новой оценки.</param>
        /// <param name="quests">Список вопросов, связанных с оценкой. Может быть null.</param>
        /// <param name="students">Список студентов, связанных с оценкой. Может быть null.</param>
        public GradeEditorView(User user, Grade grade = null!, List<Quest> quests = null!, List<Student> students = null!)
        {
            InitializeComponent();
            var gradeService = new StudentRatingSystemDbContext.Services.GradeService();

            /// <summary>
            /// Инициализация модели представления с необходимыми данными.
            /// </summary>
            viewModel = new(user, grade, gradeService, quests, students);

            /// <summary>
            /// Установка контекста данных для связывания.
            /// </summary>
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик нажатия кнопки для закрытия окна.
        /// </summary>
        /// <param name="sender">Объект-отправитель события.</param>
        /// <param name="e">Аргументы события.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
