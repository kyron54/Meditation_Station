using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerBehaviour : MonoBehaviour
{

    public GameObject[] waypoints;
    int currentWaypoint = 0;
    public float waypointRadius;

    public GameObject player;

    public float Speed = 3;

    public float rotSpeed;

    public float degreesPerSecond;

    public int numofWaypoints = 0;

    public float resetTime = 10.0f;

    private Animator anim;
    public int waypointtoStop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartReset()
    {
        yield return new WaitForSeconds(resetTime);

        numofWaypoints = 0;
    }

    public void Movement()
    {


        if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position)
          < waypointRadius)
        {
            numofWaypoints++;
            currentWaypoint = Random.Range(0, waypoints.Length);

            waypointtoStop = Random.Range(1, 16);

            // currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }


        if (numofWaypoints <= waypointtoStop)
        {
            transform.position = Vector3.MoveTowards(transform.position,
             waypoints[currentWaypoint].transform.position, Time.deltaTime
             * Speed);

            anim.SetBool("isWalking", true);

            rotSpeed = Speed * Random.Range(1f, 1.1f);

            Vector3 lookAt = waypoints[currentWaypoint].transform.position
                - this.transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
                rotSpeed * Time.deltaTime);
        }

        if (numofWaypoints > waypointtoStop)
        {
            anim.SetBool("isWalking", false);

            StartCoroutine(StartReset());
        }

        if (transform.rotation.z >= 90 || transform.rotation.z <= -90)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // transform.LookAt(waypoints[currentWaypoint].transform.position);



        /*  Vector3 directionFromMeToTarget = waypoints[currentWaypoint].transform.position
              - transform.position;
          directionFromMeToTarget.x = 0.0f;
          Quaternion lookRotation = Quaternion.LookRotation(directionFromMeToTarget);

          transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation,
              Time.deltaTime * degreesPerSecond / 360.0f);
        */
    }
}
