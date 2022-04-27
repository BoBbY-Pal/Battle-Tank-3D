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
            TankModel model = new TankModel(10, 100f);
            TankController tank = new TankController(model, tankView);
            return tank; 
            
        }

    }
}
