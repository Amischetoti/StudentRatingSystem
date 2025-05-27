using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentRatingSystemApp.Commands
{
    namespace StudentRatingSystemApp.Commands
    {
        /// <summary>
        /// Команда для асинхронного выполнения действия, реализующая интерфейс ICommand.
        /// Позволяет запускать асинхронные методы с поддержкой проверки возможности выполнения (CanExecute).
        /// </summary>
        public class AsyncRelayCommand : ICommand
        {
            private readonly Func<Task> _execute;
            private readonly Func<bool> _canExecute;
            private bool _isExecuting;

            /// <summary>
            /// Инициализирует новый экземпляр команды с указанным асинхронным методом и опциональной проверкой возможности выполнения.
            /// </summary>
            /// <param name="execute">Асинхронный метод для выполнения.</param>
            /// <param name="canExecute">Метод проверки возможности выполнения команды.</param>
            public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            /// <summary>
            /// Определяет, может ли команда быть выполнена в текущий момент.
            /// </summary>
            /// <param name="parameter">Параметр команды (не используется).</param>
            /// <returns>True, если команда может быть выполнена; иначе — false.</returns>
            public bool CanExecute(object parameter)
            {
                return !_isExecuting && (_canExecute?.Invoke() ?? true);
            }

            /// <summary>
            /// Событие, возникающее при изменении возможности выполнения команды.
            /// </summary>
            public event EventHandler CanExecuteChanged;

            /// <summary>
            /// Выполняет асинхронный метод команды.
            /// </summary>
            /// <param name="parameter">Параметр команды (не используется).</param>
            public async void Execute(object parameter)
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();

                try
                {
                    await _execute();
                }
                finally
                {
                    _isExecuting = false;
                    RaiseCanExecuteChanged();
                }
            }

            /// <summary>
            /// Вызывает событие <see cref="CanExecuteChanged"/>, уведомляя об изменении статуса возможности выполнения команды.
            /// </summary>
            protected void RaiseCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

    }

}
