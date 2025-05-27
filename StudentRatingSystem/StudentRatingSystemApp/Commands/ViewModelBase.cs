using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using System.Windows;
using System.Diagnostics;

namespace StudentRatingSystemApp.Commands
{
    /// <summary>
    /// Базовый класс для моделей представления (ViewModel) с поддержкой уведомления об изменении свойств и управлением окнами.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие, возникающее при изменении значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Устанавливает новое значение поля и оповещает об изменении свойства, если значение изменилось.
        /// </summary>
        /// <typeparam name="T">Тип свойства.</typeparam>
        /// <param name="field">Ссылка на поле свойства.</param>
        /// <param name="value">Новое значение для установки.</param>
        /// <param name="propertyName">Имя свойства, по умолчанию передаётся автоматически.</param>
        /// <returns>True, если значение изменилось и было установлено; иначе – false.</returns>
        protected bool Set<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/>, уведомляя о том, что указанное свойство изменилось.
        /// </summary>
        /// <param name="propertyName">Имя изменённого свойства.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Открывает новое окно, закрывая текущее главное окно приложения.
        /// </summary>
        /// <param name="window">Окно, которое нужно открыть.</param>
        /// <returns>Открытое окно.</returns>
        protected Window OpenWindow(Window window)
        {
            var temp = Application.Current.MainWindow;
            Application.Current.MainWindow = window;
            temp.Close();
            window.Show();
            return window;
        }

        /// <summary>
        /// Открывает модальное диалоговое окно с эффектом размытия основного окна.
        /// </summary>
        /// <param name="window">Диалоговое окно для отображения.</param>
        /// <returns>Открытое окно.</returns>
        protected Window OpenWindowDialog(Window window)
        {
            try
            {
                Application.Current.MainWindow.Effect = new BlurEffect();
                window.ShowDialog();
                Application.Current.MainWindow.Effect = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
            }

            return window;
        }

        /// <summary>
        /// Завершает работу приложения.
        /// </summary>
        protected void AppClose()
        {
            Application.Current.Shutdown();
        }
    }
}

