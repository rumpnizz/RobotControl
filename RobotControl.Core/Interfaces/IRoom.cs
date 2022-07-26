namespace RobotControl.Core.Interfaces;

public interface IRoom
{
    IPoint StartPosition { get; }
    bool Contains(IPoint point);
}
