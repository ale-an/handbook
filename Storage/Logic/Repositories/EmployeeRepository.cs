using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Storage.Abstractions;
using Storage.Models;

namespace Storage.Logic.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public EmployeeRepository(ApplicationContext context, IEmployeeGenerator employeeGenerator)
    {
        _context = context;
        _employeeGenerator = employeeGenerator;
    }

    public IQueryable<Employee> GetAll()
    {
        return _context.Employees!.AsQueryable();
    }

    public void AddEmployee(Employee employee)
    {
        _context.Employees!.Add(new Employee
        {
            Fullname = employee.Fullname,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender
        });

        _context.SaveChanges();
    }

    public void CreateTables()
    {
        var databaseCreator =
            (RelationalDatabaseCreator)_context.Database.GetService<IDatabaseCreator>();
        databaseCreator.CreateTables();
    }

    public void FillTable()
    {
        var stopwatch = Stopwatch.StartNew();

        for (var i = 0; i < 1000000; i++)
        {
            _context.Employees!.Add(_employeeGenerator.Generate());
        }

        _context.SaveChanges();

        stopwatch.Stop();

        Console.WriteLine($"Заполнил базу за {stopwatch.Elapsed.TotalSeconds} секунд.");
        // 47,0858472 секунд
    }

    public void FillTableOptimized()
    {
        var stopwatch = Stopwatch.StartNew();

        var query = new StringBuilder();

        query.Append("INSERT INTO public.\"Employees\" (\"Fullname\", \"BirthDate\", \"Gender\") values ");

        for (int i = 0; i < 1000000; i++)
        {
            query.Append(_employeeGenerator.GenerateInsertValue());
            query.Append(i == 999999 ? ';' : ',');
        }

        _context.Database.ExecuteSqlRaw(query.ToString());

        stopwatch.Stop();

        Console.WriteLine($"Заполнил базу за {stopwatch.Elapsed.TotalSeconds} секунд.");
        // 6,116878 секунд
        // Время уменьшилось, потому что запрос состравлен с помощью базы данных, а не через Entity Framework
    }

    private readonly ApplicationContext _context;
    private readonly IEmployeeGenerator _employeeGenerator;
}