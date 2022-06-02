using Enums;
using UnityEngine;

public class ChasingState : EnemyStateBase
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        tankView.activeState = EnemyState.Chasing;
    }
    private void Update()
    {
        if (!tankModel.b_PlayerInRange && !tankModel.b_PlayerInAttackRange)
            tankView.currentEnemyState.ChangeCurrentState(tankView.patrollingState);
        else if(tankModel.b_PlayerInRange && tankModel.b_PlayerInAttackRange)
            tankView.currentEnemyState.ChangeCurrentState(tankView.attackingState);
        
        ChasePlayer(); 
    }

    private void ChasePlayer()
    {
        if (!tankView.playerTransform)
        {
            tankView.currentEnemyState.ChangeCurrentState(tankView.patrollingState);
            return;
        }

        tankView.navMeshAgent.SetDestination(tankView.playerTransform.position);
        if (Mathf.Abs(Vector3.SignedAngle(tankView.transform.forward, tankView.turret.transform.forward, Vector3.up)) > 1)
        {
            tankView.turret.transform.Rotate(GetRequiredTurretRotation(), Space.Self);
        }
    }


    private Vector3 GetRequiredTurretRotation()
    {
        Vector3 desiredRotation = new Vector3(0, 0,0);
        float angle = Vector3.SignedAngle(tankView.transform.forward, tankView.turret.transform.forward, Vector3.up);
        if (angle < 0)
        {
            desiredRotation = Vector3.up * (tankModel.turretRotationRate * Time.deltaTime); 
        }
        else if (angle > 0)
        {
            desiredRotation = -Vector3.up * tankModel.turretRotationRate * Time.deltaTime;
        }

        return desiredRotation;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
