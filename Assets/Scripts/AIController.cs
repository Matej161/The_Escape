using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AIState
{
    Patrolling,
    Chasing,
    Investigating,
    Returning
}

public class AIController : MonoBehaviour
{
    public AIState currentState = AIState.Patrolling;

    public Transform[] patrolPoints; // Set patrol waypoints in Inspector
    public Transform player;
    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;

    private Vector3 lastSeenPlayerPos;

    private float investigationTimer = 3f; // Timer for investigation state
    private float investigationDuration = 3f; // Duration to investigate in seconds

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Patrolling:
                Patrol();
                break;
            case AIState.Chasing:
                Chase();
                break;
            case AIState.Investigating:
                Investigate();
                break;
            case AIState.Returning:
                ReturnToPatrol();
                break;
        }

        CheckForPlayer();

        //upravit ten uhel toho jak to AI vidi
    }

    void Patrol()
    {
        agent.speed = 8;
        if (agent.remainingDistance < 0.5f) // Reached patrol point
        { 
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Chase()
    {
        agent.speed = 12;
        agent.SetDestination(player.position);
    }

    void Investigate()
    {
        agent.speed = 8;
        agent.SetDestination(lastSeenPlayerPos);

        if (agent.remainingDistance < 0.5f)
        {
            investigationTimer -= Time.deltaTime;

            if (investigationTimer <= 0f)
            {
                // Investigation time is up, return to patrol
                currentState = AIState.Patrolling;
            }
        }
    }

    void ReturnToPatrol()
    {
        agent.speed = 8;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        if (agent.remainingDistance < 0.5f)
        {
            currentState = AIState.Patrolling;
        }
    }

    void CheckForPlayer()
    {
        agent.speed = 8;
        if (CanSeePlayer())
        {
            lastSeenPlayerPos = player.position;
            currentState = AIState.Chasing;
        }
        else if (currentState == AIState.Chasing)
        {
            currentState = AIState.Investigating;
            investigationTimer = investigationDuration; // Reset the timer
        }
    }

    /*private bool CanSeePlayer()
    {
        // Direction and distance to player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Raycast starting point (slightly above AI's position)
        Vector3 rayStart = transform.position + Vector3.up * 1.5f;

        // LayerMask to interact with Player and Obstacles
        int layerMask = LayerMask.GetMask("Player", "Obstacles");

        // Check Field of View
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer < 60f) // 60-degree FOV
        {
            // Raycast for line of sight
            if (Physics.Raycast(rayStart, directionToPlayer, out RaycastHit hit, distanceToPlayer, layerMask))
            {
                Debug.DrawRay(rayStart, directionToPlayer * distanceToPlayer, Color.green); // Success
                if (hit.collider.CompareTag("Player")) // Only detect player
                {
                    return true;
                }
            }
        }
        Debug.DrawRay(rayStart, directionToPlayer * distanceToPlayer, Color.red); // Failure
        return false;
    }*/

    private bool CanSeePlayer()
    {
        // Start the ray slightly above the AI to avoid clipping
        agent.speed = 8;
        Vector3 rayStart = transform.position + Vector3.up * 1.5f;
        Vector3 directionToPlayer = (player.position - rayStart).normalized;
        float distanceToPlayer = Vector3.Distance(rayStart, player.position);

        // Create a LayerMask for Player and Obstacles
        int layerMask = LayerMask.GetMask("Player", "Obstacles");

        Debug.Log("LayerMask: " + layerMask);

        // Cast the ray
        if (Physics.Raycast(rayStart, directionToPlayer, out RaycastHit hit, distanceToPlayer, layerMask))
        {
            Debug.DrawRay(rayStart, directionToPlayer * distanceToPlayer, Color.green);

            // Check if the ray hit the Player
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        Debug.DrawRay(rayStart, directionToPlayer * distanceToPlayer, Color.red); // Visualize failure
        return false;
    }
}
