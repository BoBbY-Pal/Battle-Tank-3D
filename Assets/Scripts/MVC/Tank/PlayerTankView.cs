using System;
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

     private PlayerTankController tankController;
     
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
         rightJoystick = joys[0];
         leftJoystick = joys[1];
     }

     private void Update()
     {
         tankController.EngineAudio();
     }

     private void FixedUpdate()
     {
         if (tankController != null)
         {
             FixedUpdateTankController();
         }
     }

     private void FixedUpdateTankController()     //calling this func in fixed update from Tank Service
     {
         if (tank_rb)
         {
             if (leftJoystick.Vertical != 0)
             {
                 tankController.MovePlayerTank();
             }

             if (leftJoystick.Horizontal != 0)
             {
                 tankController.TurnPlayerTank();
             }
             // EngineAudio();    
         }

         if (turret)
         {
             if (rightJoystick.Horizontal != 0)
             {
                 tankController.RotatePlayerTankTurret();
             }
         }
     }
     public void SetTankControllerReference(PlayerTankController controller)
     {
         tankController = controller;
     }
     public void Death()
     {
         Destroy(gameObject);
     }

     public void TakeDamage(int damage)
     {
         tankController.TakeDamage(damage);
     }
 }
