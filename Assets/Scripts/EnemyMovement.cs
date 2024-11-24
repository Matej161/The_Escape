using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public GameObject target;
    //public GameObject enemy;

    Rigidbody rb;

    //public float rotationSpeed = 5f;

    //public float moveSpeed;

    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        /*enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, moveSpeed);*/

        /*Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }*/
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        }
    }
}
