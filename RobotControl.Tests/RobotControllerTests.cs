using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotControl.Core;
using RobotControl.Core.Enums;
using System;

namespace RobotControl.Tests;

[TestClass]
public class RobotControllerTests
{
    [TestMethod]
    public void RectangularRoom_StartPositionOutsideGrid()
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        ArgumentException ex = Assert.ThrowsException<ArgumentException>(delegate
        {
            var startPoint = new Point(-1, 0);
            var rectangularRoom = new RectangularRoom(startPoint, 5, 5);
        });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        Assert.AreEqual(ex?.Message, "The start position isn't in the generated room grid");
    }

    [TestMethod]
    public void CircularRoom_StartPositionOutsideGridException()
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        ArgumentException ex = Assert.ThrowsException<ArgumentException>(delegate
        {
            var startPoint = new Point(-11, 0);
            var circularRoom = new CircularRoom(startPoint, 10);
        });
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

        Assert.AreEqual(ex?.Message, "The start position isn't in the generated room grid");
    }

    [DataTestMethod]
    [DataRow("RFRFFRFRF", Language.English)]
    [DataRow("HGHGGHGHG", Language.Swedish)]
    public void Example1_RectangularRoom(string commandInput, Language language)
    {
        var startPoint = new Point(1, 2);
        var rectangularRoom = new RectangularRoom(startPoint, width: 5, height: 5);

        var translator = new RobotTranslator(language);
        var controller = new RobotController(rectangularRoom, translator);

        var positionString = controller.Move(commandInput);

        Assert.AreEqual("1 3 N", positionString);
    }

    [TestMethod]
    [DataRow("RRFLFFLRF", Language.English)]
    [DataRow("HHGVGGVHG", Language.Swedish)]
    public void Example2_CircularRoom(string commandInput, Language language)
    {
        var startPoint = new Point(0, 0);
        var circularRoom = new CircularRoom(startPoint, radius: 10);

        var translator = new RobotTranslator(language);
        var controller = new RobotController(circularRoom, translator);

        var positionString = controller.Move(commandInput);

        if (language == Language.English)
            Assert.AreEqual("3 1 E", positionString);
        else if (language == Language.Swedish)
            Assert.AreEqual("3 1 Ö", positionString);
    }
}
