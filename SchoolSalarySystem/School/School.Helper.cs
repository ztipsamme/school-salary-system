namespace SchoolSalarySystem;

public partial class School
{
    public class Helper
    {
        private record InputData(object? Input, object? Value);

        public Dictionary<string, object?>? CollectBaseEmployeeData()
        {
            var data = new Dictionary<string, InputData>
            {
                ["name"] = new(Ui.Input("Name"), null),
                ["birthDate"] =
                    new(Ui.Input<DateOnly>("Birthday (yy-mm-dd)"), null),
                ["startDate"] =
                    new(Ui.Input<DateOnly>("StartDate (yy-mm-dd)"), null),
                ["salary"] = new(Ui.Input<decimal>("Salary"), null)
            };

            foreach (var key in data.Keys.ToList())
            {
                if (Ui.CancelRequested) return null;
                data[key] = data[key] with { Value = data[key].Input };
            }

            return data.ToDictionary(k => k.Key, v => v.Value.Value);
        }

        public Teacher? CreateTeacher(string name, DateOnly birthDate,
            DateOnly startDate, decimal salary)
        {
            string? subject = Ui.Input("Subject");
            if (Ui.CancelRequested || subject == null) return null;

            int classesPerWeek = Ui.Input<int>("Classes per week");
            if (Ui.CancelRequested) return null;

            return new Teacher(name, birthDate, startDate, salary, subject,
                classesPerWeek);
        }

        public Administrator? CreateAdministrator(string name,
            DateOnly birthDate,
            DateOnly startDate, decimal salary)
        {
            string? department = Ui.Input("Department");
            if (Ui.CancelRequested || department == null) return null;

            return new Administrator(name, birthDate, startDate, salary,
                department);
        }
    }
}