using RobotControl.Core.Enums;

namespace RobotControl.Core.Interfaces;

public interface ITranslator
{
    public char TranslateCompassDirectionShort(CompassDirection compassDirection);
    public RobotCommand GetCommand(char charCommand);
}
