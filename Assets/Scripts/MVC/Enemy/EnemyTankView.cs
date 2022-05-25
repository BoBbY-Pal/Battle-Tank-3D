using Enums;
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
    
    public Patrolling patrollingState;
    public Chasing chasingState;
    public Attacking attackingState;
    
    [SerializeField] private EnemyState initialState;
    [HideInInspector] public EnemyState activeState;
    [HideInInspector] public StateManager currentState;

    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public ParticleSystem explosionParticles;
    [HideInInspector] public AudioSource explosionAudio;

    private void Awake()
    {
        explosionParticles = Instantiate(explosionPrefab.GetComponent<ParticleSystem>());
        explosionAudio = explosionParticles.GetComponent<AudioSource>();
        explosionParticles.gameObject.SetActive(false);
    }

    private void Start()
    {
        _tankController.SetHealthUI();
        if (PlayerTankService.Instance.tankView)
        {
            playerTransform = PlayerTankService.Instance.tankView.transform;
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        SetEnemyTankMaterial();
        InitializeStates();
    }

    private void InitializeStates()
    {
        switch(initialState)
        {
            case EnemyState.Patrolling:
            {
                currentState = patrollingState;
                break;
            }
            case EnemyState.Chasing:
            {
                currentState = chasingState;
                break;
            }
            case EnemyState.Attacking:
            {
                currentState = attackingState;
                break;
            }
            default:
            {
                currentState = null;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        _tankController.UpdateTankController();
    }

    public void TakeDamage(float damage)
    {
        _tankController.TakeDamage(damage);
    }
    public void Death()
    {
        _tankController.SetDeathTrue();
        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();
        explosionAudio.Play();
        Destroy(gameObject);
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
