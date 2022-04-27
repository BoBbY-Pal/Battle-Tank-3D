using UnityEditor;
using UnityEngine;

namespace MVC.Tank
{
    public class TankController 
    {
        private TankModel TankModel { get; }
        private  TankView TankView { get; }
        public TankController(TankModel tankModel, TankView tankPrefab)
        {
            TankModel = tankModel;
            TankView = GameObject.Instantiate<TankView>(tankPrefab);
            Debug.Log("Tank Created", TankView);
        }
    }
}
