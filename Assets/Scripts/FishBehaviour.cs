using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{

    public GameObject[] waypoints;
    int currentWaypoint =0;
    public float waypointRadius;

    public float Speed = 3;

   public float rotSpeed;

    public float degreesPerSecond;
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
        

        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position)
          < waypointRadius)
        {

            currentWaypoint = Random.Range(0, waypoints.Length);
            // currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position,
              waypoints[currentWaypoint].transform.position, Time.deltaTime
              * Speed);

        // transform.LookAt(waypoints[currentWaypoint].transform.position);



        /*  Vector3 directionFromMeToTarget = waypoints[currentWaypoint].transform.position
              - transform.position;
          directionFromMeToTarget.x = 0.0f;
          Quaternion lookRotation = Quaternion.LookRotation(directionFromMeToTarget);

          transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation,
              Time.deltaTime * degreesPerSecond / 360.0f);
        */
        rotSpeed = Speed * Random.Range(1f, 3f);

        Vector3 lookAt = waypoints[currentWaypoint].transform.position
            - this.transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
            rotSpeed * Time.deltaTime);

        
    }
}
