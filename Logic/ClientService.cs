using System.Globalization;
using Abstractions;
using Models;
using Storage.Models;
using Employee = Models.Employee;

namespace Logic;

public sealed class ClientService
{
    public ClientService(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public void Execute()
    {
        Console.WriteLine("Здравствуйте!");
        Console.WriteLine("Нажмите 1 для создания таблицы с полями справочника сотрудников.");
        Console.WriteLine("Нажмите 2 для cоздания записи справочника сотрудников.");
        Console.WriteLine(
            "Нажмите 3 для вывода всех строк справочника сотрудников с уникальным значением ФИО и даты, " +
            "отсортированных по ФИО.");
        Console.WriteLine("Нажмите 4 для автоматического заполнения 1000000 строк справочника сотрудников.");
        Console.WriteLine("Нажмите 5 для вывода выборки из таблицы по критериям: " +
                          "пол мужской, " +
                          "фамилия начинается с F.");
        Console.WriteLine("Нажмите 6 для заполнения 1000000 строк справочника оптимизированным способом.");

        while (true)
        {
            var input = Console.ReadLine();

            var key = input[0];

            var parameters = input[1..].TrimStart();

            switch (key)
            {
                case '1':
                    _employeeService.CreateTables();
                    break;
                case '2':
                    var fullname = parameters
                        .Remove(parameters.LastIndexOf('\"'))
                        .Replace("\"", "");

                    var birthdateAndGender = parameters
                        .Substring(parameters.LastIndexOf('\"'))
                        .Replace("\"", "")
                        .Trim()
                        .Split(" ");

                    var employee = new Employee(
                        new Fullname(fullname),
                        new BirthDate(
                            DateTime.ParseExact(
                                birthdateAndGender[0],
                                "yyyy-MM-dd",
                                CultureInfo.InvariantCulture)),
                        Enum.Parse<Gender>(birthdateAndGender[1]));

                    _employeeService.AddEmployee(employee);
                    break;
                case '3':
                    var employees = _employeeService.GetAll().OrderBy(x => x.Fullname.Value);
                    foreach (var emp in employees)
                    {
                        Console.WriteLine(emp);
                    }

                    break;
                case '4':
                    _employeeService.FillTable();
                    break;
                case '5':
                    var employeesFiltered = _employeeService.GetFiltered();
                    foreach (var filtered in employeesFiltered)
                    {
                        Console.WriteLine(filtered);
                    }

                    break;

                case '6':
                    _employeeService.FillTableOptimized();
                    break;
                default:
                    Console.WriteLine("Ошибка! Введите 1, 2, 3, 4, 5 или 6.");
                    break;
            }
        }
    }

    private readonly IEmployeeService _employeeService;
}