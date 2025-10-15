namespace SchoolSalarySystem;

public static partial class Ui
{
    public static void Header(string title)
    {
        string line = new string('—', Console.WindowWidth - title.Length - 2);
        Console.WriteLine(line.Insert(3, $" {title} "));
    }

    public static int GetCursorPositionTop() => Console.GetCursorPosition().Top;

    public static void Clear(int startRow = 0)
    {
        for (int i = startRow; i <= Console.WindowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowHeight));
        }

        Console.SetCursorPosition(0, startRow);
    }

    public static void ClearSingleLine(int? startRow = null)
    {
        int row = startRow ?? GetCursorPositionTop();
        Console.SetCursorPosition(0, row);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, row);
    }

    public static void ClearMultipleLines(int startRow, int? endRow = null)
    {
        int rows = endRow ?? Console.WindowHeight;

        for (int i = startRow; i <= rows; i++) ClearSingleLine(i);

        Console.SetCursorPosition(0, startRow);
    }
}