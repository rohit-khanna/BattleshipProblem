using System.Collections.Generic;

namespace Game.Model
{
    public abstract class BaseGameSetup<TCoordinates>
    {
        public BaseGameSetup()
        {
            Players = new List<Model.BasePlayer>();
        }


        public TCoordinates GameCoordinates { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public List<BasePlayer> Players { get; }

        /// <summary>
        /// Prepares the game setup.
        /// </summary>
        /// <param name="inputInstructionsFilePath">The input instructions file path.</param>
        public abstract void PrepareGameSetup(string inputInstructionsFilePath);



    }
}
