using UnityEngine;
public class PlayerTankController 
{
    private PlayerTankModel tankModel { get; }
    private PlayerTankView tankView { get; }

    public PlayerTankController(PlayerTankModel tankModel, PlayerTankView tankPrefab)
    {
        this.tankModel = tankModel;
        tankView = Object.Instantiate(tankPrefab);
        SetHealthSlider();
        tankView.SetTankController(this);
        Debug.Log("Tank Created", tankView);
    }
    
    public void EngineAudio()
    {
        // If there is no input (Tank Idle State)
        if(Mathf.Abs(tankView.leftJoystick.Vertical) < 0.1f && Mathf.Abs(tankView.leftJoystick.Horizontal) < 0.1f) 
        {
            // And if Driving audio is playing
            if(tankView.movementAudioSource.clip == tankView.engineDriving) 
            {   // Change the clip to Idle and play it
                tankView.movementAudioSource.clip = tankView.engineIdling;
                tankView.movementAudioSource.pitch = Random.Range(tankView.originalPitch - tankModel.pitchRange,
                                                                  tankView.originalPitch + tankModel.pitchRange);
                tankView.movementAudioSource.Play();
            }
        }
        else
        {
            // If tank is moving and idling clip is currently plating
            if(tankView.movementAudioSource.clip == tankView.engineIdling)
            {
                tankView.movementAudioSource.clip = tankView.engineDriving;
                tankView.movementAudioSource.pitch = Random.Range(tankView.originalPitch - tankModel.pitchRange, tankView.originalPitch + tankModel.pitchRange);
                tankView.movementAudioSource.Play();
            }
        }
    }

    public void MovePlayerTank()
    {
        // rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        Vector3 movement = tankView.transform.forward * (tankView.leftJoystick.Vertical * tankModel.speed * Time.deltaTime);
        // Applying movement on rigidbody
        tankView.tank_rb.MovePosition( tankView.tank_rb.position + movement);
    }

    public void TurnPlayerTank()
    {
        // Number of degrees to be turned 
        float turn = tankView.leftJoystick.Horizontal * tankModel.rotationRate * Time.deltaTime;
      
        // Make this into a Rotation
        Quaternion turnRotation = Quaternion.Euler(0f,turn,0f);
       
        // Applying rotation to the rigidbody
        tankView.tank_rb.MoveRotation(tankView.tank_rb.rotation * turnRotation);
    }

    public void RotatePlayerTankTurret()
    {
        Vector3 desiredRotation = Vector3.up * (tankView.rightJoystick.Horizontal * tankModel.turretRotationRate * Time.deltaTime);
        tankView.turret.transform.Rotate(desiredRotation, Space.Self);
    }

    public void TakeDamage(float damage)
    {
        tankModel.currentHealth -= damage;
        SetHealthSlider();  // Update the health bar
        if (tankModel.currentHealth - damage <= 0 && !tankModel.isDead)
        {   
            tankModel.isDead = true;
            tankView.PlayerDied();
        }
    }

    public void SetHealthSlider()
    {   
        // Set the value and color of the health slider.
        tankView.healthSlider.value = tankModel.currentHealth;
        tankView.fillImage.color = Color.Lerp(tankModel.zeroHealthColor, tankModel.fullHealthColor,
            tankModel.currentHealth / tankModel.maxHealth);
    }

    public void SetAimSlider()
    {
        tankView.aimSlider.value = tankModel.currentLaunchForce;
    }

    public void FireInputCheck()
    {
        tankView.aimSlider.value = tankModel.minLaunchForce;
        
        if (tankModel.currentLaunchForce >= tankModel.maxLaunchForce && !tankModel.isFired)
        {
            tankModel.currentLaunchForce = tankModel.maxLaunchForce;
            Fire();
        }
        else if (Input.GetMouseButtonDown(0))   // If pressed fire button for the first time
        {
            tankModel.isFired = false;
            tankModel.currentLaunchForce = tankModel.minLaunchForce;
            tankView.shootingAudioSource.clip = tankView.chargingClip;
            tankView.shootingAudioSource.Play();
        }
        else if (Input.GetMouseButton(0) && !tankModel.isFired)     // If holding fire button
        {
            tankModel.currentLaunchForce += tankModel.chargeSpeed * Time.deltaTime;
            tankView.aimSlider.value = tankModel.currentLaunchForce;
        }
        else if (Input.GetMouseButtonUp(0) && !tankModel.isFired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        tankModel.isFired = true;
        BulletService.Instance.FireBullet(tankModel.bulletType, tankView.fireTransform, tankModel.currentLaunchForce);
        tankView.shootingAudioSource.clip = tankView.firingClip;
        tankView.shootingAudioSource.Play();

        tankModel.currentLaunchForce = tankModel.minLaunchForce;
    }
}