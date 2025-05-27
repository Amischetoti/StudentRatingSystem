using StudentRatingSystemApp.Commands;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using StudentRatingSystemDbContext.Services;

namespace StudentRatingSystemApp.ViewModels
{
    /// <summary>
    /// Модель окна регистрации пользователя.
    /// Управляет процессом входа, регистрации и закрытия окна.
    /// </summary>
    public class RegistrationWindowModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RegistrationWindowModel"/>.
        /// Настраивает команды для входа, регистрации и закрытия окна.
        /// </summary>
        public RegistrationWindowModel()
        {
            /// <summary>
            /// Команда входа пользователя.
            /// Пытается получить аккаунт по введенным логину и паролю.
            /// В случае успешного входа открывает главное меню и закрывает текущее окно.
            /// Если пользователь не найден — показывает сообщение об ошибке.
            /// </summary>
            LoginCommand = new RelayCommand(o =>
            {
                var user = new UserService().GetAccount(Login, Password).Result;
                if (user != null)
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь найден!");
                    MessageBox.Show($"[{GetType()}] - пользователь найден!");
                    var newWin = new MenuWindow(user);
                    var pervWin = Application.Current.MainWindow;
                    Application.Current.MainWindow = newWin;
                    pervWin.Close();
                    newWin.Show();
                }
                else
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь не найден!");
                    MessageBox.Show($"[{GetType()}] - пользователь не найден!");
                }
            });

            /// <summary>
            /// Команда регистрации нового пользователя.
            /// Создает нового пользователя с указанными данными и пытается добавить его через сервис.
            /// В случае успеха отображает подтверждающее сообщение, иначе сообщение об ошибке.
            /// </summary>
            RegisterCommand = new RelayCommand(o =>
            {
                if (new UserService().Add(new User()
                {
                    Id = Guid.NewGuid(),
                    Firstname = firstname,
                    Lastname = lastname,
                    Middlename = middlename,
                    Login = login,
                    Password = password
                }).Result)
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь создан!");
                    MessageBox.Show($"[{GetType()}] - пользователь создан!");
                }
                else
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь не был создан!!");
                    MessageBox.Show($"[{GetType()}] - пользователь не был создан!");
                }
            });

            /// <summary>
            /// Команда закрытия окна регистрации.
            /// Вызывает метод <see cref="AppClose"/> для закрытия приложения или окна.
            /// </summary>
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string middlename = string.Empty;
        private string login = string.Empty;
        private string password = string.Empty;

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Firstname { get => firstname; set => Set(ref firstname, value, nameof(Firstname)); }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string Lastname { get => lastname; set => Set(ref lastname, value, nameof(Lastname)); }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        public string Middlename { get => middlename; set => Set(ref middlename, value, nameof(Middlename)); }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get => login; set => Set(ref login, value, nameof(Login)); }
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string Password { get => password; set => Set(ref password, value, nameof(Password)); }

        /// <summary>
        /// Команда для входа пользователя.
        /// </summary>
        public RelayCommand LoginCommand { get; }

        /// <summary>
        /// Команда для регистрации нового пользователя.
        /// </summary>
        public RelayCommand RegisterCommand { get; }

        /// <summary>
        /// Команда для закрытия окна регистрации.
        /// </summary>
        public RelayCommand CloseCommand { get; }
    }
}