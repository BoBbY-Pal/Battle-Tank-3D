using UnityEngine;

namespace MVC.Tank
{
    public class TankService : MonoSingletonGeneric<TankService>
    {
        public TankView tankView;
        void Start()
        {
            CreateNewTank();
        }

        private TankController CreateNewTank()
        {
            PlayerTankModel model = new PlayerTankModel(10, 100f);
            TankController tank = new TankController(model, tankView);
            return tank;             
        }

    }
}
