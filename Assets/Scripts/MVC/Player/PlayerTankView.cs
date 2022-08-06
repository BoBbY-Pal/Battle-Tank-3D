using Interfaces;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
 public class PlayerTankView : MonoBehaviour, IDamageable
 {
     public GameObject turret;
     public GameObject explosionEffectPrefab;
     
     // Aim UI
     public Slider aimSlider;
     public Transform fireTransform;
     
     // Health UI
     public Slider healthSlider;
     public Image fillImage;

     public AudioSource movementAudioSource;
     public AudioClip engineIdling, engineDriving;

     [HideInInspector] public float originalPitch;     // The pitch of the audio source at the start of the scene.
     
     [HideInInspector] public Rigidbody tank_rb;             // Rigidbody reference
     [HideInInspector] public Joystick rightJoystick, leftJoystick;

     private PlayerTankController _tankController;

     

     private void Awake()
     {
         originalPitch = movementAudioSource.pitch;                          
         tank_rb = GetComponent<Rigidbody>();

     }
     
     private void Start()
     {
         Joystick[] joys = FindObjectsOfType<Joystick>();
         rightJoystick = joys[1];
         leftJoystick = joys[0];
         _tankController.SetAimSlider();
         _tankController.SetHealthSlider();
         
         CameraController.Instance.AddTargetPosition(this.transform);
     }
     
     private void Update()
     {
         
         if (_tankController != null)
         {
             _tankController.EngineAudio();
             _tankController.FireInputCheck();
             
             
         }
     }

     private void FixedUpdate()
     {
         if (_tankController != null)
         {
             MovementController();
         }
     }
     
     private void MovementController()     // Handles all the tank related movements
     {
         if (tank_rb)
         {
             if (leftJoystick.Vertical != 0)
             {
                 _tankController.MovePlayerTank();
             }

             if (leftJoystick.Horizontal != 0)
             {
                 _tankController.TurnPlayerTank();
             }
                
         }

         if (turret)
         {
             if (rightJoystick.Horizontal != 0)
             {
                 _tankController.RotatePlayerTankTurret();
             }
         }
     }
     public void SetTankController(PlayerTankController controller) => _tankController = controller;
  
     public void PlayerDied()
     {
         _tankController.SetDeathTrue();
     
         // Spawning the explosion particles when player dying at the position of tank.
         ParticleSystem explosionParticles = Instantiate(explosionEffectPrefab.GetComponent<ParticleSystem>());
         AudioSource explosionAudio = explosionParticles.GetComponent<AudioSource>();
         explosionParticles.gameObject.transform.position = transform.position;

         // Playing the effects on the death of the player and destroying the tank.
         explosionParticles.Play();
         explosionAudio.Play(); 
         
         //  Destroy the tank object.
         Destroy(gameObject);
         
         // When particle effect finishes or audio clip ends destroy the particle object.
         float waitTime = Mathf.Max(explosionParticles.main.duration,
                                    explosionAudio.clip.length);
         Destroy(explosionParticles.gameObject, waitTime);
         
         // Adjusting camera 
         CameraController.Instance.RemoveTargetPosition(this.transform);
         CameraController.Instance.SetCameraWithEndTargets();
         
        
     }

     public void TakeDamage(float damage)
     {
         _tankController.TakeDamage(damage);
     }
     
 }
