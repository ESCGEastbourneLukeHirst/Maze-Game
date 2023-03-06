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
    [SerializeField] private float seeingDistance;
    [SerializeField] private float attackDamage;
    [SerializeField] private float currentHealth;
    public Vector3 cubeVectors;

    public enemyStates states;
    public enum enemyStates
    {
        walking,
        chasing,
        attacking
    }

    [SerializeField] bool isSeeingPlayer;

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
                agent.destination = hit.collider.transform.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.forward * seeingDistance, cubeVectors);
    }

    void SetAgentParameters()
    {
        agent.speed = speed;
        agent.angularSpeed = turnSpeed;
    }
}
