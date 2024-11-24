using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    public float rotationSpeed = 1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //agent.angularSpeed = 360f;
        //agent.stoppingDistance = 5f;
        //agent.acceleration = 15f;
    }

    void Update()
    {
        agent.destination = target.position;

        Vector3 velocity = agent.velocity;

        if (velocity.magnitude > 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
