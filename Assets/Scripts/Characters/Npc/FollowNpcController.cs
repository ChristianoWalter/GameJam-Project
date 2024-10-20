using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNpcController : MonoBehaviour
{
    [Header("Basic Stats")]
    float moveSpeed;
    [SerializeField] float fixedSpeed;
    [SerializeField] float rotationSpeed;
    bool canMove;

    [Header("Movement variables")]
    Transform destiny;
    [SerializeField] float distanceToPlayer;
    bool decrementingSpeed;

    private void Awake()
    {
        //destiny = PlayerController.instance.transform;
    }

    private void Start()
    {
        PlayerController.instance.AddSpirits(gameObject);
        destiny = PlayerController.instance.transform;
        canMove = true;
    }

    void Update()
    {
        if (Vector2.Distance(destiny.position, transform.position) <= distanceToPlayer)
        {
            if (moveSpeed > 0)
            {
                if (!decrementingSpeed)
                {
                    moveSpeed = 2;
                    decrementingSpeed = true;
                }
                moveSpeed = Mathf.Max(moveSpeed - Time.deltaTime * 2, 0);
            }
        }
        else
        {
            decrementingSpeed = false;
            if (moveSpeed < fixedSpeed)
            {
                moveSpeed = Mathf.Min(moveSpeed + Time.deltaTime * 4, fixedSpeed);
            }
        }

        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, destiny.position, moveSpeed * Time.deltaTime);

            Vector2 direction = transform.position - destiny.position;
            direction.Normalize();
            float rotationAngle = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotationAngle), rotationSpeed);
        }
    }

    
}
