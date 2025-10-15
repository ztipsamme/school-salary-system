using SchoolSalarySystem;

School school = new();

List<(string option, Action action)> menu =
[
    ("Add Employee", school.AddEmployee),
    ("List Employees", school.ListEmployees),
    ("Pay Salary", school.PaySalaries),
    ("Find Employee", school.FindEmployee),
    ("Update Salary", school.UpdateSalary),
    ("Remove Employee", school.RemoveEmployee),
    ("Quit", () =>
    {
        Console.Clear();
        Console.WriteLine("Goodbye!");
        Thread.Sleep(800);
        Environment.Exit(0);
    })
];

while (true)
{
    Console.Clear();
    Console.WriteLine("=== School Salary System ===");
    Console.WriteLine(new string('—', Console.WindowWidth));
    Console.WriteLine($"{"[enter]",-8} = confirm\n" +
                      $"{"\"back\"",-8} = go back");
    Console.WriteLine(new string('—', Console.WindowWidth));
    int startRow = Ui.GetCursorPositionTop();

    for (int i = 0; i < menu.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {menu[i].option}");
    }

    string errMessage = $"Must be between 1 and {menu.Count}";
    int input = Ui.Input<int>("Enter",
        errMessage,
        input => int.TryParse(input, out int opt) && opt > 0 &&
                 opt <= menu.Count);
            
    if (input == 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errMessage);
        Thread.Sleep(1200);
        Console.ResetColor();
    }
    else
    {
        Ui.ClearMultipleLines(startRow);
        menu[input - 1].action();
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}