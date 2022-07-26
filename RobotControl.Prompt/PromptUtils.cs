using System.Text;

namespace RobotControl.Prompt;

internal class PromptUtils
{
    internal static string ReadLineWithCancel()
    {
        string result = null;

        StringBuilder builder = new StringBuilder();

        ConsoleKeyInfo info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
        {
            Console.Write(info.KeyChar);
            builder.Append(info.KeyChar);
            info = Console.ReadKey(true);
        }

        if (info.Key == ConsoleKey.Enter)
            result = builder.ToString();

        if (info.Key == ConsoleKey.Escape)
            result = "[ESC]";

        return result;
    }
}
