using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatActBehaviour : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWaypoint = 0;
    public float waypointRadius;

    public float Speed = 3;

    private bool stopMovement;

    private bool isinWater;

    private float upWardForce = 5;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        stopMovement = false;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }

    public void Movement()
    {
        if(Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position)
            < waypointRadius)
        {
            currentWaypoint++;
            if(currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        if(isinWater)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                          waypoints[currentWaypoint].transform.position, Time.deltaTime
                          * Speed);

            Vector3 force = transform.up * upWardForce;

            this.rb.AddRelativeForce(force);
        }
        
       /* if(transform.position == waypoints[4].transform.position)
        {
            transform.position = waypoints[4].transform.position;
        }

        */
        Debug.Log("isRunning");

        //transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Contains("water")
            && !stopMovement)
        {
            Movement();
            isinWater = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Last Point")
        {
            stopMovement = true;
        }
    }
}
