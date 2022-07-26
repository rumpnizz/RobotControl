using RobotControl.Core.Enums;
using RobotControl.Core.Interfaces;

namespace RobotControl.Core;

public class RobotTranslator : ITranslator
{
    private static Dictionary<Language, Dictionary<CompassDirection, char>> _directionShort => new()
    {
        {
            Language.English,
            new Dictionary<CompassDirection, char>() 
            {
                { CompassDirection.North, 'N' },
                { CompassDirection.South, 'S' },
                { CompassDirection.East,  'E' },
                { CompassDirection.West,  'W' }
            }
        },
        {
            Language.Swedish,
            new Dictionary<CompassDirection, char>()
            {
                { CompassDirection.North, 'N' },
                { CompassDirection.South, 'S' },
                { CompassDirection.East,  'Ö' },
                { CompassDirection.West,  'V' }
            }
        }
    };

    private static Dictionary<Language, Dictionary<char, RobotCommand>> _commands => new()
    {
        {
            Language.English,
            new Dictionary<char, RobotCommand>()
            {
                { 'L', RobotCommand.TurnLeft },
                { 'R', RobotCommand.TurnRight },
                { 'F', RobotCommand.MoveForward }
            }
        },
        {
            Language.Swedish,
            new Dictionary<char, RobotCommand>()
            {
                { 'V', RobotCommand.TurnLeft },
                { 'H', RobotCommand.TurnRight },
                { 'G', RobotCommand.MoveForward }
            }
        }
    };

    protected Language Language { get; set; }

    public RobotTranslator(Language language)
    {
        Language = language;
    }

    public char TranslateCompassDirectionShort(CompassDirection compassDirection)
    {
        if (!_directionShort.ContainsKey(Language) || !_directionShort[Language].ContainsKey(compassDirection))
            return '?';

        return _directionShort[Language][compassDirection];
    }

    public RobotCommand GetCommand(char charCommand)
    {
        if (!_commands.ContainsKey(Language) || !_commands[Language].ContainsKey(charCommand))
            return RobotCommand.Unknown;

        return _commands[Language][charCommand];
    }
}
