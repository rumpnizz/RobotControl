// See https://aka.ms/new-console-template for more information

namespace RobotControl.Prompt;

public class Optionizer<TEnum> : IOptionizer<TEnum>
    where TEnum : Enum
{
    protected string Headline { get; private set; }
    protected Dictionary<TEnum, string> Options { get; private set; }

    protected int SelectedIndex { get; private set; }

    public Optionizer(string headline, params KeyValuePair<TEnum, string>[] options)
    {
        Headline = headline;
        Options = options.ToDictionary(x => x.Key, x => x.Value);
    }

    public TEnum PrintOptions()
    {
        Console.WriteLine(Headline);

        var topCursor = Console.CursorTop;
        var values = Options.Values.ToList();

        while (true)
        {
            for (int i = 0; i < values.Count; i++)
                PrintMarked(values[i], SelectedIndex == i);

            //var key = PromptUtils.ReadLineWithCancel();
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.UpArrow:
                    MoveSelected(key);
                    break;
                case ConsoleKey.Enter:
                    return Select();
            }

            for (int i = topCursor; i <= Console.CursorTop; i++)
            {
                ClearConsoleLine(i);
                Console.SetCursorPosition(0, topCursor);
            }
        }

        return default(TEnum);
    }

    private static void PrintMarked(string input, bool marked)
    {
        if (!marked) { 
            Console.WriteLine("  " + input);
            return;
        }

        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("> " + input);
        Console.ForegroundColor = previousColor;
    }

    private static void ClearConsoleLine(int cursorTop)
    {
        Console.SetCursorPosition(0, cursorTop);
        Console.Write(new string(' ', Console.WindowWidth));
    }

    public static IOptionizer<TEnum> Create(string headline, params KeyValuePair<TEnum, string>[] options)
    {
        return new Optionizer<TEnum>(headline, options);
    }

    private void MoveSelected(ConsoleKey key)
    {
        SelectedIndex = Math.Abs((SelectedIndex + (key == ConsoleKey.DownArrow ? 1 : -1)) % Options.Values.Count);
    }

    private TEnum Select()
    {
        return Options.Keys.ToList()[SelectedIndex];
    }

}


