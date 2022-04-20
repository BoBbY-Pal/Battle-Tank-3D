using UnityEngine;
public class PlayerTankController : MonoSingletonGeneric<PlayerTankController>  
{
    public float speed = 10f;           // Movement speed
    public float turnSpeed = 150f;     // Turning speed
    public AudioClip engineIdling, engineDriving;
    public AudioSource movementAudio;
    public float pitchRange = 0.2f;
    
    private float originalPitch;
    private Rigidbody rb;             // Rigidbody reference
    private float horizontalMov;     // for storing Horizontal movement inputs
    private float verticalMov;      // for storing vertical movement inputs
    [SerializeField] private Joystick horizontalJoystick;   // For rotating a tank
    [SerializeField] private Joystick verticalJoystick;    // For moving tank forward & backward
    protected override void Awake()
    {
        base.Awake();
        //Custom Awake
        rb = GetComponent<Rigidbody>();
        
        // Store the original pitch of the audio source
        originalPitch = movementAudio.pitch;
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
        rb.MovePosition(rb.position + movement);
    }

    void Turn()
    {
        // Number of degrees to be turned 
        float turn = horizontalMov * turnSpeed * Time.deltaTime;
      
        // Make this into a Rotation
        Quaternion turnRotation = Quaternion.Euler(0f,turn,0f);
       
        // Applying rotation to the rigidbody
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}