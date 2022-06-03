using UnityEngine;

[RequireComponent(typeof(EnemyTankView))]
public class EnemyStateBase : MonoBehaviour
{   
    protected EnemyTankView tankView;
    protected EnemyTankModel tankModel;

    protected virtual void Awake()
    {
        tankView = GetComponent<EnemyTankView>();
    }

    protected virtual void Start()
    {
        tankModel = EnemyTankService.Instance.tankController.GetModel(); 
    }

    public virtual void OnStateEnter()
    {
        this.enabled = true;
    }
    
    public void ChangeCurrentState(EnemyStateBase newEnemyState)        // Logic for changing the states
    {   
        // if something is already in the current state disable it.
        if (tankView.currentEnemyState != null)
        {
            tankView.currentEnemyState.OnStateExit();
        }
        
        // else enter new state to current & enable it.
        tankView.currentEnemyState = newEnemyState;
        tankView.currentEnemyState.OnStateEnter();
    }
    
    public virtual void OnStateExit()
    {
        this.enabled = false;
    }
}
