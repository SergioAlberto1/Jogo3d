using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Inimigo : MonoBehaviour, IDamageable
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Linha path;

    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float AlturaOlhos;
    public float maxHealth = 1f;
    private float currentHealth;

    [Header("Arma")]
    public Transform Cano;
    [Range(0.1f,10)]
    public float TaxaTiro;

    [SerializeField]
    private string currentState;
    MonsterSpawner Spawner;
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
    }

    void Update()
    {
        PodeVerPlayer();
        currentState = stateMachine.activeState.ToString();
    }
    //verifica se o inimigo pode ver o jogador
    public bool PodeVerPlayer()
    {
        if (player != null)
        {
            //o jogador está perto o suficiente para ser visto
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * AlturaOlhos);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * AlturaOlhos), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    
    public void SetSpawner(MonsterSpawner _spawner)
    {
        Spawner = _spawner;
    }
    //aplicar dano ao inimigo
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //lidar com a morte do inimigo
    void Die()
    {
        if (Spawner != null) Spawner.currentMonster.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
