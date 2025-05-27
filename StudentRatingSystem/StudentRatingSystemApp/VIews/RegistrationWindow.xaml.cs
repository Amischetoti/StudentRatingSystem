using StudentRatingSystemApp.ViewModels;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// Представляет окно регистрации пользователя.
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        /// <summary>
        /// Модель представления для окна регистрации.
        /// </summary>
        private RegistrationWindowModel _viewModel;

        /// <summary>
        /// Конструктор окна регистрации.
        /// Инициализирует модель представления и задаёт контекст данных.
        /// </summary>
        public RegistrationWindow()
        {
            _viewModel = new RegistrationWindowModel();
            InitializeComponent();
            /// <summary>
            /// Установка контекста данных для связывания UI с моделью.
            /// </summary>
            DataContext = _viewModel = new RegistrationWindowModel();
        }
    }
}
