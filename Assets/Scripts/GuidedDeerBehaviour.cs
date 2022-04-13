using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedDeerBehaviour : MonoBehaviour
{

    public bool shouldMove = false;

    public GameObject player;
    public GameObject[] waypoints;

    public int currentWaypoint = 0;
    public float speed = 3;
    public float rotSpeed = 5;

    private Vector3 currentPosition;

    public bool arrived;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position)
            < 10.0f && arrived == false)
        {
            shouldMove = true;
        }
        else if(Vector3.Distance(transform.position, player.transform.position)
            > 10.0f)
        {
            shouldMove = false;
        }

        

        GuidedMovement();
    }

    public void GuidedMovement()
    {
        if(shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                waypoints[currentWaypoint].transform.position, Time.deltaTime
                * speed);

            Vector3 lookAt = waypoints[currentWaypoint].transform.position
                - this.transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
                rotSpeed * Time.deltaTime);

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Guided Deer Waypoint")
        {
            shouldMove = false;
            arrived = true;
        }
    }
}
