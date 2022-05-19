using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
 public class PlayerTankView : MonoBehaviour
 {
     public GameObject turret;
     public GameObject explosionEffectPrefab;
     
     // Aim UI
     public Slider aimSlider;
     public Transform fireTransform;
     
     // Health UI
     public Slider healthSlider;
     public Image fillImage;
     
     public AudioClip engineIdling, engineDriving;
     public AudioSource movementAudio;

     [HideInInspector] public float originalPitch = 0.2f;
     [HideInInspector] public AudioSource explosionSound;
     [HideInInspector] public ParticleSystem explosionParticles;
     [HideInInspector] public Rigidbody tank_rb;             // Rigidbody reference
     [HideInInspector] public Joystick rightJoystick, leftJoystick;

     private PlayerTankController _tankController;
     

     private void Awake()
     {
         // Store the original pitch of the audio source
         originalPitch = movementAudio.pitch;
         tank_rb = GetComponent<Rigidbody>();
         
         explosionParticles = Instantiate(explosionEffectPrefab.GetComponent<ParticleSystem>());
         explosionSound = explosionParticles.GetComponent<AudioSource>();
         explosionParticles.gameObject.SetActive(false);
     }


     private void Start()
     {
         Joystick[] joys = FindObjectsOfType<Joystick>();
         rightJoystick = joys[1];
         leftJoystick = joys[0];
     }

     private void Update()
     {
         _tankController.EngineAudio();
     }

     private void FixedUpdate()
     {
         if (_tankController != null)
         {
             MovementController();
         }
     }
     
     private void MovementController()     //calling this func in fixed update 
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
         // Playing the effects on the death of the tank and destroying it.
         explosionParticles.transform.position = transform.position;
         explosionParticles.gameObject.SetActive((true));
         explosionParticles.Play();
         explosionSound.Play(); 
         
         //  Destroy the object
         Destroy(gameObject);
     }

     public void TakeDamage(int damage)
     {
         _tankController.TakeDamage(damage);
     }
 }
