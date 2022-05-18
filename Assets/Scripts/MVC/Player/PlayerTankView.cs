using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
 public class PlayerTankView : MonoBehaviour
 {
     public GameObject turret;
     public GameObject explosionEffectPrefab;
     public Slider healthSlider;
     public Image fillImage;
     public AudioClip engineIdling, engineDriving;
     public AudioSource movementAudio;
     public Joystick rightJoystick, leftJoystick;
     public Camera cam;
     
     [HideInInspector] public float originalPitch = 0.2f;
     [HideInInspector] public AudioSource explosionSound;
     [HideInInspector] public ParticleSystem explosionParticles;
     [HideInInspector] public Rigidbody tank_rb;             // Rigidbody reference

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
         cam = Camera.main;
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
             FixedUpdateTankController();
         }
     }

     private void FixedUpdateTankController()     //calling this func in fixed update 
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
     public void SetTankControllerReference(PlayerTankController controller)
     {
         _tankController = controller;
     }
     public void Death()
     {
         Destroy(gameObject);
     }

     public void TakeDamage(int damage)
     {
         _tankController.TakeDamage(damage);
     }
 }
