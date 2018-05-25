using NUnit.Framework;
using System.Drawing;

namespace Game.Model.Tests
{
    [TestFixture()]
    public class GameCoordinateTests
    {
        [Test()]
        public void ConvertToPointTest()
        {
            // Arrange
            GameCoordinate _coordinate = new Model.GameCoordinate()
            {
                X = XCoordinate.EIGHT,
                Y = YCoordinate.D
            };
            Point _point = new Point(8, 4);

            // Act
            var equivalentPoint = _coordinate.ConvertToPoint();

            //Assert
            Assert.AreEqual(equivalentPoint.X, _point.X);
            Assert.AreEqual(equivalentPoint.Y, _point.Y);
        }

        [Test(Description = "Test for Convert input POINT to Game Coordinate")]
        public void ConvertToGameCoordinateTest()
        {
            // Arrange
            GameCoordinate _coordinate = new Model.GameCoordinate()
            {
                X = XCoordinate.EIGHT,
                Y = YCoordinate.D
            };
            Point _point = new Point(8, 4);

            // Act
            var _equivalentCoordinate = GameCoordinate.ConvertToGameCoordinate(_point);

            //Assert
            Assert.IsTrue(_coordinate.Equals(_equivalentCoordinate));
        }

        [Test(Description = "Test for Convert input X,Y to Game Coordinate")]
        public void ConvertToGameCoordinateTest_1()
        {

            // Arrange
            GameCoordinate _coordinate = new Model.GameCoordinate()
            {
                X = XCoordinate.EIGHT,
                Y = YCoordinate.D
            };
            // Act
            var _equivalentCoordinate = GameCoordinate.ConvertToGameCoordinate("8", "4");

            //Assert
            Assert.IsTrue(_coordinate.Equals(_equivalentCoordinate));
        }
    }
}