using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
 public class PlayerTankView : MonoBehaviour
 {
     public GameObject turret;
     public GameObject explosionEffectPrefab;
     public Slider healthSlider;
     public Image fillImage;

     [HideInInspector] public AudioSource explosionSound;
     [HideInInspector] public ParticleSystem explosionParticles;

     private PlayerTankController tankController;

     private void Awake()
     {
         explosionParticles = Instantiate(explosionEffectPrefab.GetComponent<ParticleSystem>());
         explosionSound = explosionParticles.GetComponent<AudioSource>();
         explosionParticles.gameObject.SetActive(false);
     }

     private void Start()
     {
         tankController.SetHealthUI();
     }

     public void Death()
     {
         Destroy(gameObject);
     }

     public void TakeDamage()
     {
         tankController.TakeDamage();
     }
 }
