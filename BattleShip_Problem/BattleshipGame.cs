using Game.IO;
using Game.Model;
using System;

namespace Game
{
    class BattleshipGame : IGame<BattleGameSetup, GameCoordinate>
    {
        /// <summary>
        /// Method to Play The Game
        /// </summary>
        /// <param name="gameSetup">The game setup.</param>
        public void Play(BattleGameSetup gameSetup)
        {
            try
            {
                if (gameSetup == null)
                    throw new Exception("Game Setup Not Completed");

                gameSetup.Players.ForEach(player =>
                {
                    ((BattlePlayer)player).PlaceShipsOnLocations(gameSetup.BattleField.GetUpperBound(0) + 1, gameSetup.BattleField.GetUpperBound(1) + 1);
                });

                startTheGame(gameSetup);
            }
            catch (Exception exception)
            {
                Write.WriteToConsole(new ErrorResponse { Message = exception.Message });
                return;
            }
        }

        /// <summary>
        /// Method to prepare the Game Setup.
        /// <para>
        /// This also fetches the inputs from external files
        /// </para>
        /// </summary>
        /// <param name="filePathForSetUp">The file path for set up details.</param>
        /// <returns></returns>
        public BattleGameSetup PrepareGameSetup(string filePathForSetUp)
        {
            var battleShipGameSetup = new BattleGameSetup();

            battleShipGameSetup.PrepareGameSetup(filePathForSetUp);
            return battleShipGameSetup;
        }


        #region private methods


        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="gameSetup">The game setup.</param>
        private static void startTheGame(BattleGameSetup gameSetup)
        {
            bool firstPlayerToPlay = true;
            bool isWinnerFound = false;
            bool isPeaceFound = false;

            BattlePlayer player1 = (BattlePlayer)gameSetup.Players.Find(player => player.ID == 1);
            BattlePlayer player2 = (BattlePlayer)gameSetup.Players.Find(player => player.ID == 2);

            BattlePlayer attacker = null;
            BattlePlayer receiver = null;

            do
            {
                if (firstPlayerToPlay)
                {
                    attacker = player1;
                    receiver = player2;
                }
                else
                {
                    attacker = player2;
                    receiver = player1;
                }

                if (attacker.MissileFiringSequence.Count > 0)
                {
                    LoadNextMissileSequenceAndFire(ref firstPlayerToPlay, ref isWinnerFound, attacker, receiver);
                }
                else // no active command found for Launching new missile
                {
                    Write.WriteToConsole(new GameResponse { missilesEmpty = true, playerID = attacker.ID });
                    if (receiver.MissileFiringSequence.Count <= 0)
                    {
                        isPeaceFound = true;
                        break;
                    }
                    firstPlayerToPlay = !firstPlayerToPlay;
                }


            } while (!isWinnerFound);


            if (isWinnerFound) Write.WriteToConsole(firstPlayerToPlay ? player1.ID : player2.ID);
            if (isPeaceFound) Write.WriteToConsole(-1);

            return;
        }

        /// <summary>
        /// Loads the next missile sequence for attacker and fire on receiver
        /// </summary>
        /// <param name="firstPlayerToPlay">if set to <c>true</c> [first player to play].</param>
        /// <param name="isWinnerFound">if set to <c>true</c> [is winner found].</param>
        /// <param name="attacker">The attacker.</param>
        /// <param name="receiver">The receiver.</param>
        private static void LoadNextMissileSequenceAndFire(ref bool firstPlayerToPlay, ref bool isWinnerFound, BattlePlayer attacker, BattlePlayer receiver)
        {
            var missileLaunchCommand = attacker.MissileFiringSequence.Dequeue();
            if (missileLaunchCommand != null)
            {
                var fireAgain = fireTheMissile(attacker, receiver, missileLaunchCommand);
                if (fireAgain) // hit was successful and its time for attacker to fire again
                {
                    // check if receiver is destroyed completely
                    if (!receiver.ShipFleet.Exists(ship => ship.IsFullyDestroyed == false))
                    {
                        // receiver is Destroyed
                        isWinnerFound = true;
                    }
                }
                else // hit was unsuccessful- time to switch turns 
                {
                    firstPlayerToPlay = !firstPlayerToPlay;
                }
            }
        }

        /// <summary>
        /// Fires the missile and tell the attacker whether they can fire again.
        /// If Missile is Hit then only attacker will be asked to fire again 
        /// </summary>
        /// <param name="attacker">The attacker.</param>
        /// <param name="receiver">The receiver.</param>
        /// <param name="missileLaunchCommand">The missile launch command.</param>
        /// <returns>true, in case missile hit on undestroyed cell/part of receiver ship; else false</returns>
        private static bool fireTheMissile(BattlePlayer attacker, BattlePlayer receiver, GameCoordinate missileLaunchCommand)
        {
            bool isMissileHitTargetAndFireAgain = false;
            var activeShips = receiver.ShipFleet.FindAll(ship => ship.IsFullyDestroyed == false);
            foreach (var activeShip in activeShips)
            {

                if (activeShip.GetMissileResistencePower(missileLaunchCommand.ConvertToPoint()) > 0)
                {
                    activeShip.Attacked(missileLaunchCommand.ConvertToPoint());
                    isMissileHitTargetAndFireAgain = true;
                    Write.WriteToConsole(new GameResponse { missilesEmpty = false, isHit = true, playerID = attacker.ID, targetArea = missileLaunchCommand.ToString() });
                    return isMissileHitTargetAndFireAgain;
                }
            }

            Write.WriteToConsole(new GameResponse { missilesEmpty = false, isHit = false, playerID = attacker.ID, targetArea = missileLaunchCommand.ToString() });
            return isMissileHitTargetAndFireAgain;

        }

        #endregion
    }
}

