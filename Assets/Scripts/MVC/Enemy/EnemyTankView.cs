﻿using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyTankView : MonoBehaviour, IDamageable
{
    private EnemyTankController _tankController;

    public Slider healthSlider;
    public Image fillImage;

    public NavMeshAgent navMeshAgent;
    public Transform fireTransform, turret;
    public AudioSource shootingAudio;
    public AudioClip fireClip;
    public GameObject explosionPrefab;
    public LayerMask playerLayerMask, groundLayerMask;
    
    public PatrollingState patrollingState;
    public ChasingState chasingState;
    public AttackingState attackingState;
    
    [SerializeField] private EnemyState initialState;
    [HideInInspector] public EnemyState activeState;
    [HideInInspector] public EnemyStateBase currentEnemyState;

    [HideInInspector] public Transform playerTransform;

    private void Start()
    {
        _tankController.SetHealthUI();
        if (PlayerTankService.Instance.tankViewPrefab)
        {
            playerTransform = PlayerTankService.Instance.player.GetPlayerTransform();
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        SetEnemyTankMaterial();
        InitializeStates();
        
        CameraController.Instance.AddTargetPosition(this.transform);
    }

    private void FixedUpdate()
    {
        _tankController.UpdateTankController();
    }
    
    private void InitializeStates()
    {
        switch(initialState)
        {
            case EnemyState.Patrolling:
            {
                currentEnemyState = patrollingState;
                break;
            }
            case EnemyState.Chasing:
            {
                currentEnemyState = chasingState;
                break;
            }
            case EnemyState.Attacking:
            {
                currentEnemyState = attackingState;
                break;
            }
            default:
            {
                currentEnemyState = null;
                break;
            }
        }

        currentEnemyState.OnStateEnter();
    }


    public void TakeDamage(float damage)
    {
        _tankController.TakeDamage(damage);
    }
    public void Death()
    {
        _tankController.SetDeathTrue();
        
        // Spawning the explosion particles at the position of enemy when dies.
        ParticleSystem explosionParticles = Instantiate(explosionPrefab.GetComponent<ParticleSystem>());
        AudioSource explosionAudio = explosionParticles.GetComponent<AudioSource>();
        explosionParticles.transform.position = transform.position;
        
        // Playing the effects on the death of the enemy and destroying tank.
        explosionParticles.Play();
        explosionAudio.Play();
        
        //  Destroy the tank object.
        Destroy(gameObject);
        
        float waitTime = Mathf.Max(explosionParticles.main.duration,
                                    explosionAudio.clip.length);
        Destroy(explosionParticles.gameObject, waitTime);
        
        CameraController.Instance.RemoveTargetPosition(this.transform);
    }
    
    private void SetEnemyTankMaterial()
    {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var t in meshRenderers)
        {
            t.material.color = _tankController.GetTankColor();
        }
    }
    
    public void SetController(EnemyTankController controller) => _tankController = controller;
}
