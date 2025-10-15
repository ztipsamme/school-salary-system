namespace SchoolSalarySystem;

public static class SalaryCalculator
{
    public static decimal ApplyTax(decimal salary, decimal taxRate)
    {
        return salary * (1 - taxRate);
    }

    public static decimal AddBonus(decimal salary, decimal bonus)
    {
        return salary + bonus;
    }

    public static decimal CalculateGross(Employee employee) =>
        employee.GetSalaryWithBonus();

    public static decimal CalculateNet(Employee employee, decimal taxRate) =>
        ApplyTax(employee.GetSalaryWithBonus(), taxRate);

    public static decimal
        CalculateTotalGross(IEnumerable<Employee> employees) =>
        employees.Sum(e => e.GetSalaryWithBonus());


    public static decimal CalculateTotalNet(IEnumerable<Employee> employees,
        decimal taxRate) =>
        employees.Sum(e => ApplyTax(e.GetSalaryWithBonus(), taxRate));
}