namespace SchoolSalarySystem;

public class Administrator(
    string name,
    DateOnly birthday,
    DateOnly startDate,
    decimal baseSalary,
    string department)
    : Employee(name, birthday, startDate, baseSalary), IRoleDetails
{
    public string Department {get; set;} = department;

    public override decimal GetSalaryWithBonus()
    {
        decimal bonus = 1000;
        return SalaryCalculator.AddBonus(BaseSalary, bonus);
    }

    public void RoleDetails()
    {
        Console.WriteLine($"Department: {Department}");
    }
}