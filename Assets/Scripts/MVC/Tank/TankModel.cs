namespace MVC.Tank
{
    public class TankModel 
    {
        private int Speed { get; }
        private float Health { get; }                 
        public TankModel(int speed, float health)
        {
            Speed = speed;
            Health = health;
        }

    }
}
