using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected bool canMove;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotationSpeed;
    protected Vector2 moveDirection;

    [Header("Components")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;

    protected void Update()
    {
        if (canMove) rb.velocity = moveDirection * moveSpeed;
        
        if (moveDirection != Vector2.zero)
        {
            float rotationAngle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,rotationAngle), rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed);
        }
    }

    protected void Movement(float horizontalInput, float verticalInput)
    {
        if (canMove) rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);        
    }
}
