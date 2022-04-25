using UnityEngine;
public class PlayerTankController 
{
    private PlayerTankModel tankModel { get; }
    private PlayerTankView tankView { get; }
    
    
    public AudioClip engineIdling, engineDriving;
    public AudioSource movementAudio;
    public float pitchRange = 0.2f;
    private float originalPitch;
    
    
    private Rigidbody tank_rb;             // Rigidbody reference
    private Joystick _leftJoystick;   // For rotating a tank
    private Joystick _rightJoystick;    // For moving tank forward & backward
    private Camera _camera;
    

    public PlayerTankController(PlayerTankModel tankModel, PlayerTankView tankPrefab)
    {
        this.tankModel = tankModel;
        tankView = GameObject.Instantiate<PlayerTankView>(tankPrefab);
        tank_rb = tankView.GetComponent<Rigidbody>();
        tankView.SetTankControllerReference(this);
        Debug.Log("Tank Created", tankView);
    }
    protected void Awake()
    {

        // Store the original pitch of the audio source
        originalPitch = movementAudio.pitch;
    }

    public void SetJoystickReference(Joystick rightJoystickRef, Joystick leftJoystickRef)
    {
        _rightJoystick = rightJoystickRef;
        _leftJoystick = leftJoystickRef;
    }

    public void SetCameraReference(Camera cameraRef)
    {
        _camera = cameraRef;
        _camera.transform.SetParent(tankView.turret.transform);
    }
    
    public void FixedUpdateTankController()     //calling this func in fixed update from Tank Service
    {
        if (tank_rb)
        {
            if (_leftJoystick.Vertical != 0)
            {
                MovePlayerTank();
            }

            if (_leftJoystick.Horizontal != 0)
            {
                TurnPlayerTank();
            }
            EngineAudio();    
        }

        if (tankView.turret)
        {
            if (_rightJoystick.Horizontal != 0)
            {
                RotatePlayerTankTurret();
            }
        }
    }

    private void EngineAudio()
    {
        // If there is no input (Tank Idle State)
        if(Mathf.Abs(_leftJoystick.Vertical) < 0.1f && Mathf.Abs(_leftJoystick.Horizontal) < 0.1f) 
        {
            // And if Driving audio is playing
            if(movementAudio.clip == engineDriving)
            {   // Change the clip to Idle and play it
                movementAudio.clip = engineIdling;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
        else
        {
            // If tank is moving and idling clip is currently plating
            if(movementAudio.clip == engineIdling)
            {
                movementAudio.clip = engineDriving;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
    }

    private void MovePlayerTank()
    {
        // rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        Vector3 movement = tank_rb.transform.forward * (_leftJoystick.Vertical * tankModel.speed * Time.deltaTime);
        // Applying movement on rigidbody
        tank_rb.MovePosition( tank_rb.position + movement);
    }

    private void TurnPlayerTank()
    {
        // Number of degrees to be turned 
        float turn = _leftJoystick.Horizontal * tankModel.rotationRate * Time.deltaTime;
      
        // Make this into a Rotation
        Quaternion turnRotation = Quaternion.Euler(0f,turn,0f);
       
        // Applying rotation to the rigidbody
        tank_rb.MoveRotation(tank_rb.rotation * turnRotation);
    }

    private void RotatePlayerTankTurret()
    {
        Vector3 desiredRotation = Vector3.up * _rightJoystick.Horizontal * tankModel.turretRotationRate * Time.deltaTime;
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

    public void SetHealthUI()
    {
        tankView.healthSlider.value = tankModel.health;
        tankView.fillImage.color = Color.Lerp(tankModel.zeroHealthColor, tankModel.fullHealthColor,
            tankModel.health / tankModel.maxHealth);
    }
}