using Enums;
using UI;
using UnityEngine;
public class PlayerTankController 
{
    private bool b_IsFireButtonPressed;
    private PlayerTankModel TankModel { get; }
    private PlayerTankView TankView { get; }

    public PlayerTankController(PlayerTankModel tankModel, PlayerTankView tankPrefab)
    {
        this.TankModel = tankModel;
        TankView = Object.Instantiate(tankPrefab);
        SetHealthSlider();
        TankView.SetTankController(this);
        Debug.Log("Tank Created", TankView);
        SubscribeEvents();
    }

    public Transform GetPlayerTransform()
    {
        return TankView.transform;
    }
    public void EngineAudio()
    {
        // If there is no input (Tank Idle State)
        if(Mathf.Abs(TankView.leftJoystick.Vertical) < 0.1f && Mathf.Abs(TankView.leftJoystick.Horizontal) < 0.1f) 
        {
            // And if Driving audio is playing
            if(TankView.movementAudioSource.clip == TankView.engineDriving) 
            {   // Change the clip to Idle and play it
                TankView.movementAudioSource.clip = TankView.engineIdling;
                TankView.movementAudioSource.pitch = Random.Range(TankView.originalPitch - TankModel.pitchRange,
                                                                  TankView.originalPitch + TankModel.pitchRange);
                TankView.movementAudioSource.Play();
            }
        }
        else
        {
            // If tank is moving and idling clip is currently plating
            if(TankView.movementAudioSource.clip == TankView.engineIdling)
            {
                TankView.movementAudioSource.clip = TankView.engineDriving;
                TankView.movementAudioSource.pitch = Random.Range(TankView.originalPitch - TankModel.pitchRange, TankView.originalPitch + TankModel.pitchRange);
                TankView.movementAudioSource.Play();
            }
        }
    }

    public void MovePlayerTank()
    {
        // rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        Vector3 movement = TankView.transform.forward * (TankView.leftJoystick.Vertical * TankModel.Speed * Time.deltaTime);
        // Applying movement on rigidbody
        TankView.tank_rb.MovePosition( TankView.tank_rb.position + movement);
    }

    public void TurnPlayerTank()
    {
        // Number of degrees to be turned 
        float turn = TankView.leftJoystick.Horizontal * TankModel.RotationRate * Time.deltaTime;
      
        // Make this into a Rotation
        Quaternion turnRotation = Quaternion.Euler(0f,turn,0f);
       
        // Applying rotation to the rigidbody
        TankView.tank_rb.MoveRotation(TankView.tank_rb.rotation * turnRotation);
    }

    public void RotatePlayerTankTurret()
    {
        Vector3 desiredRotation = Vector3.up * (TankView.rightJoystick.Horizontal * TankModel.TurretRotationRate * Time.deltaTime);
        TankView.turret.transform.Rotate(desiredRotation, Space.Self);
    }

    public void TakeDamage(float damage)
    {
        TankModel.CurrentHealth -= damage;
        SetHealthSlider();  // Update the health bar
        if (TankModel.CurrentHealth - damage <= 0 && !TankModel.IsDead)
        {   
            TankModel.IsDead = true;
            TankView.PlayerDied();
        }
    }

    public void SetDeathTrue()
    {
        TankModel.IsDead = true;
        UnSubscribeEvents();
    }
    
    public void SetHealthSlider()
    {   
        // Set the value and color of the health slider.
        TankView.healthSlider.value = TankModel.CurrentHealth;
        TankView.fillImage.color = Color.Lerp(TankModel.ZeroHealthColor, TankModel.FullHealthColor,
            TankModel.CurrentHealth / TankModel.MaxHealth);
    }

    public void SetAimSlider()
    {
        TankView.aimSlider.value = TankModel.CurrentLaunchForce;
    }

    public void FireInputCheck()
    {
        TankView.aimSlider.value = TankModel.MinLaunchForce;
        
        // If the max force has been exceeded and the bullet hasn't yet been launched...
        if (TankModel.CurrentLaunchForce >= TankModel.MaxLaunchForce && !TankModel.b_IsFired)
        {   
            // ... use the max force and launch the bullet shell.
            TankModel.CurrentLaunchForce = TankModel.MaxLaunchForce;
            Fire();
        }
     
        // Otherwise, if the fire button is being held and the bullet hasn't been launched yet...
        else if (b_IsFireButtonPressed && !TankModel.b_IsFired)     
        {
            // ... Increment the launch force and update the slider.
            TankModel.CurrentLaunchForce += TankModel.ChargeSpeed * Time.deltaTime;
            TankView.aimSlider.value = TankModel.CurrentLaunchForce;
        }
    }

    // if the fire button is released and the bullet hasn't been launched yet...
    private void FireButtonReleased()
    {
        b_IsFireButtonPressed = false;
        
        if(!TankModel.b_IsFired)
        {
            // ... Launch the shell
            Fire();
        }
    }
    
    // if the fire button has just started being pressed...
    private void FireButtonPressed()
    {
        // ... reset the fired flag and reset the launch force.
        TankModel.b_IsFired = false;
        TankModel.CurrentLaunchForce = TankModel.MinLaunchForce;
            
        // ... Change the clip to the charging clip and start it playing.
        SFXManager.Instance.Play(SoundTypes.ShotCharging);
        
        b_IsFireButtonPressed = true;
    }
    private void Fire()
    {
        TankModel.b_IsFired = true;
        BulletService.Instance.CreateBullet(TankModel.BulletType, TankView.fireTransform, TankModel.CurrentLaunchForce);
        
        SFXManager.Instance.Play(SoundTypes.Firing);
        
        // Reset the launch force.
        TankModel.CurrentLaunchForce = TankModel.MinLaunchForce;
    
        EventHandler.Instance.InvokeBulletFiredEvent();
    }

    private void SubscribeEvents()
    {
        EventHandler.Instance.OnEnemyDeath += UpdateEnemiesKilledCount; 
        EventHandler.Instance.OnBulletFired += UpdateBulletFiresCount;
        EventHandler.Instance.OnFireButtonPressed += FireButtonPressed;
        EventHandler.Instance.OnFireButtonReleased += FireButtonReleased;
    }

    private void UnSubscribeEvents()
    {
        EventHandler.Instance.OnEnemyDeath -= UpdateEnemiesKilledCount;
        EventHandler.Instance.OnBulletFired -= UpdateBulletFiresCount;
        EventHandler.Instance.OnFireButtonPressed -= FireButtonPressed;
        EventHandler.Instance.OnFireButtonReleased -= FireButtonReleased;
    }

    private void UpdateBulletFiresCount()
    {
        int count = ++TankModel.BulletsFired;
        UIManager.Instance.UpdateFireCount(count);
        AchievementHandler.Instance.BulletsFiredAchievement(count);
        
    }
    
    private void UpdateEnemiesKilledCount()
    {
        int count = ++TankModel.EnemiesKilled;
        UIManager.Instance.UpdateKills(count);
        AchievementHandler.Instance.EnemyKilledAchievement(count);
    }
}