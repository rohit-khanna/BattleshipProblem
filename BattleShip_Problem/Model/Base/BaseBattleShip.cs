using System.Collections.Generic;
using System.Drawing;

namespace Game.Model
{
    public abstract class BaseBattleShip
    {
        #region Private Members        
        /// <summary>
        /// The type of battleship
        /// </summary>
        private BattleshipType TypeOfBattleship;

        /// <summary>
        /// The ship coordinates hit count
        /// Dictionary to Store Location-HitCapacityLeft
        /// </summary>
        private Dictionary<Point, int> shipCoordinatesHitCapacity;

        /// <summary>
        /// The placement location for Ship
        /// </summary>
        private Point placementLocation;

        #endregion




        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBattleShip"/> class.
        /// </summary>
        /// <param name="shipType">Type of the ship.</param>
        /// <param name="_MissleHitResistenceCapacity">The missle hit resistence capacity.</param>
        public BaseBattleShip(BattleshipType shipType, int _MissleHitResistenceCapacity = 1)
        {
            TypeOfBattleship = shipType;
            MissleHitResistenceCapacity = _MissleHitResistenceCapacity;
            HitCounter = 0;
            //  SafeBattleShipCoordinates = GetOccupiedCoordinates();
            shipCoordinatesHitCapacity = new Dictionary<Point, int>();
        }



        #region Public Members

        /// <summary>
        /// Gets or sets the battleship identifier.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the battleship width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the battleship height.
        /// </summary>
        public int Height { get; set; }


        /// <summary>
        /// Gets or sets PlacementLocation
        /// </summary>
        public Point PlacementLocation
        {
            get { return placementLocation; }
            set
            {
                placementLocation = value;
                //  SafeBattleShipCoordinates = GetOccupiedCoordinates();

                if (shipCoordinatesHitCapacity != null)
                {
                    GetOccupiedCoordinates().ForEach(point =>
                    {
                        if (!shipCoordinatesHitCapacity.ContainsKey(point))
                        {
                            shipCoordinatesHitCapacity.Add(point, MissleHitResistenceCapacity);
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Gets or sets the hit counter.
        /// </summary>
        private int HitCounter { get; set; }

        /// <summary>
        /// The missle hit resistence capacity
        /// </summary>
        public int MissleHitResistenceCapacity { get; set; }

        /// <summary>
        /// Gets a value indicating whether this Battleship is fully destroyed.
        /// 
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fully destroyed; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsFullyDestroyed
        {
            get
            {
                foreach (Point Key in shipCoordinatesHitCapacity.Keys)
                {
                    if (shipCoordinatesHitCapacity[Key] > 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Gets the occupied coordinates by Ship
        /// </summary>
        /// <returns></returns>
        public List<Point> GetOccupiedCoordinates()
        {
            List<Point> localtionsOccupied = new List<Point>();

            if (PlacementLocation.Y > 0 && PlacementLocation.X > 0)
                localtionsOccupied.Add(new Point(PlacementLocation.X, PlacementLocation.Y));

            if (Width > 1)
                for (int i = 1; i < Width; i++)
                {
                    localtionsOccupied.Add(new Point(PlacementLocation.X + i, PlacementLocation.Y));
                }


            if (Height > 1)
                for (int i = 1; i < Height; i++)
                {
                    localtionsOccupied.Add(new Point(PlacementLocation.X, PlacementLocation.Y + i));
                }


            if (Height > 1 && Width > 1)
                localtionsOccupied.Add(new Point(PlacementLocation.X + Width - 1, PlacementLocation.Y + Height - 1));

            return localtionsOccupied;
        }


        /// <summary>
        /// Get the Missile Hit Resistence power for the input cell/location.
        /// </summary>
        /// <param name="locationPoint"></param>
        /// <returns>the </returns>
        public int GetMissileResistencePower(Point locationPoint)
        {
            return shipCoordinatesHitCapacity.ContainsKey(locationPoint) ? shipCoordinatesHitCapacity[locationPoint] : 0;
        }


        /// <summary>
        /// Process the Action : Attack
        /// </summary>
        public void Attacked(Point shipLocation)
        {
            HitCounter++;
            // SafeBattleShipCoordinates.Remove(shipLocation);
            shipCoordinatesHitCapacity[shipLocation]--;
        }

        #endregion


    }// end of class


    /// <summary>
    /// Enum: Type of Battleship
    /// </summary>
    public enum BattleshipType
    {
        NONE = 0,
        P,
        Q
    }
}

