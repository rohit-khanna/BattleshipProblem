namespace Game.Model
{
    /// <summary>
    ///  Battle SHip : Type Q
    /// </summary>
    /// <seealso cref="Game.Model.BaseBattleShip" />
    public class BattleshipTypeQ : BaseBattleShip
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BattleshipTypeQ"/> class.
        /// </summary>
        public BattleshipTypeQ() : base(BattleshipType.Q, 2)
        {

        }
    }
}
