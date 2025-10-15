namespace SchoolSalarySystem;

public abstract class Employee
{
    private string _name;
    decimal _baseSalary;

    public string Name
    {
        get => _name;
        private init => _name = String.IsNullOrWhiteSpace(value)
            ? throw new ArgumentNullException("Name cannot be null or empty")
            : value;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateOnly Birthday { get; private set; }
    public DateOnly StartDate { get; private set; }

    public decimal BaseSalary
    {
        get => _baseSalary;
        private set => _baseSalary = value < 0 ? 0 : value;
    }

    public Employee(string name, DateOnly birthday, DateOnly startDate,
        decimal baseSalary)
    {
        Name = name;
        Birthday = birthday;
        StartDate = startDate;
        BaseSalary = baseSalary;
    }

    public void UpdateBaseSalary(decimal? newSalary = null)
    {
        decimal salary = newSalary ?? BaseSalary;
        int employmentLength = LengthOfEmployment();
        int senior = 5;

        if (employmentLength >= senior) BaseSalary = salary * (decimal)1.05;
        else BaseSalary = salary;
    }

    public virtual decimal GetSalaryWithBonus() => BaseSalary;

    private int LengthOfEmployment()
    {
        DateTime today = DateTime.Today;

        int years = today.Year - StartDate.Year;

        if (today.DayOfYear < StartDate.DayOfYear) years--;

        return years;
    }

    public override string ToString()
    {
        var fields = new Dictionary<string, object>
        {
            { "Id", Id },
            { "Name", Name },
            { "Birthday", Birthday },
            { "Start date", StartDate },
            { "Base salary", $"{BaseSalary:C}" }
        };

        return string.Join("\n",
            fields.Select(kv => $"{kv.Key}: {kv.Value}"));
    }
}