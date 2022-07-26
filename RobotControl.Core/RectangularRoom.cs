using RobotControl.Core.Interfaces;

namespace RobotControl.Core;

public class RectangularRoom : RoomBase
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public RectangularRoom(IPoint startPoint, int width, int height)
        : base(startPoint)
    {
        Width = width;
        Height = height;
        
        GeneratePoints();
        ValidateStartPosition();
    }

    protected override void GeneratePoints()
    {
        Points = new IPoint[Width * Height];

        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
            Points[y * Height + x] = new Point(x, y);
    }
}
