namespace Game
{
    interface IGame<TGameSetup, TFieldCoordinates> where TGameSetup : Model.BaseGameSetup<TFieldCoordinates>
    {

        /// <summary>
        /// Method to prepare the Game Setup.
        /// <para>
        /// This also fetches the inputs from external files
        /// </para>
        /// </summary>
        /// <param name="filePathForSetUp">The file path for set up details.</param>
        /// <returns></returns>
        TGameSetup PrepareGameSetup(string filePathForSetUp);

        /// <summary>
        /// Method to Play The Game
        /// </summary>
        /// <param name="gameSetup">The game setup.</param>
        void Play(TGameSetup gameSetup);
    }
}
