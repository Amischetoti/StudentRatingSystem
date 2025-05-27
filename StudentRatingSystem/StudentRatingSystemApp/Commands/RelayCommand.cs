using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentRatingSystemApp.Commands
{
    /// <summary>
    /// Реализация интерфейса ICommand для синхронных команд с возможностью проверки.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие, выполняемое при вызове команды.
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Функция для проверки, может ли команда выполняться.
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Событие, вызываемое при изменении состояния возможности выполнения команды.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Инициализация новой команды с указанными методами выполнения и проверки.
        /// </summary>
        /// <param name="execute">Метод, выполняемый командой.</param>
        /// <param name="canExecute">Метод, проверяющий возможность выполнения команды. Необязательный.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null!)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Проверяет, может ли команда быть выполнена в текущий момент.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True, если команда может быть выполнена; иначе — false.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter!);
        }

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object? parameter)
        {
            _execute(parameter!);
        }
    }
}
