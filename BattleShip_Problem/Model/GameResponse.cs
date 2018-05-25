namespace Game.Model
{
    public class GameResponse
    {
        /// <summary>
        /// Gets or sets the player identifier.
        /// </summary>
        /// <value>
        /// The player identifier.
        /// </value>
        public int playerID { get; set; }


        /// <summary>
        /// Gets or sets the target area.
        /// </summary>
        /// <value>
        /// The target area.
        /// </value>
        public string targetArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether MIssile hit the target.
        /// </summary>
        public bool isHit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [missiles empty].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [missiles empty]; otherwise, <c>false</c>.
        /// </value>
        public bool missilesEmpty { get; set; }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (missilesEmpty)
                return string.Format("Player-{0} no more missiles left to launch", playerID);
            else
                return string.Format("Player-{0} fires a missile with target {1} which got {2}", playerID, targetArea, isHit ? "hit" : "miss");
        }


    }
}
