using Storage.Models;

namespace Models;

public class Employee
{
    public Fullname Fullname { get; set; }
    public BirthDate BirthDate { get; set; }
    public Gender Gender { get; set; }

    public Employee(Fullname fullname, BirthDate birthDate, Gender gender)
    {
        Fullname = fullname;
        BirthDate = birthDate;
        Gender = gender;
    }

    public override string ToString()
    {
        return
            $"ФИО сотрудника: {Fullname.Value}, Дата рождения: {BirthDate.Value}, Пол: {Gender}, Полных лет: {CalculateAge()}";
    }

    private int CalculateAge()
    {
        return DateTime.Now.Year - BirthDate.Value.Year;
    }
}