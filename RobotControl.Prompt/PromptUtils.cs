using System.Text;

namespace RobotControl.Prompt;

internal class PromptUtils
{
    internal static string ReadLineWithCancel()
    {
        string result = null;

        StringBuilder builder = new StringBuilder();
        var leftCursorPosition = Console.CursorLeft;

        ConsoleKeyInfo info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
        {
            if (info.Key == ConsoleKey.Backspace)
            {
                if (builder.Length > 0)
                {
                    builder.Remove(builder.Length - 1, 1);
                    Console.SetCursorPosition(leftCursorPosition, Console.CursorTop);
                    Console.Write(new string(' ', Console.WindowWidth - leftCursorPosition));
                    Console.SetCursorPosition(leftCursorPosition, Console.CursorTop);
                    Console.Write(builder.ToString());
                }
            }
            else
            {
                Console.Write(info.KeyChar);
                builder.Append(info.KeyChar);
            }

            info = Console.ReadKey(true);
        }

        if (info.Key == ConsoleKey.Enter)
            result = builder.ToString();

        if (info.Key == ConsoleKey.Escape)
            result = "[ESC]";

        return result;
    }
}
