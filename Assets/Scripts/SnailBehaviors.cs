using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailBehaviors : MonoBehaviour
{
    public GameObject[] waypoints;
    public float waypointRadius = 1f;
    int currentWaypoint = 0;

    public float speed = 1f;

    public float rotationSpeed = 1f;
    public float degreesPerSecond = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if(Vector3.Distance(waypoints[currentWaypoint].transform.position,
            transform.position) < waypointRadius)
        {
            currentWaypoint = Random.Range(0, waypoints.Length);
            if(currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position,
            waypoints[currentWaypoint].transform.position, Time.deltaTime *
            speed);

        rotationSpeed = speed * Random.Range(1f, 3f);
        Vector3 lookAt = waypoints[currentWaypoint].transform.position -
            transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(lookAt), rotationSpeed * Time.deltaTime);
    }
}
