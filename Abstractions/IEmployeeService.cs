using Models;

namespace Abstractions;

public interface IEmployeeService
{
    Employee[] GetAll();
    Employee[] GetFiltered();
    void AddEmployee(Employee employee);
    void CreateTables();
    void FillTable();
    void FillTableOptimized();
}