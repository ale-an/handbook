using System.Diagnostics;
using Abstractions;
using Models;
using Storage.Abstractions;
using Storage.Models;
using Employee = Models.Employee;

namespace Logic;

public class EmployeeService : IEmployeeService
{
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Employee[] GetAll()
    {
        return _employeeRepository.GetAll()
            .Select(x => new Employee(
                new Fullname(x.Fullname),
                new BirthDate(x.BirthDate),
                x.Gender))
            .ToArray();
    }

    public Employee[] GetFiltered()
    {
        var stopwatch = Stopwatch.StartNew();
        var result = _employeeRepository.GetAll()
            .Where(x => x.Gender == Gender.Male)
            .Where(x => x.Fullname.StartsWith("F"))
            .Select(x => new Employee(
                new Fullname(x.Fullname),
                new BirthDate(x.BirthDate),
                x.Gender))
            .ToArray();
        stopwatch.Stop();
        Console.WriteLine($"Получил за {stopwatch.Elapsed.Seconds} секунд.");
        // 708 миллисекунд

        return result;
    }

    public void AddEmployee(Employee employee)
    {
        _employeeRepository.AddEmployee(new Storage.Models.Employee
        {
            Fullname = employee.Fullname.Value,
            BirthDate = employee.BirthDate.Value,
            Gender = employee.Gender
        });
    }

    public void CreateTables()
    {
        _employeeRepository.CreateTables();
    }

    public void FillTable()
    {
        _employeeRepository.FillTable();
    }

    public void FillTableOptimized()
    {
        _employeeRepository.FillTableOptimized();
    }

    private readonly IEmployeeRepository _employeeRepository;
}