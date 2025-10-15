namespace SchoolSalarySystem;

public partial class School
{
    private List<Employee> _employees = new();

    public School()
    {
        LoadEmployees();
    }

    public void ListEmployees()
    {
        Ui.Header("List employees");

        List<(string option, List<Employee> employees)> sortingOptions =
        [
            ("By Name", _employees.OrderBy(x => x.Name).ToList()),
            ("By Salary", _employees.OrderBy(x => x.BaseSalary).ToList())
        ];

        int idx = 1;
        foreach (var o in sortingOptions)
        {
            Console.WriteLine($"{idx}. {o.option}");
            idx++;
        }

        string? choice = Ui.Input<string>("Sort by",
            $"Must be empty or between 1 and {sortingOptions.Count}",
            input =>
                input == "" || int.TryParse(input, out int opt) && opt >= 1 &&
                opt <= sortingOptions.Capacity);

        if (Ui.CancelRequested) return;

        if (choice != null)
        {
            Console.WriteLine();
            Ui.Header($"Employees {(choice != "" ? $"{sortingOptions[int.Parse(choice) - 1].option} " : "")}");


            (choice == ""
                    ? _employees
                    : sortingOptions[int.Parse(choice) - 1].employees)
                .ForEach(employee =>
                    Console.WriteLine(employee.ToString() + "\n"));
        }
    }

    private Employee? FindEmployeeById()
    {
        Guid id = Ui.Input<Guid>("Enter Id");
        return _employees.Find(x => x.Id == id);
    }

    public void FindEmployee()
    {
        Ui.Header("Find employee");

        Console.WriteLine(new string('—', Console.WindowWidth));

        Employee? employee = FindEmployeeById();

        if (Ui.CancelRequested) return;

        Console.WriteLine(employee == null
            ? "Employee not found"
            : employee?.ToString());
    }

    public void RemoveEmployee()
    {
        Ui.Header("Remove employee");

        Guid? id = Ui.Input<Guid>("Enter Id");
        if (Ui.CancelRequested) return;
        Employee? employee = _employees.Find(x => x.Id == id);

        if (employee == null) Console.WriteLine("Employee not found");
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(
                "Warning: Once the employee has been removed you can't undo the action.");
            Console.ResetColor();
            string? choice =
                Ui.Input(
                    $"Would you like to remove {employee.Name} {employee.Id} (y/n)");

            if (choice == "y")
            {
                Console.WriteLine($"{employee.Name} has been removed");
                _employees.Remove(employee);
                SaveEmployees();
            }
            else
            {
                Console.WriteLine("Process cancelled");
            }
        }
    }
}