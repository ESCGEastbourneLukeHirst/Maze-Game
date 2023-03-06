using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Agent variables")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [Tooltip("This variable is to determine how far away the enemy can wonder from the current centre point")]
    [SerializeField] private float walkRange;

    [Header("Search and Attack parameters")]
    [Tooltip("How far away can the enemy see the player from")]
    [SerializeField] private float seeingDistance;
    [Tooltip("How far away can the enemy chase the player until giving up")]
    [SerializeField] private float stopChasingDistance;
    [SerializeField] private float attackDamage;
    [SerializeField] private float currentHealth;
    [Tooltip("This changes the shape on the wire cube gizmos of the enemy sight")]
    public Vector3 cubeVectors;
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
            Destroy(gameObject);
        }
    }

    void CheckSeeingForPlayer()
    {
        RaycastHit hit;
        Physics.BoxCast(transform.position, cubeVectors, transform.forward, out hit, transform.localRotation, seeingDistance);

        if (hit.collider != null)
        {
            if(hit.collider.tag == "Player")
            {
                isSeeingPlayer = true;
                player = hit.collider.gameObject;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.forward * seeingDistance, cubeVectors);
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
}
