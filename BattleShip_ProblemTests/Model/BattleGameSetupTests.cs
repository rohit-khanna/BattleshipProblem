using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Game.Model.Tests
{
    [TestFixture()]
    public class BattleGameSetupTests
    {
        [Test(Description = "Test the Game Setup for battle field")]
        public void PrepareGameSetupTest()
        {
            //Arrange
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestInputFiles/testInput.txt");
            var battleGameSetUp = new BattleGameSetup();


            //Act
            battleGameSetUp.PrepareGameSetup(path);


            //Assert
            Assert.AreEqual(battleGameSetUp.BattleField.GetUpperBound(0) + 1, 4);
            Assert.AreEqual(battleGameSetUp.BattleField.GetUpperBound(1) + 1, 3);
            Assert.AreEqual(battleGameSetUp.BattleshipsPerUser, 2);
            Assert.IsTrue(battleGameSetUp.GameCoordinates.Equals(new GameCoordinate { X = XCoordinate.THREE, Y = YCoordinate.D }));
            Assert.AreEqual(battleGameSetUp.Players.Count, 2);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[0]).ShipFleet.Count, 1);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[0]).ShipFleet[0].Height, 1);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[0]).ShipFleet[0].Width, 1);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[0]).MissileFiringSequence.Count(), 3);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[0]).ShipFleet[0].PlacementLocation,
                new GameCoordinate { X = XCoordinate.ONE, Y = YCoordinate.A }.ConvertToPoint());

            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[1]).ShipFleet.Count, 1);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[1]).ShipFleet[0].Height, 1);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[1]).ShipFleet[0].Width, 1);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[1]).MissileFiringSequence.Count(), 2);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[1]).ShipFleet[0].PlacementLocation,
                new GameCoordinate { X = XCoordinate.TWO, Y = YCoordinate.B }.ConvertToPoint());



        }


        /// <summary>
        /// Prepares the game setup test for only P type ships.
        /// </summary>
        [Test(Description = "test to setup the game with only P Type ships [3 diff dimensions] for both players")]
        public void PrepareGameSetupTest_OnlyPTypeShips()
        {
            //Arrange
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestInputFiles\OnlyPTypeShips_Input.txt");
            var battleGameSetUp = new BattleGameSetup();

            //Act
            battleGameSetUp.PrepareGameSetup(path);

            //Assert
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[0]).ShipFleet.Count, 3);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[1]).ShipFleet.Count, 3);

            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[0]).ShipFleet.Exists(ship => ship.GetType() == typeof(BattleshipTypeQ)), false);
            Assert.AreEqual(((BattlePlayer)battleGameSetUp.Players[1]).ShipFleet.Exists(ship => ship.GetType() == typeof(BattleshipTypeQ)), false);
        }
    }
}