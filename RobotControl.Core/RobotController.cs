using RobotControl.Core.Enums;
using RobotControl.Core.Interfaces;

namespace RobotControl.Core;

public class RobotController : IRobotController
{
    public static List<CompassDirection> Directions => new()
    {
        // Order matters for new turn index (clock-wise from the North to West)
        CompassDirection.North,
        CompassDirection.East,
        CompassDirection.South,
        CompassDirection.West
    };

    protected IRoom Room { get; private set; }

    public IPoint CurrentPosition { get; private set; }

    public CompassDirection CurrentDirection { get; private set; }

    protected Language Language { get; private set; }
    protected ITranslator Translator { get; private set; }

    public RobotController(IRoom room, ITranslator translator)
    {
        Room = room;
        Translator = translator;
        CurrentPosition = room.StartPosition;
        CurrentDirection = CompassDirection.North;
    }

    public string Move(string commandInput)
    {
        for (int i = 0; i < commandInput.Length; i++)
        {
            var robotCommand = Translator.GetCommand(commandInput[i]);

            switch (robotCommand)
            {
                case RobotCommand.TurnLeft:
                case RobotCommand.TurnRight:
                    Turn(robotCommand);
                    break;
                case RobotCommand.MoveForward:
                    MoveForward();
                    break;
            }
        }

        return $"{CurrentPosition.X} {CurrentPosition.Y} {Translator.TranslateCompassDirectionShort(CurrentDirection)}";
    }

    protected void Turn(RobotCommand command)
    {
        var idx = Directions.IndexOf(CurrentDirection);

        if (command == RobotCommand.TurnLeft)
            idx -= 1;
        else if (command == RobotCommand.TurnRight)
            idx += 1;

        // Facing north, turning left; set index to last direction index (west)
        if (idx < 0)
            idx = Directions.Count - 1;
        // Facing west, turning right; set index to first direction index (north)
        else if (Directions.Count - 1 < idx)
            idx = 0;

        CurrentDirection = Directions[idx];
    }

    protected void MoveForward()
    {
        int moveX = 0,
            moveY = 0;

        if (CurrentDirection == CompassDirection.West)
            moveX = -1;
        else if (CurrentDirection == CompassDirection.East)
            moveX = 1;
        else if (CurrentDirection == CompassDirection.North)
            moveY = -1;
        else if (CurrentDirection == CompassDirection.South)
            moveY = 1;

        var newPosition = new Point(CurrentPosition.X + moveX, CurrentPosition.Y + moveY);

        if (Room.Contains(newPosition))
            CurrentPosition = newPosition;
    }
}
