using RobotControl.Core.Interfaces;

namespace RobotControl.Core;

public abstract class RoomBase : IRoom
{
    public IPoint StartPosition { get; private set; }

    protected IPoint[]? Points;

    protected RoomBase(IPoint startPoint)
    {
        StartPosition = startPoint;
    }

    public bool Contains(IPoint point) 
        => Points != null && Points.Contains(point);

    protected abstract void GeneratePoints();

    protected void ValidateStartPosition()
    {
        if (!Contains(StartPosition))
            throw new ArgumentException("The start position isn't in the generated room grid");
    }
}
