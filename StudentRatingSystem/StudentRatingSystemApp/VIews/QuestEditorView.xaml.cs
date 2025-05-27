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
    /// Логика взаимодействия для QuestEditorView.xaml
    /// Представляет окно редактора заданий.
    /// </summary>
    public partial class QuestEditorView : Window
    {
        /// <summary>
        /// Модель представления для редактора квеста.
        /// </summary>
        private QuestEditorViewModel viewModel;

        /// <summary>
        /// Конструктор окна редактора квеста.
        /// Инициализирует модель представления и привязывает её к контексту данных.
        /// </summary>
        /// <param name="user">Пользователь, оформляющий задание.</param>
        /// <param name="quest">Объект задания для редактирования; может быть null для создания нового.</param>
        /// <param name="students">Список студентов, используемый для выбора в редакторе; может быть null.</param>
        public QuestEditorView(User user, Quest quest = null!, List<Student> students = null!)
        {
            InitializeComponent();

            var dbContext = DbContextSingleton.Instance.DbContext;

            /// <summary>
            /// Инициализация модели представления с необходимыми параметрами.
            /// </summary>
            viewModel = new(user, quest, new StudentRatingSystemDbContext.Services.QuestService(), students);

            /// <summary>
            /// Установка контекста данных для UI.
            /// </summary>
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик события нажатия кнопки закрытия окна.
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
