namespace SchoolSalarySystem;

public class CourseAdministrator(
    string name,
    DateOnly birthday,
    DateOnly startDate,
    decimal baseSalary)
    : Employee(name, birthday,
        startDate, baseSalary), IRoleDetails
{
    public int ActiveCourses { get; set; }

    public override decimal GetSalaryWithBonus()
    {
        decimal bonus = 300;
        bonus *= ActiveCourses;

        return SalaryCalculator.AddBonus(BaseSalary, bonus);
    }

    public void RoleDetails()
    {
        Console.WriteLine($"Active courses: {ActiveCourses}");
    }
}