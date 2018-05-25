using System;
using System.Drawing;

namespace Game.Model
{
    /// <summary>
    /// Class for Game Coordinates
    /// </summary>
    public class GameCoordinate
    {
        public XCoordinate X { get; set; }
        public YCoordinate Y { get; set; }



        /// <summary>
        /// Gets the point equivalent.
        /// </summary>
        /// <returns></returns>
        public Point ConvertToPoint()
        {
            Point equivalentPoint = new Point();
            equivalentPoint.X = (int)X;
            equivalentPoint.Y = (int)Y;

            return equivalentPoint;
        }

        /// <summary>
        /// Converts to game coordinate.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>instance of <see cref="GameCoordinate"/> </returns>
        public static GameCoordinate ConvertToGameCoordinate(Point point)
        {
            return
            new GameCoordinate()
            {
                Y = (YCoordinate)Convert.ToInt32(Enum.Parse(typeof(YCoordinate), point.Y.ToString())),
                X = (XCoordinate)Convert.ToInt32(Enum.Parse(typeof(XCoordinate), point.X.ToString())),
            };
        }

        /// <summary>
        /// Converts to game coordinate.
        /// </summary>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <returns>instance of <see cref="GameCoordinate"/> </returns>
        public static GameCoordinate ConvertToGameCoordinate(string X, string Y)
        {
            return
            new GameCoordinate()
            {
                Y = (YCoordinate)Convert.ToInt32(Enum.Parse(typeof(YCoordinate), Y)),
                X = (XCoordinate)Convert.ToInt32(Enum.Parse(typeof(XCoordinate), X)),
            };
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Y.ToString() + ((int)X).ToString();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(GameCoordinate))
            {
                return (X == (obj as GameCoordinate).X) && (Y == (obj as GameCoordinate).Y);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }



    }// end Game Coordinate

    public enum XCoordinate
    {
        ONE = 1,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE = 9
    }

    public enum YCoordinate
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        K = 11,
        L = 12,
        M = 13,
        N = 14,
        O = 15,
        P = 16,
        Q = 17,
        R = 18,
        S = 19,
        T = 20,
        U = 21,
        V = 22,
        W = 23,
        X = 24,
        Y = 25,
        Z = 26

    }

}
