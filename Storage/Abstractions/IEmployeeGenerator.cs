using Storage.Models;

namespace Storage.Abstractions;

public interface IEmployeeGenerator
{
    Employee Generate();
    string GenerateInsertValue();
}