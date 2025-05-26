# Модуль системы рейтинга студентов
Модуль, предназначенный для удобного заполнения и просмотра рейтинга студентов. \
\
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)
![SQLite](https://img.shields.io/badge/sqlite-%2307405e.svg?style=for-the-badge&logo=sqlite&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)


## Содержание
- [Введение](#введение)
- [Установка](#установка)
- [Библиотеки](#библиотеки)
- [Базы данных](#базы-данных)
- [XML документация](#xml-документация)
- [Диаграммы сущностей](#диаграммы-сущностей)
- [Инструкция](#инструкция)
  
## Введение
Программный модуль создан для удобной регистрации и изменений данных кадров детского сада.

## Установка
Для установки в **`Bash`** или **`Power Shell`** введите эту команду:
```
https://github.com/QuanYizhenpls/Kindergarten.git
```

## Библиотеки
* [Microsft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
* [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/10.0.0-preview.2.25163.8)
* [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/10.0.0-preview.2.25163.8)
* [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/10.0.0-preview.2.25163.8)

## Базы данных
В проекте есть возможность использовать такие варианты БД как MS Sql Server и Sqlite.

## XML Докумеентация
Ссылка на XML документацию:
[StudentRatingSystemApp](https://github.com/QuanYizhenpls/Kindergarten/blob/main/KinderApp.xml),
[StudentRatingSystemLib](https://github.com/QuanYizhenpls/Kindergarten/blob/main/KinderData.xml),
[StudentRatingSystemDbContext](https://github.com/QuanYizhenpls/Kindergarten/blob/main/KinderDbContext.xml).

## Диаграммы сущностей
![Диаграмма переходов состояний](https://github.com/QuanYizhenpls/Kindergarten/blob/main/Диаграмма%20переходов%20состояний.drawio)
![Диаграмма сущность-связь](https://github.com/QuanYizhenpls/Kindergarten/blob/main/Диаграмма%20сущность-связь.drawio)
![Диаграмма потоков данных](https://github.com/QuanYizhenpls/Kindergarten/blob/main/Диаграмма%20потоков%20данных.drawio)
![Функциональная диаграмма](https://github.com/QuanYizhenpls/Kindergarten/blob/main/Функциональная%20диаграмма.drawio)

## Инструкция
Краткое руководство использования:
> [!IMPORTANT]
> Перед началом работы с программой необходимо убедиться в том, что на ПК пользователя установлена Visual Studio с установленными библиотеками.

  Регистрация новых пользователей осуществляется непосредственно в программе. На экране будет открыта форма регистрации, в которой необходимо заполнить следущие поля:
 - ФАМИЛИЯ
 - ИМЯ
 - ОТЧЕСТВО
 - ЛОГИН
 - ПАРОЛЬ

  После регистрации пользователя при перезаходе в программу достаточно заполнить следущие поля в форме входа:
 - ЛОГИН
 - ПАРОЛЬ
 
  Далее вам будет доступен экран меню, на котором вы можете просмотреть следующие вкладки:
 - Сотрудники
 - Воспитанники
 - Группы
 - Договоры
 - Данные сотрудников
 - Зарплата
 - Планы
   
  На каждой вкладке вы обнаружите кнопки для добавления, изменения и удаления данных.
