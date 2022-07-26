using RobotControl.Core.Interfaces;

namespace RobotControl.Core;

public class CircularRoom : RoomBase
{
    public int Radius { get; private set; }

    public CircularRoom(IPoint startPoint, int radius)
        : base(startPoint)
    {
        Radius = radius;
        
        GeneratePoints();
        ValidateStartPosition();
    }

    protected override void GeneratePoints()
    {
        var points = new List<IPoint>();

        // Iterate for standard 2d grid (width, height) by the circle diameter
        // Check if each cells center is within the circle (pythagoras)
        // If so, append it to list
        for (int y = 0; y < Radius * 2; y++)
        for (int x = 0; x < Radius * 2; x++)
        {
            double distX = x + .5 - Radius;
            double distY = y + .5 - Radius;

            double dist = Math.Sqrt(distX * distX + distY * distY);
            
            if (dist < Radius)
            {
                // Since we want the origin point (0, 0) around the center and not top left;
                // move each point upwards and left by the radius
                points.Add(new Point(x - Radius, y - Radius)); 
            }
        }

        Points = points.ToArray();
    }
}
