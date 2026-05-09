using Microsoft.EntityFrameworkCore;
using OnlineCourses.Web.Data;
using OnlineCourses.Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=onlinecourses.db"));

builder.Services.AddSession();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();

    if (!db.Courses.Any())
    {
        var course1 = new Course
        {
            Title = "Основи програмування",
            Description = "Курс для початківців, який допомагає зрозуміти алгоритми, змінні, умови та цикли.",
            Duration = "6 тижнів",
            ImageUrl = "/images/programming.jpg",

            TeacherName = "Олена Ковальчук",
            TeacherPosition = "Викладач програмування",
            TeacherDescription = "Пояснює основи програмування простими словами та допомагає студентам розібратися з алгоритмами, змінними й умовами.",
            TeacherPhotoUrl = "/images/teacher-programming.jpg",
            TeacherExperienceYears = 5,
            TeacherReviewsCount = 128,
            TeacherRating = 4.8,

            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Title = "Лекція 1. Алгоритми та програми",
                    TheoryText = "У цій лекції розглядається поняття алгоритму. Алгоритм — це послідовність дій, яку потрібно виконати для розв’язання певної задачі.",
                    TaskText = "Скласти алгоритм приготування чаю у вигляді послідовності команд.",
                    VideoUrl = "https://www.youtube.com/embed/gfkTfcpWqAY"
                },
                new Lesson
                {
                    Title = "Лекція 2. Змінні та типи даних",
                    TheoryText = "У цій лекції пояснюється, що таке змінна, для чого вона потрібна та які бувають типи даних.",
                    TaskText = "Створити приклад програми зі змінними для імені користувача, віку та результату навчання.",
                    VideoUrl = "https://www.youtube.com/embed/yquQ9mLtkN8"
                },
                new Lesson
                {
                    Title = "Лекція 3. Умовні оператори",
                    TheoryText = "Умовні оператори дозволяють програмі виконувати різні дії залежно від умови.",
                    TaskText = "Написати приклад програми, яка перевіряє, чи користувач повнолітній.",
                    VideoUrl = "https://www.youtube.com/embed/eVWTeoygvFA"
                }
            }
        };

        var course2 = new Course
        {
            Title = "Веб-розробка",
            Description = "Курс про створення веб-сторінок за допомогою HTML, CSS та ASP.NET Core MVC.",
            Duration = "8 тижнів",
            ImageUrl = "/images/web.jpg",

            TeacherName = "Андрій Мельник",
            TeacherPosition = "Викладач веб-розробки",
            TeacherDescription = "Навчає створенню сучасних сайтів, пояснює HTML, CSS та ASP.NET Core MVC на практичних прикладах.",
            TeacherPhotoUrl = "/images/teacher-web.jpg",
            TeacherExperienceYears = 6,
            TeacherReviewsCount = 156,
            TeacherRating = 4.9,

            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Title = "Лекція 1. Основи HTML",
                    TheoryText = "HTML використовується для створення структури веб-сторінки.",
                    TaskText = "Створити просту HTML-сторінку з заголовком, текстом і кнопкою.",
                    VideoUrl = "https://www.youtube.com/embed/qz0aGYrrlhU"
                },
                new Lesson
                {
                    Title = "Лекція 2. Основи CSS",
                    TheoryText = "CSS використовується для оформлення веб-сторінки.",
                    TaskText = "Додати стилі для заголовка, кнопки та картки курсу.",
                    VideoUrl = "https://www.youtube.com/embed/1PnVor36_40"
                },
                new Lesson
                {
                    Title = "Лекція 3. ASP.NET Core MVC",
                    TheoryText = "MVC — це підхід до побудови веб-додатків.",
                    TaskText = "Створити контролер і представлення для сторінки каталогу курсів.",
                    VideoUrl = "https://www.youtube.com/embed/hZ1DASYd9rk"
                }
            }
        };

        var course3 = new Course
        {
            Title = "Бази даних та SQL",
            Description = "Курс для вивчення таблиць, зв’язків між ними, SQL-запитів та Entity Framework Core.",
            Duration = "5 тижнів",
            ImageUrl = "/images/database.jpg",

            TeacherName = "Ірина Шевченко",
            TeacherPosition = "Викладач баз даних",
            TeacherDescription = "Допомагає студентам зрозуміти SQL, таблиці, зв’язки між даними та роботу з Entity Framework Core.",
            TeacherPhotoUrl = "/images/teacher-database.jpg",
            TeacherExperienceYears = 7,
            TeacherReviewsCount = 103,
            TeacherRating = 4.7,

            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Title = "Лекція 1. Що таке база даних",
                    TheoryText = "База даних використовується для зберігання інформації у структурованому вигляді.",
                    TaskText = "Описати, які таблиці потрібні для онлайн-платформи курсів.",
                    VideoUrl = "https://www.youtube.com/embed/5sG9kmXYsKU"
                },
                new Lesson
                {
                    Title = "Лекція 2. SQL-запити",
                    TheoryText = "SQL — це мова запитів для роботи з базами даних.",
                    TaskText = "Написати SQL-запит для вибору всіх курсів з таблиці Courses.",
                    VideoUrl = "https://www.youtube.com/embed/h0nxCDiD-zg"
                },
                new Lesson
                {
                    Title = "Лекція 3. Зв’язки між таблицями",
                    TheoryText = "Зв’язки між таблицями дозволяють поєднувати дані.",
                    TaskText = "Пояснити зв’язок між таблицями Users, Courses та Enrollments.",
                    VideoUrl = "https://www.youtube.com/embed/5OdVJbNCSso"
                }
            }
        };

        var course4 = new Course
        {
            Title = "UI/UX дизайн",
            Description = "Курс про створення зручного інтерфейсу, структуру сторінок, макети та користувацький досвід.",
            Duration = "4 тижні",
            ImageUrl = "/images/design.jpg",

            TeacherName = "Марія Бондар",
            TeacherPosition = "UI/UX дизайнер",
            TeacherDescription = "Навчає створювати зручні інтерфейси, продумувати структуру сторінок і працювати з макетами.",
            TeacherPhotoUrl = "/images/teacher-design.jpg",
            TeacherExperienceYears = 4,
            TeacherReviewsCount = 89,
            TeacherRating = 4.8,

            Lessons = new List<Lesson>
            {
                new Lesson
                {
                    Title = "Лекція 1. Що таке UI та UX",
                    TheoryText = "UI відповідає за зовнішній вигляд інтерфейсу, а UX — за зручність користування.",
                    TaskText = "Проаналізувати головну сторінку навчальної платформи та описати її зручність.",
                    VideoUrl = "https://www.youtube.com/embed/ODpB9-MCa5s"
                },
                new Lesson
                {
                    Title = "Лекція 2. Макет сторінки",
                    TheoryText = "Макет сторінки допомагає продумати розташування блоків, меню, кнопок і карток.",
                    TaskText = "Створити структуру сторінки каталогу курсів.",
                    VideoUrl = "https://www.youtube.com/embed/c9Wg6Cb_YlU"
                },
                new Lesson
                {
                    Title = "Лекція 3. Дизайн у Figma",
                    TheoryText = "Figma використовується для створення макетів сайтів і додатків.",
                    TaskText = "Створити простий макет картки курсу у Figma або описати його структуру.",
                    VideoUrl = "https://www.youtube.com/embed/deJDQ1SZt4A"
                }
            }
        };

        db.Courses.AddRange(course1, course2, course3, course4);
        db.SaveChanges();
    }

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { Login = "admin", Password = "admin123", Role = "Admin" },
            new User { Login = "teacher", Password = "teacher123", Role = "Teacher" },
            new User { Login = "student", Password = "student123", Role = "Student" }
        );

        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();