namespace SchoolSalarySystem;

public partial class School
{
    public void UpdateSalary()
    {
        Ui.Header("Update Salary");

        ListEmployees();

        Employee? employee = FindEmployeeById();

        if (employee == null) Console.WriteLine("Employee not found");
        else
        {
            decimal newSalary = Ui.Input<decimal>("New Base Salary");
            if (Ui.CancelRequested) return;

            employee.UpdateBaseSalary(newSalary);

            Console.WriteLine($"New Base Salary: {employee.BaseSalary:C}");
        }
    }

    public void PaySalaries()
    {
        Ui.Header("Pay Salaries");

        decimal taxRate = Ui.Input<decimal>("Tax");

        foreach (Employee employee in _employees)
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine($"{employee.Name} | id: {employee.Id}");
            Console.WriteLine(
                $"Gross: {SalaryCalculator.CalculateGross(employee):C}");
            Console.WriteLine(
                $"Net (after tax): {SalaryCalculator.CalculateNet(employee, taxRate):C}");
        }

        decimal totalGross = SalaryCalculator.CalculateTotalGross(_employees);
        decimal totalNet =
            SalaryCalculator.CalculateTotalNet(_employees, taxRate);

        Console.WriteLine("\n" + new string('—', Console.WindowWidth));
        Console.WriteLine($"Total Gross Salary: {totalGross:C}");
        Console.WriteLine($"Total Net Salary (after tax): {totalNet:C}");
    }
}