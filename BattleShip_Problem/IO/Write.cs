using Game.Model;
using System;

namespace Game.IO
{
    public class Write
    {
        private Write()
        { }

        /// <summary>
        /// Writes to console.
        /// </summary>
        /// <param name="gameResponse">The game response.</param>
        public static void WriteToConsole(GameResponse gameResponse)
        {
            Console.WriteLine(gameResponse.ToString());
        }

        /// <summary>
        /// Writes to console.
        /// </summary>
        /// <param name="winnerIdentifier">The winner identifier.</param>
        public static void WriteToConsole(int winnerIdentifier)
        {
            if (winnerIdentifier == -1)
                Console.WriteLine(String.Format("PEACE"));
            else
                Console.WriteLine(String.Format("Player-{0} won", winnerIdentifier));

        }

        /// <summary>
        /// Writes to console.
        /// </summary>
        /// <param name="errorResponse">The error response.</param>
        public static void WriteToConsole(ErrorResponse errorResponse)
        {
            if (errorResponse != null)
                Console.WriteLine(errorResponse.ToString());
        }
    }
}
