namespace SchoolSalarySystem;

public class Teacher(
    string name,
    DateOnly birthday,
    DateOnly startDate,
    decimal baseSalary,
    string subject,
    int classesPerWeek)
    : Employee(name, birthday, startDate, baseSalary), IRoleDetails
{
    public string Subject { get; set; } = subject;
    public int ClassesPerWeek { get; set; } = classesPerWeek;

    public override decimal GetSalaryWithBonus()
    {
        decimal bonus = 200;
        bonus *= ClassesPerWeek;
        
        return SalaryCalculator.AddBonus(BaseSalary, bonus);
    }

    public void RoleDetails()
    {
        Console.WriteLine($"Subject: {Subject}\n" +
                          $"Classes per week: {ClassesPerWeek}");
    }
}