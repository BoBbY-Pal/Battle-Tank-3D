using UnityEngine;
public class PlayerTankController 
{
    public PlayerTankModel playerTankModel { get; }
    public  PlayerTankView tankView { get; }
    
    public float speed = 10f;           // Movement speed
    public float turnSpeed = 150f;     // Turning speed
    public AudioClip engineIdling, engineDriving;
    public AudioSource movementAudio;
    public float pitchRange = 0.2f;
    
    private float originalPitch;
    private Rigidbody tank_rb;             // Rigidbody reference
    private float horizontalMov;     // for storing Horizontal movement inputs
    private float verticalMov;      // for storing vertical movement inputs
    private Joystick _leftJoystick;   // For rotating a tank
    private Joystick _rightJoystick;    // For moving tank forward & backward
    private Camera _camera;
    

    public PlayerTankController(PlayerTankModel playerTankModel, PlayerTankView tankPrefab)
    {
        this.playerTankModel = playerTankModel;
        tankView = GameObject.Instantiate<PlayerTankView>(tankPrefab);
        Debug.Log("Tank Created", tankView);
    }
    protected void Awake()
    {
        base.Awake();
        //Custom Awake
        tank_rb = GetComponent<Rigidbody>();
        
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
    private void Update()
    {
        horizontalMov = horizontalJoystick.Horizontal;
        verticalMov = verticalJoystick.Vertical;
        EngineAudio();
    }

    private void EngineAudio()
    {
        // If there is no input (Tank Idle State)
        if(Mathf.Abs(verticalMov) < 0.1f && Mathf.Abs(horizontalMov) < 0.1f) 
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
    private void FixedUpdate()
    {
        MovePlayer();
        Turn();
    }

    private void MovePlayer()
    {
        // rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
        Vector3 movement = transform.forward * (verticalMov * speed * Time.deltaTime);
        // Applying movement on rigidbody
        tank_rb.MovePosition( tank_rb.position + movement);
    }

    void Turn()
    {
        // Number of degrees to be turned 
        float turn = horizontalMov * turnSpeed * Time.deltaTime;
      
        // Make this into a Rotation
        Quaternion turnRotation = Quaternion.Euler(0f,turn,0f);
       
        // Applying rotation to the rigidbody
        tank_rb.MoveRotation(tank_rb.rotation * turnRotation);
    }
}