using Storage.Models;

namespace Storage.Abstractions;

public interface IEmployeeRepository
{
    IQueryable<Employee> GetAll();
    void AddEmployee(Employee employee);
    void CreateTables();
    void FillTable();
    void FillTableOptimized();
}