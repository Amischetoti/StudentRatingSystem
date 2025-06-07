using StudentRatingSystemApp.Commands;
using StudentRatingSystemDbContext.Services;
using StudentRatingSystemLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StudentRatingSystemApp.ViewModels
{
    /// <summary>
    /// ViewModel для редактора заданий.  Позволяет создавать и редактировать задания для студентов.
    /// </summary>
    public class QuestEditorViewModel : ViewModelBase
    {
           /// <summary>
           /// Конструктор класса <see cref="QuestEditorViewModel"/>.
           /// </summary>
           /// <param name="user">Пользователь, осуществляющий редактирование.</param>
           /// <param name="quest">Редактируемое задание. Если <c>null</c>, создается новое задание.</param>
           /// <param name="questService">Сервис для работы с заданиями.</param>
        public QuestEditorViewModel(User user, Quest quest, QuestService questService)
        {
            Quest = quest;
            _questService = questService;
            if (quest != null)
            {
                TypeOfTask = quest.TypeOfTask;
                DateOfCompletion = quest.DateOfCompletion;
                NumberOfPoints = quest.NumberOfPoints;
            }
            else 
            {
                DateOfCompletion = DateTime.Now;
            }

                SaveCommand = new RelayCommand(o =>
                {
                    if (!ValidateData()) return;
                    if (quest == null)
                    {
                        _questService.Add(new Quest() { Quest_id = Guid.NewGuid(), TypeOfTask = this.TypeOfTask, DateOfCompletion = this.DateOfCompletion, NumberOfPoints = this.NumberOfPoints });
                        MessageBox.Show("Задание добавлено!");
                    }
                    else
                    {

                        _questService.Update(quest, new Quest() { Quest_id = Guid.NewGuid(), TypeOfTask = this.TypeOfTask, DateOfCompletion = this.DateOfCompletion, NumberOfPoints = this.NumberOfPoints });
                        MessageBox.Show("Задание изменено!");
                    }
                });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }
        private string typeOfTask = string.Empty;
        private DateTime dateOfCompletion;
        private string numberOfPoints = string.Empty;
        private QuestService _questService;

        private Quest quest;
        /// <summary>
        /// Получает или задает редактируемое задание.
        /// </summary>
        public Quest Quest { get => quest; set => Set(ref quest, value, nameof(quest)); }

        /// <summary>
        /// Получает или задает тип задания.
        /// </summary>
        public string TypeOfTask
        {
            get => typeOfTask; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Тип задания' не может быть пустым.");
                    return;
                }
                Set(ref typeOfTask, value, nameof(typeOfTask));
            }
        }

        /// <summary>
        /// Получает или задает дату выполнения задания.
        /// </summary>
        public DateTime DateOfCompletion { get => dateOfCompletion; set
            {
                if (value == null)
                {
                    MessageBox.Show("Поле 'Дата выдачи задания' не может быть пустым.");
                    return;
                }
                Set(ref dateOfCompletion, value, nameof(dateOfCompletion));
            }
        }

        /// <summary>
        /// Получает или задает количество баллов за задание.
        /// </summary>
        public string NumberOfPoints
        {
            get => numberOfPoints; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Критерии оценивания' не может быть пустым.");
                    return;
                }
                Set(ref numberOfPoints, value, nameof(numberOfPoints));
            }
        }

        /// <summary>
        /// Команда сохранения задания.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда закрытия редактора заданий.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Валидирует данные, введенные пользователем.
        /// </summary>
        /// <returns><c>true</c>, если данные валидны, иначе <c>false</c>.</returns>
        private bool ValidateData()
        {
            if (DateOfCompletion == null)
            {
                MessageBox.Show("Поле 'Дата выдачи задания' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(TypeOfTask))
            {
                MessageBox.Show("Поле 'Тип задания' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(NumberOfPoints))
            {
                MessageBox.Show("Поле 'Критерии оценивания' не может быть пустым.");
                return false;
            }
            return true;
        }

    }
}
