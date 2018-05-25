namespace Game.Model
{
    public abstract class BasePlayer
    {
        public int ID { get; set; }

        public virtual string name
        {
            get
            {
                return "Player-" + ID;
            }

            set
            {

            }
        }


    }
}
