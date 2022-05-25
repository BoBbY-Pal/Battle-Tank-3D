using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.VFX;

public class Patrolling : StateManager
{
    protected override void Start()
    {
        base.Start();
        ChangeWalkPoint();
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

    private void patrolling()
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
    }

    private IEnumerator ChangeWalkPoint()
    {
        while (true)
        {
            yield return new WaitForSeconds(tankModel.patrolTime);
        }
    }
}