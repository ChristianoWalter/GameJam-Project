using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    [Header("Stats")]
    public bool canMove;
    [SerializeField] protected bool isMoving;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotationSpeed;
    [HideInInspector] public Vector2 moveDirection;

    [Header("Components")]
    public Animator anim;
    public Rigidbody2D rb;

    protected virtual void Start()
    {
        canMove = true;
        isMoving = false;
    }

    protected virtual void Update()
    {
        if (canMove)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
        
        if (moveDirection != Vector2.zero)
        {
            float rotationAngle = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,rotationAngle), rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed);
        }

        isMoving = Mathf.Abs(rb.velocity.x) > 0.01f || Mathf.Abs(rb.velocity.y) > 0.01f;

        if (anim != null)
        {
            anim.SetBool("IsMoving", isMoving);
        }
    }

    protected void Movement(float horizontalInput, float verticalInput)
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
        }
    }
    
}
