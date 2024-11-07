using System.Text;
using Storage.Abstractions;
using Storage.Models;

namespace Storage.Logic;

public class EmployeeGenerator : IEmployeeGenerator
{
    public Employee Generate()
    {
        var name = $"{GenerateName()} {GenerateName()} {GenerateName()}";
        var birthday = GenerateBirthDate();
        var gender = (Gender)(new Random().Next(0, 2));

        return new Employee
        {
            Fullname = name,
            BirthDate = birthday,
            Gender = gender
        };
    }

    public string GenerateInsertValue()
    {
        var employee = Generate();

        return $"(\'{employee.Fullname}\', \'{employee.BirthDate: yyyy-MM-dd}\', {(int)employee.Gender})";
    }

    private string GenerateName()
    {
        var alphabet = "abcdefghijklmnopqrstuvwxyz";

        var random = new Random();

        var length = random.Next(3, 10);

        var name = new StringBuilder();

        for (var j = 0; j < length; j++)
        {
            var symbolIndex = random.Next(0, alphabet.Length - 1);

            if (j == 0)
                name.Append(alphabet[symbolIndex].ToString().ToUpper());
            else
                name.Append(alphabet[symbolIndex]);
        }

        return name.ToString();
    }

    private DateTime GenerateBirthDate()
    {
        var random = new Random();
        var day = random.Next(1, 29);
        var month = random.Next(1, 13);
        var year = random.Next(1950, 2010);

        return new DateTime(year, month, day);
    }
}