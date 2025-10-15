namespace SchoolSalarySystem;

public static partial class Ui
{
    public static bool CancelRequested { get; private set; }

    public static string? Input(
        string prompt,
        string? errMessage = null,
        Func<string, bool>? validator = null)
    {
        if (prompt.Contains("(y/n)"))
            return Input<string>(prompt,
                "Must be 'y' or 'n'", input =>
                    input.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("n", StringComparison.OrdinalIgnoreCase));

        return Input<string>(prompt, errMessage, validator);
    }

    public static T? Input<T>(
        string prompt,
        string? errMessage = null,
        Func<string, bool>? validator = null,
        Func<string, T>? convert = null)
    {
        CancelRequested = false;
        
        var typeDefaults =
            new Dictionary<Type, (Func<string, bool> Validator, Func<string, T>
                Convert, string ErrMessage)>
            {
                {
                    typeof(string),
                    (s => !string.IsNullOrWhiteSpace(s), s => (T)(object)s,
                        "Can't be empty")
                },
                {
                    typeof(int),
                    (s => int.TryParse(s, out _), s => (T)(object)int.Parse(s),
                        "Must be a number")
                },
                {
                    typeof(decimal),
                    (s => decimal.TryParse(s, out _),
                        s => (T)(object)decimal.Parse(s),
                        "Must be a decimal number")
                },
                {
                    typeof(DateOnly),
                    (s => DateOnly.TryParse(s, out _),
                        s => (T)(object)DateOnly.Parse(s),
                        "Must be a valid date")
                },
                {
                    typeof(Guid),
                    (s => Guid.TryParse(s, out _),
                        s => (T)(object)Guid.Parse(s), "Must be a valid id")
                }
            };

        if (typeDefaults.TryGetValue(typeof(T), out var defaults))
        {
            validator ??= defaults.Validator;
            convert ??= defaults.Convert;
            errMessage ??= defaults.ErrMessage;
        }
        else
        {
            validator ??= s => !string.IsNullOrWhiteSpace(s);
            convert ??= s =>
                throw new NotSupportedException(
                    $"Conversion for {typeof(T)} is not supported");
            errMessage ??= "Invalid input";
        }

        while (true)
        {
            Console.Write($"{prompt}: ");
            int startRow = GetCursorPositionTop();

            string input = Console.ReadLine() ?? "";

            if (input.Equals("back", StringComparison.OrdinalIgnoreCase))
            {
                CancelRequested = true;
                return default;
            }

            try
            {
                if (validator(input))
                {
                    return convert(input);
                }
            }
            catch
            {
                // ignore parse exceptions
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errMessage);
            Thread.Sleep(1200);
            Console.ResetColor();

            ClearMultipleLines(startRow);
        }
    }
}