using UnityEngine;

public class EnemyTankController
{
    private EnemyTankModel tankModel { get; }
    private EnemyTankView tankView { get; }
    
    public EnemyTankController(EnemyTankModel tankModel, EnemyTankView tankPrefab)
    {
        this.tankModel = tankModel;
        tankView = Object.Instantiate(tankPrefab, new Vector3(3,0,-3), new Quaternion(0,0,0,0));
        tankView.SetController(this);
    }
    
    public void UpdateTankController()
    {
        tankModel.b_PlayerInRange = Physics.CheckSphere(tankView.transform.position, tankModel.patrollingRange,
                                                    tankView.playerLayerMask);
        tankModel.b_PlayerInAttackRange = Physics.CheckSphere(tankView.transform.position, tankModel.attackRange,
                                                           tankView.playerLayerMask);
    }
    
    public void TakeDamage(float damage)
    {
        tankModel.currentHealth -= damage;
        SetHealthUI();
        ShowHealthUI();
        if (tankModel.currentHealth <= 0 && !tankModel.b_IsDead)
        {
            tankView.Death();
        }   
    }

    public void SetHealthUI()
    {
        tankView.healthSlider.value = tankModel.currentHealth;
        tankView.fillImage.color = Color.Lerp(tankModel.zeroHealthColor, tankModel.fullHealthColor,
                                              tankModel.currentHealth / tankModel.maxHealth);
    }

    private void ShowHealthUI()
    {
        if (tankView)
        {
            tankView.healthSlider.gameObject.SetActive(true);
        }

        // yield return new WaitForSeconds(3f); //new WaitForSeconds(3f);
        //  if (tankView)
        //  {
        //      tankView.healthSlider.gameObject.SetActive(false);
        //  }
    }

    public void SetDeathTrue()
    {
        tankModel.b_IsDead = true;
        
        EventHandler.Instance.InvokeEnemyDeathEvent();
    }


    public Color GetTankColor() => tankModel.tankColor;
    public float GetRandLaunchForce() => Random.Range(tankModel.minLaunchForce, tankModel.maxLaunchForce);
    public EnemyTankModel GetModel() =>   tankModel; // Getter for Enemy Tank Model
    

}
