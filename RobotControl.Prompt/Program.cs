using RobotControl.Core;
using RobotControl.Core.Enums;
using RobotControl.Core.Interfaces;
using System.Text.RegularExpressions;

namespace RobotControl.Prompt;
class Program
{
    private static IOptionizer<RoomType> _roomOptionizer;
    private static IOptionizer<Language> _languageOptionizer;

    static Program()
    {
        _roomOptionizer = Optionizer<RoomType>.Create(
            "Configure room",
            KeyValuePair.Create(RoomType.Rectangular, "Rectangular room"),
            KeyValuePair.Create(RoomType.Circular, "Circular room")
        );
        _languageOptionizer = Optionizer<Language>.Create(
            "\n\nChoose robot input language",
            KeyValuePair.Create(Language.English, "English"),
            KeyValuePair.Create(Language.Swedish, "Swedish")
        );

    }

    static void Main(string[] args)
    {
        Console.WriteLine("/* Welcome to the Robot Controller */\n");

        while (true)
        {
            var roomSelection = _roomOptionizer.PrintOptions();

            var room = GetRoomBySizeInput(roomSelection);

            if (room == null)
            {
                Console.Clear();
                Console.WriteLine("Something went wrong. Restarting.");
                continue;
            }

            var languageSelection = _languageOptionizer.PrintOptions();

            var translator = new RobotTranslator(languageSelection);
            var robotController = new RobotController(room, translator);

            Console.WriteLine($"\nRobot initialized at start position: " +
                $"({room.StartPosition.X}, {room.StartPosition.Y}) facing {robotController.CurrentDirection}");


            string? commandInput;
            do
            {
                Console.Write("\nPress [ESC] to restart.\nEnter robot command: ");
                commandInput = PromptUtils.ReadLineWithCancel();

                var newPosition = robotController.Move(commandInput);
                Console.WriteLine($"\nNew robot position: {newPosition}");
            } while (commandInput != "[ESC]");

            if (commandInput == "[ESC]") 
            {
                Console.Clear();
                Console.WriteLine("Escape pressed, restarted.\n");
            }
        }
    }

    private static IRoom GetRoomBySizeInput(RoomType roomType)
    {
        if (roomType == RoomType.Rectangular)
        {
            return GetRectangularRoom();
        }
        else if (roomType == RoomType.Circular)
        {
            return GetCircularRoom();
        }

        return null;
    }

    private static IRoom GetRectangularRoom()
    {

        string? sizeInput;
        do
        {
            Console.Write("\nEnter size (default 5x5): ");

            int width = 5, height = 5;
            sizeInput = PromptUtils.ReadLineWithCancel();

            if (string.IsNullOrWhiteSpace(sizeInput))
                return new RectangularRoom(new Point(1, 2), width, height);

            var match = GetInputMatch(sizeInput, @"(\d+)[x](\d+)");
            if (!match.Success)
            {
                Console.WriteLine("\nInvalid input, try again.");
                continue;
            }

            width = int.Parse(match.Groups[1].Value);
            height = int.Parse(match.Groups[2].Value);

            return new RectangularRoom(new Point(1, 2), width, height);
        }
        while (sizeInput != "[ESC]");

        return null;
    }

    private static IRoom GetCircularRoom()
    {
        string? radiusInput;
        do
        {
            Console.Write("\nEnter radius (default 10): ");

            radiusInput = PromptUtils.ReadLineWithCancel();
            var radius = 10;

            if (string.IsNullOrWhiteSpace(radiusInput))
                return new CircularRoom(new Point(0, 0), radius);

            var match = GetInputMatch(radiusInput, @"(\d+)");
            if (!match.Success)
            {
                Console.WriteLine("\nInvalid input, try again.");
                continue;
            }

            radius = int.Parse(match.Groups[1].Value);

            return new CircularRoom(new Point(0, 0), radius);
        }
        while (radiusInput != "[ESC]");

        return null;
    }

    private static Match GetInputMatch(string input, string pattern)
    {
        return new Regex(pattern, RegexOptions.IgnoreCase).Match(input);
    }
}


