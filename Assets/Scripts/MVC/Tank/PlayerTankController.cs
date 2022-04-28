using UnityEngine;
public class PlayerTankController 
{
    private PlayerTankModel tankModel { get; }
    private PlayerTankView tankView { get; }
    
    
    
    public float pitchRange = 0.2f;
    
    
    
   
    // private Joystick _leftJoystick;   // For rotating a tank
    // private Joystick _rightJoystick;    // For moving tank forward & backward
    // private Camera _camera;
    //

    public PlayerTankController(PlayerTankModel tankModel, PlayerTankView tankPrefab)
    {
        this.tankModel = tankModel;
        tankView = Object.Instantiate(tankPrefab);
        SetHealthUI();
        tankView.SetTankControllerReference(this);
        Debug.Log("Tank Created", tankView);
    }



    public void EngineAudio()
    {
        // If there is no input (Tank Idle State)
        if(Mathf.Abs(tankView.leftJoystick.Vertical) < 0.1f && Mathf.Abs(tankView.leftJoystick.Horizontal) < 0.1f) 
        {
            // And if Driving audio is playing
            if(tankView.movementAudio.clip == tankView.engineDriving) 
            {   // Change the clip to Idle and play it
                tankView.movementAudio.clip = tankView.engineIdling;
                tankView.movementAudio.pitch = Random.Range(tankView.originalPitch - pitchRange, tankView.originalPitch + pitchRange);
                tankView.movementAudio.Play();
            }
        }
        else
        {
            // If tank is moving and idling clip is currently plating
            if(tankView.movementAudio.clip == tankView.engineIdling)
            {
                tankView.movementAudio.clip = tankView.engineDriving;
                tankView.movementAudio.pitch = Random.Range(tankView.originalPitch - pitchRange, tankView.originalPitch + pitchRange);
                tankView.movementAudio.Play();
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
        Vector3 desiredRotation = Vector3.up * tankView.rightJoystick.Horizontal * tankModel.turretRotationRate * Time.deltaTime;
        tankView.turret.transform.Rotate(desiredRotation, Space.Self);
    }

    public void TakeDamage(int damage)
    {
        tankModel.health -= damage;
        SetHealthUI();  // Update the health bar
        if (tankModel.health - damage <= 0 && !tankModel.isDead)
        {
            Death();
        }
    }

    private void Death()
    {
        tankModel.isDead = true;
        tankView.explosionParticles.transform.position = tankView.transform.position;
        tankView.explosionParticles.gameObject.SetActive((true));
        tankView.explosionParticles.Play();
        tankView.explosionSound.Play();
        tankView.Death();   //  Destroy the object
    }

    private void SetHealthUI()
    {
        tankView.healthSlider.value = tankModel.health;
        tankView.fillImage.color = Color.Lerp(tankModel.zeroHealthColor, tankModel.fullHealthColor,
            tankModel.health / tankModel.maxHealth);
    }
}