using System.Text.Json;

namespace SchoolSalarySystem;

public partial class School
{
    private const string EmployeeFile = "employees.json";

    private void SaveEmployees()
    {
        var teachers = _employees.OfType<Teacher>().ToList();
        var administrators = _employees.OfType<Administrator>().ToList();
        var courseAdmins = _employees.OfType<CourseAdministrator>().ToList();

        var allData = new EmployeesData
        {
            Teachers = teachers,
            Administrators = administrators,
            CourseAdministrators = courseAdmins
        };
    
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(allData, options);

        File.WriteAllText(EmployeeFile, json);
    }


    private void LoadEmployees()
    {
        if (!File.Exists(EmployeeFile)) return;

        var json = File.ReadAllText(EmployeeFile);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        EmployeesData? allData;
        try
        {
            allData = JsonSerializer.Deserialize<EmployeesData>(json, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading employees: {ex.Message}");
            return;
        }

        if (allData == null) return;

        _employees.Clear();
        if (allData.Teachers != null) _employees.AddRange(allData.Teachers);
        if (allData.Administrators != null) _employees.AddRange(allData.Administrators);
        if (allData.CourseAdministrators != null) _employees.AddRange(allData.CourseAdministrators);
    }
    
    private class EmployeesData
    {
        public List<Teacher>? Teachers { get; set; }
        public List<Administrator>? Administrators { get; set; }
        public List<CourseAdministrator>? CourseAdministrators { get; set; }
    }
}