using System;
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
        tankModel = tankView.tankController.tankModel; // i want to access model here so that i can use member variables
                                                       // like patrolling range, fire range etc. but this approach is not efficient 
                                                       // becaz if i use this then i need to make model & view public in the controller 

        
        // This is state manager class which will act as a base class to other behaviour classes like chase and attack which will 
        // contain a core logic for their behaviors like patrolling attacking etc.
        // and i'll then i'll initialise the state in enemy tank view.  
    }

    private void OnStateEnter()
    {
        this.enabled = true;
    }
    
    private void OnStateExit()
    {
        this.enabled = false;
    }

    protected virtual void ChangeCurrentState()
    {
        // Logic for changing the states
    }
}
