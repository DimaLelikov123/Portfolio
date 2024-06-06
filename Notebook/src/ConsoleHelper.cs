using System;

static class ConsoleHelper
{
    public static string ReadString(string text, string error_text, Predicate<string> predicate)
    {
        Console.WriteLine(text);
        string res = Console.ReadLine()!;
        while(!predicate(res))
        {
            PrintError(error_text);
            Console.WriteLine(text);
            res = Console.ReadLine()!;
        }

        return res;
    }

    public static string ReadString(string text)
    {
        Console.WriteLine(text);
        return Console.ReadLine()!;
    }

    public static int ReadInt(string text, string error_text)
    {
        int res = 0;
        ReadString(text, error_text, (s) => int.TryParse(s, out res));
        return res;
    }

    public static TEnum ReadEnum<TEnum>(string message) where TEnum : struct, System.Enum
    {
        TEnum res = default;

        var variants = string.Join(", ", Enum.GetNames<TEnum>());

        PrintInfo(variants);

        ReadString(message, "Value must be one of: " + variants, (s) => Enum.TryParse<TEnum>(s, out res));

        return res;
    }

    public static DateOnly ReadDateOnly(string text, string error_text, string format)
    {
        DateOnly res = new DateOnly();
        ReadString(text, error_text, (s) => DateOnly.TryParseExact(s, format, out res));
        return res;
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}