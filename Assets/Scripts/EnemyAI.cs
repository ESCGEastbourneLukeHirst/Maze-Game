using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Agent variables")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float seeingDistance;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [Tooltip("This variable is to determine how far away the enemy can wonder from the current centre point")]
    [SerializeField] private float walkRange;

    [Header("Search and Attack parameters")]
    [Tooltip("How far away can the enemy chase the player until giving up")]
    [SerializeField] private float stopChasingDistance;
    [SerializeField] private float attackDamage;
    [SerializeField] private float currentHealth;
    [SerializeField] private Transform seeingPoint;
    GameObject player;

    public enemyStates states;
    public enum enemyStates
    {
        walking,
        chasing,
        attacking
    }

    [SerializeField] bool isSeeingPlayer;

    [Header("Animator parameters")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource attackSound;

    private void Start()
    {
        SetAgentParameters();
    }

    private void Update()
    {
        CheckSeeingForPlayer();

        if (!isSeeingPlayer)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * walkRange;
                NavMeshHit hit;
                if(NavMesh.SamplePosition(randomPoint, out hit, 1, NavMesh.AllAreas))
                {
                    agent.destination = hit.position;
                }
            }
        }
        else if (isSeeingPlayer)
        {
            agent.destination = player.transform.position;

            if(agent.remainingDistance >= stopChasingDistance)
            {
                isSeeingPlayer = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            agent.speed = 0;
            agent.angularSpeed = 0;
            animator.SetTrigger("Death");
        }
    }

    void CheckSeeingForPlayer()
    {
        Collider[] hitCols = Physics.OverlapSphere(seeingPoint.transform.position, seeingDistance);

        foreach(Collider col in hitCols)
        {
            if (col.tag == "Player")
            {
                print("Seeing player");
                isSeeingPlayer = true;
                player = col.gameObject;
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(seeingPoint.transform.position, seeingDistance);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    void Attack()
    {
        Collider[] playerCol = Physics.OverlapSphere(seeingPoint.transform.position, 2f);

        foreach (Collider col in playerCol)
        {
            if (col.tag == "Player")
            {
                col.transform.GetComponent<PlayerMovement>().TakeDamage(attackDamage);
            }
        }
    }

    public void PlayAttackAudio()
    {
        attackSound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            animator.SetBool("Attacking", false);
        }
    }

    void SetAgentParameters()
    {
        agent.speed = speed;
        agent.angularSpeed = turnSpeed;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
