using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class BaseNpcController : MonoBehaviour
{
    [Header("Basic Stats")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float timeToBeingCaught;
    bool destroyObject;
    float currentTime;
    bool canMove;

    [Header("Movement variables")]
    [SerializeField] Transform[] walkPoints;
    int currentPoint;
    [SerializeField] float waitForPoints;
    bool switchDestiny;
    Transform destiny;
    bool isTriggering;

    [Header("External components")]
    [SerializeField] GameObject mainGroup;
    [SerializeField] GameObject followSpirit;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (walkPoints.Length == 0) return;

        currentPoint = 0;
        transform.position = walkPoints[currentPoint].position;

        StartCoroutine(NewDestination());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0 && !isTriggering) currentTime = Mathf.Max(currentTime - Time.deltaTime, 0);
        
        if (walkPoints != null && destiny != null) 
        {
            if (Vector2.Distance(destiny.position, transform.position) <= 0.3f && switchDestiny)
            {
                switchDestiny = false;
                canMove = false;
                StartCoroutine(NewDestination());
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

    // Rotina para mudar trajetória do NPC
    IEnumerator NewDestination()
    {
        yield return new WaitForSeconds(waitForPoints);
        currentPoint++;
        if (currentPoint >= walkPoints.Length) currentPoint = 0;
        destiny = walkPoints[currentPoint];
        canMove = true;
        switchDestiny = true;
    }

    #region catch sequence actions
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isTriggering) isTriggering = true;

            if (currentTime < timeToBeingCaught)
            {
                currentTime = Mathf.Min(currentTime + Time.deltaTime, timeToBeingCaught);
            }
            else
            {
                if (!destroyObject)
                {
                    destroyObject = true;
                    Instantiate(followSpirit, transform.position, Quaternion.identity);
                    Destroy(mainGroup);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriggering = false;
        }
    }

    IEnumerator DecrementCatch()
    {
        for (float time = 0; time < timeToBeingCaught; time += .01f)
        {

            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator CatchSpirit()
    {
        for (float time = 0; time < timeToBeingCaught; time += .01f)
        {

            yield return new WaitForSeconds(.01f);
        }
        Instantiate(followSpirit, transform.position, Quaternion.identity);
        Destroy(mainGroup);
    }
    #endregion
}
