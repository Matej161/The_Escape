using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        }
    }
}
