using System.Collections.Generic;

namespace Game.Model
{
    public class BattlePlayer : BasePlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BattlePlayer"/> class.
        /// </summary>
        public BattlePlayer()
        {
            ShipFleet = new List<BaseBattleShip>();
            MissileFiringSequence = new Queue<GameCoordinate>();
        }

        public int[,] BattleField { get; set; }

        /// <summary>
        /// Gets the ship fleet. 
        /// List of <see cref="BaseBattleShip"/> 
        /// </summary>      
        public List<BaseBattleShip> ShipFleet { get; }

        /// <summary>
        /// Gets the missile firing sequence.
        /// </summary>
        public Queue<GameCoordinate> MissileFiringSequence { get; }



        /// <summary>
        /// Place the ships on locations.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public void PlaceShipsOnLocations(int rows, int columns)
        {

            BattleField = new int[rows, columns];  // [Y,X]

            foreach (var ship in ShipFleet)
            {
                ship.GetOccupiedCoordinates().ForEach(p =>
                {
                    BattleField[p.Y - 1, p.X - 1] = 1; // mark them as Filled/Occupied
                });

            }
        }


    }
}
