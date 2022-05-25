using UnityEngine;

[RequireComponent(typeof(EnemyTankView))]
public class StateManager : MonoBehaviour
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
    
    public void ChangeCurrentState(StateManager newState)        // Logic for changing the states
    {   // if something is already in the current state disable it.
        if (tankView.currentState != null)
        {
            tankView.currentState.OnStateExit();
        }
        
        // else enter new state to current & enable it.
        tankView.currentState = newState;
        tankView.currentState.OnStateEnter();
    }
    
    public virtual void OnStateExit()
    {
        this.enabled = false;
    }
}
