using System.Collections;
using Enums;
using UnityEngine;

public class PatrollingState : EnemyStateBase
{
    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChangeWalkPoint());
    }


    public override void OnStateEnter()
    {
        base.OnStateEnter();
        tankView.activeState = EnemyState.Patrolling;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    

    private void Update()
    { 
        if (tankModel.b_PlayerInRange && !tankModel.b_PlayerInAttackRange)
            tankView.currentEnemyState.ChangeCurrentState(tankView.chasingState);
        else if(tankModel.b_PlayerInRange && tankModel.b_PlayerInAttackRange)
            tankView.currentEnemyState.ChangeCurrentState(tankView.attackingState);
        
        Patrolling();
        ResetTurretRotation();
    }

    private void Patrolling()
    {
        if (!tankModel.b_WalkPoint)
            SearchWalkPoint();
        if (tankModel.b_WalkPoint)
        {
            tankView.navMeshAgent.SetDestination(tankModel.walkPoint);
        }

        Vector3 distToWalkPoint = tankView.transform.position - tankModel.walkPoint;
        if (distToWalkPoint.magnitude < 1f)
        {
            tankModel.b_WalkPoint = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZAxis = Random.Range(-tankModel.walkPointRange, tankModel.walkPointRange);
        float randomXAxis = Random.Range(-tankModel.walkPointRange, tankModel.walkPointRange);
        tankModel.walkPoint = new Vector3(tankView.transform.position.x + randomXAxis,
                                            tankView.transform.position.y, 
                                          tankView.transform.position.z + randomZAxis);
        if (Physics.Raycast(tankModel.walkPoint, -tankView.transform.up, 2f, tankView.groundLayerMask))
            tankModel.b_WalkPoint = true;
    }

    private IEnumerator ChangeWalkPoint()
    {
        while (true)
        {
            yield return new WaitForSeconds(tankModel.patrolTime);
            tankModel.b_WalkPoint = false;
        }
    }

    private void ResetTurretRotation()
    {
        if (tankView.turret.transform.rotation.eulerAngles.y - tankView.transform.rotation.eulerAngles.y > 1 ||
            tankView.turret.transform.rotation.eulerAngles.y - tankView.transform.rotation.eulerAngles.y < -1)
        {
            Vector3 desiredRotation = Vector3.up * (tankModel.turretRotationRate * Time.deltaTime);
            tankView.turret.transform.Rotate(desiredRotation, Space.Self);
        }
    }
}