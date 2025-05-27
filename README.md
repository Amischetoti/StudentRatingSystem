# Модуль мотивационной системы рейтинга студентов
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
Программный модуль создан для удобной регистрации и изменений данных мотивационной системы рейтинга студентов.

## Установка
Для установки в **`Bash`** или **`Power Shell`** введите эту команду:
```
https://github.com/Amischetoti/StudentRatingSystem.git
```

## Библиотеки
* [Microsft.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
* [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/10.0.0-preview.2.25163.8)
* [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/10.0.0-preview.2.25163.8)
* [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/10.0.0-preview.2.25163.8)

## Базы данных
В проекте есть возможность использовать такой вариант БД как MS Sql Server.

## XML Докумеентация
Ссылка на XML документацию:
[StudentRatingSystemApp](https://github.com/Amischetoti/StudentRatingSystem/blob/main/StudentRatingSystemApp.xml),
[StudentRatingSystemLib](https://github.com/Amischetoti/StudentRatingSystem/blob/main/StudentRatingSystemLib.xml),
[StudentRatingSystemDbContext](https://github.com/Amischetoti/StudentRatingSystem/blob/main/StudentRatingSystemDbContext.xml).

## Диаграммы сущностей
![Диаграмма переходов состояний](https://github.com/Amischetoti/StudentRatingSystem/blob/main/%D0%94%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%BF%D0%B5%D1%80%D0%B5%D1%85%D0%BE%D0%B4%D0%BE%D0%B2%20%D1%81%D0%BE%D1%81%D1%82%D0%BE%D1%8F%D0%BD%D0%B8%D0%B9.png)
![Диаграмма сущность-связь](https://github.com/Amischetoti/StudentRatingSystem/blob/main/%D0%94%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D1%81%D1%83%D1%89%D0%BD%D0%BE%D1%81%D1%82%D1%8C-%D1%81%D0%B2%D1%8F%D0%B7%D1%8C.jpg)
![Диаграмма потоков данных](https://github.com/Amischetoti/StudentRatingSystem/blob/main/%D0%94%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0%20%D0%BF%D0%BE%D1%82%D0%BE%D0%BA%D0%BE%D0%B2%20%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D1%85.png)
![Функциональная диаграмма](https://github.com/Amischetoti/StudentRatingSystem/blob/main/%D0%A4%D1%83%D0%BD%D0%BA%D1%86%D0%B8%D0%BE%D0%BD%D0%B0%D0%BB%D1%8C%D0%BD%D0%B0%D1%8F%20%D0%B4%D0%B8%D0%B0%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B0.png)

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
 - Преподаватели
 - Дисциплины
 - Студенты
 - Задания
 - Оценки

  На каждой вкладке вы обнаружите кнопки для добавления, изменения и удаления данных.
