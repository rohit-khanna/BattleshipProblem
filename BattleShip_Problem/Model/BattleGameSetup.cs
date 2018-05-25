using Game.ExceptionHandler;
using Game.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Game.Model
{
    public class BattleGameSetup : BaseGameSetup<GameCoordinate>
    {
        /// <summary>
        /// Gets or sets the battle field.
        /// </summary>
        /// <value>
        /// The battle field.
        /// </value>
        public int[,] BattleField { get; set; }

        /// <summary>
        /// Gets or sets the battleships per user.
        /// </summary>
        /// <value>
        /// The battleships per user.
        /// </value>
        public int BattleshipsPerUser { get; set; }


        /// <summary>
        /// Prepares the game setup.
        /// </summary>
        /// <param name="inputInstructionsFilePath">The input instructions file path.</param>
        /// <exception cref="InvalidInputFileException"></exception>
        public override void PrepareGameSetup(string inputInstructionsFilePath)
        {
            if (!string.IsNullOrWhiteSpace(inputInstructionsFilePath))
            {
                //fetch Setup details
                using (IRead reader = new ReadTextFile())
                {
                    try
                    {
                        var arrayOfDataLines = reader.Read(inputInstructionsFilePath).ToArray();
                        if (arrayOfDataLines != null && arrayOfDataLines.Length > 0)
                        {
                            PrepareBattleGameSetupInstance(inputInstructionsFilePath, arrayOfDataLines, this);
                        }
                        else
                        {
                            throw new InvalidInputFileException(inputInstructionsFilePath);
                        }

                    }
                    catch (FileNotFoundException)
                    {
                        Handler.HandleException("Setup file Path not found in system", false);
                        return;
                    }
                    catch (InvalidInputFileException exception)
                    {
                        Handler.HandleException(exception.Message, true);
                        return;
                    }
                    catch (Exception exception)
                    {
                        Handler.HandleException(exception.Message, true);
                    }

                }
            }
            else
            {
                Handler.HandleException("Setup Input file Path not provided.", false);
            }
        }



        #region Private Static Methods


        /// <summary>
        /// Prepares the battle game setup instance.
        /// </summary>
        /// <param name="filePathForSetUp">The file path for set up.</param>
        /// <param name="arrayOfDataLines">The array of data lines.</param>
        /// <param name="battleShipGameSetup">The battle ship game setup.</param>
        /// <exception cref="Game.ExceptionHandler.InvalidInputFileException">
        /// </exception>
        private static void PrepareBattleGameSetupInstance(string filePathForSetUp, string[] arrayOfDataLines, BattleGameSetup battleShipGameSetup)
        {
            if (arrayOfDataLines.Length > 4) // as per the problem statment
            {
                // 1. Setup BattleField Measurements
                // Assumption: 5 E ==  5<space>E
                setupBattleField(filePathForSetUp, arrayOfDataLines[0], battleShipGameSetup);

                // 2. instantiate battle field
                battleShipGameSetup.BattleField = new int[(int)battleShipGameSetup.GameCoordinates.Y, (int)battleShipGameSetup.GameCoordinates.X];


                // 3. no of battleships each player has
                // Assumption: 2 == 2
                battleShipGameSetup.BattleshipsPerUser = Convert.ToInt32(arrayOfDataLines[1]);


                // 4. prepare Ship Fleet for PLayers
                var player1 = new Model.BattlePlayer() { ID = 1 };
                var player2 = new Model.BattlePlayer() { ID = 2 };
                int i;

                // start with '2' index  till a Non-ShipType is found or( EndOfFile-2) lines achieved
                for (i = 2; i < arrayOfDataLines.Length - 2; i++)
                {
                    var battleShipDetailsAry = arrayOfDataLines[i].Split(' ');

                    // Assumption: Q 1 1 A1 B2  == Q<space>1<space>1<space>A1<space>B2   [11] means [Width Height]
                    // Assumption: P 2 1 D4 C3  == P<space>2<space>1<space>D4<space>C3

                    BattleshipType shipType;
                    Enum.TryParse(battleShipDetailsAry[0], true, out shipType);

                    if (shipType == BattleshipType.NONE)
                    {
                        return;
                    }

                    // Width=X, Height = Y
                    var shipDimensions = new Point(Convert.ToInt32(battleShipDetailsAry[1]), Convert.ToInt32(battleShipDetailsAry[2]));

                    player1.ShipFleet.AddRange(prepareShipsForPlayer(shipType, shipDimensions, battleShipDetailsAry[3]));
                    player2.ShipFleet.AddRange(prepareShipsForPlayer(shipType, shipDimensions, battleShipDetailsAry[4]));
                }

                if ((player1.ShipFleet.Count > battleShipGameSetup.BattleshipsPerUser) ||
                    (player2.ShipFleet.Count > battleShipGameSetup.BattleshipsPerUser))
                {
                    throw new InvalidInputFileException(filePathForSetUp, " More ships found per user than specified");
                }
                else
                {

                    battleShipGameSetup.Players.Add(player1);
                    battleShipGameSetup.Players.Add(player2);

                    // 5. Missile Target sequence for PLayer One 
                    getMissileFiringSequenceForPlayer(arrayOfDataLines[i], player1);

                    //6. Missile Target sequence for PLayer Two
                    getMissileFiringSequenceForPlayer(arrayOfDataLines[i + 1], player2);
                }

            }

            else
            {
                throw new InvalidInputFileException(filePathForSetUp, " Invalid Lines");
            }


        }


        /// <summary>
        /// Prepares the ships for player.
        /// </summary>
        /// <param name="shipType">Type of the ship.</param>
        /// <param name="shipDimensions">The ship dimensions.</param>
        /// <param name="shipPositions">The ship positions.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Invalid Ship Length</exception>
        private static IEnumerable<BaseBattleShip> prepareShipsForPlayer(BattleshipType shipType, Point shipDimensions, string shipPositions)
        {
            BaseBattleShip ship = null;
            List<BaseBattleShip> shipFleet = new List<BaseBattleShip>();

            if (shipPositions.Length % 2 != 0) // even length
            {
                throw new Exception("Invalid Ship Length");
            }

            var numberOfShips = shipPositions.Length / 2;


            for (int i = 0; i < shipPositions.Length; i = i + 2) // looking for ship dimensions
            {
                // create Ship             

                switch (shipType)
                {

                    case BattleshipType.P:
                        ship = new BattleshipTypeP();
                        break;
                    case BattleshipType.Q:
                        ship = new BattleshipTypeQ();
                        break;
                    default:
                        break;
                }

                ship.Width = shipDimensions.X;
                ship.Height = shipDimensions.Y;

                ship.PlacementLocation = new Point(
                   Convert.ToInt32(Enum.Parse(typeof(XCoordinate), shipPositions[i + 1].ToString())),
                   Convert.ToInt32(Enum.Parse(typeof(YCoordinate), shipPositions[i].ToString()))
                   );

                shipFleet.Add(ship);
            }

            return shipFleet;
        }



        /// <summary>
        /// Setups the battle field.
        /// </summary>
        /// <param name="filePathForSetUp">The file path for set up.</param>
        /// <param name="battleFieldDimensionData">battleFieldDimensionData</param>
        /// <param name="battleShipGameSetup">The battle ship game setup.</param>
        /// <exception cref="Game.ExceptionHandler.InvalidInputFileException">invalid dimensions of battle area</exception>
        private static void setupBattleField(string filePathForSetUp, string battleFieldDimensionData, BattleGameSetup battleShipGameSetup)
        {
            var gameDimension = battleFieldDimensionData.Split(' '); // width and Height separated by Space
            if (gameDimension == null || gameDimension.Length != 2)
                throw new InvalidInputFileException(filePathForSetUp, " invalid dimensions of battle area");

            battleShipGameSetup.GameCoordinates = GameCoordinate.ConvertToGameCoordinate(gameDimension[0], gameDimension[1]);

        }



        /// <summary>
        /// Gets the missile firing sequence for player.
        /// </summary>
        /// <param name="firingSequenceString">The firing sequence string.</param>
        /// <param name="player">The player.</param>
        private static void getMissileFiringSequenceForPlayer(string firingSequenceString, BattlePlayer player)
        {
            // Assumption: A1 B2 B2 B3 == A1<space>B2<space>B2<space>B3
            var sequenceAry = firingSequenceString.Split(' ');

            foreach (var sequence in sequenceAry)
            {
                player.MissileFiringSequence.Enqueue(GameCoordinate.ConvertToGameCoordinate(sequence.ToCharArray()[1].ToString(), sequence.ToCharArray()[0].ToString()));
            }
        }

        #endregion
    }

}
