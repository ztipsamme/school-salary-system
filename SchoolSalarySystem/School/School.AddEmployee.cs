namespace SchoolSalarySystem;

public partial class School
{
    public void AddEmployee()
    {
        Helper helper = new();
        Ui.Header("Add a new employee");

        string[] role = { "Teacher", "Administrator", "CourseAdministrator" };
        for (int i = 0; i < role.Length; i++)
            Console.WriteLine($"{i + 1}. {role[i]}");

        int chosenRole = Ui.Input<int>("Role");
        if (Ui.CancelRequested) return;
        
        var baseData = helper.CollectBaseEmployeeData();
        if (baseData == null) return;

        string name = (string)baseData["name"]!;
        DateOnly birthDate = (DateOnly)baseData["birthDate"]!;
        DateOnly startDate = (DateOnly)baseData["startDate"]!;
        decimal salary = (decimal)baseData["salary"]!;

        
        Employee? newEmployee = chosenRole switch
        {
            1 => helper.CreateTeacher(name, birthDate, startDate, salary),
            2 => helper.CreateAdministrator(name, birthDate, startDate, salary),
            3 => new CourseAdministrator(name, birthDate, startDate, salary),
            _ => null
        };
        
        if (newEmployee == null || Ui.CancelRequested) return;

        _employees.Add(newEmployee);
        Console.WriteLine($"✅ Added {newEmployee.Name}");
    }
}