using StudentRatingSystemApp.VIews;
using System.Configuration;
using System.Data;
using System.Windows;

namespace StudentRatingSystemApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// Обеспечивает жизненный цикл и обработку событий приложения.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Переопределение метода запуска приложения.
        /// После запуска отображает окно регистрации.
        /// </summary>
        /// <param name="e">Аргументы запуска.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            /// <summary>
            /// Создание и отображение окна регистрации.
            /// </summary>
            RegistrationWindow registrationWindow = new RegistrationWindow();

            /// <summary>
            /// Показать окно регистрации пользователю.
            /// </summary>
            registrationWindow.Show();
        }
    }
}
