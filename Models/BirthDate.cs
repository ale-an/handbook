namespace Models;

public record BirthDate
{
    public DateTime Value { get; }

    public BirthDate(DateTime value)
    {
        Value = value;
    }
}