using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{

    public GameObject[] waypoints;
    int currentWaypoint =0;
    public float waypointRadius;

    public float Speed = 3;
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
        currentWaypoint = Random.Range(0, waypoints.Length);

        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position)
          < waypointRadius)
        {
           // currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position,
              waypoints[currentWaypoint].transform.position, Time.deltaTime
              * Speed);
    }
}
