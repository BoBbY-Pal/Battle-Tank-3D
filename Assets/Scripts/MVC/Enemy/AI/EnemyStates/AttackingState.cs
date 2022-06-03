using System.Collections;
using Enums;
using UnityEngine;

public class AttackingState : EnemyStateBase
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        tankView.activeState = EnemyState.Attacking;
    }

    private void Update()
    {
        if(!tankModel.b_PlayerInRange && !tankModel.b_PlayerInAttackRange)
            tankView.currentEnemyState.ChangeCurrentState(tankView.patrollingState);
        else if (tankModel.b_PlayerInRange && !tankModel.b_PlayerInAttackRange)
            tankView.currentEnemyState.ChangeCurrentState(tankView.chasingState);
        
        StartCoroutine(Attack());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Attack()
    {
        if (!tankView.playerTransform)
        {
            tankView.currentEnemyState.ChangeCurrentState(tankView.patrollingState);
            yield break;
        }

        tankView.navMeshAgent.SetDestination(tankView.transform.position);
        if (!IsPlayerPositioned())
        {   
            tankView.turret.transform.Rotate(GetRequiredTurretRotation(), Space.Self);
        } 
        else if (!tankModel.b_IsFired)
        {
            tankModel.b_IsFired = true;
            FireBullet();
            yield return new WaitForSeconds(tankModel.fireRate);
            tankModel.b_IsFired = false;
        }
    }

    private bool IsPlayerPositioned()
    {
        Vector3 forward = tankView.turret.transform.TransformDirection(Vector3.forward);
        return Physics.Raycast(tankView.transform.position, forward, tankModel.attackRange, tankView.playerLayerMask);
    }
    
    private Vector3 GetRequiredTurretRotation()
    {
        Vector3 desiredRotation = new Vector3(0, 0,0);
        Vector3 targetDirection = tankView.playerTransform.position - tankView.turret.transform.position;
        float angle = Vector3.SignedAngle(targetDirection, tankView.turret.transform.forward, Vector3.up);
        if (angle < 0)
        {
            desiredRotation = Vector3.up * (tankModel.turretRotationRate * Time.deltaTime); 
        }
        else if (angle > 0)
        {
            desiredRotation = -Vector3.up * (tankModel.turretRotationRate * Time.deltaTime);
        }

        return desiredRotation;
    }

    private void FireBullet()
    {
        BulletService.Instance.CreateBullet(tankModel.bulletType, tankView.fireTransform,
                                EnemyTankService.Instance.tankController.GetRandLaunchForce());
        tankView.shootingAudio.clip = tankView.fireClip;
        tankView.shootingAudio.Play();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
