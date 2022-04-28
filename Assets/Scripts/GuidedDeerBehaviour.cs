using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedDeerBehaviour : MonoBehaviour
{

    public bool shouldMove = false;

    public GameObject player;
    public GameObject[] waypoints;
    public GameObject[] guidedWaypoints;
    public int currentWaypoint = 0;
    public int currentWaypoint1;
    public float guidedSpeed = 3;
    public float rotSpeed = 5;

    private Vector3 currentPosition;

    public bool arrived;

    private Animator anim;

    public int numofWaypoints = 0;

    public float resetTime = 10.0f;

    public int waypointtoStop;

    public float waypointRadius;

    public float Speed = 3;

    private AudioSource walkingSoundSource;

    private bool shouldPlayWalkingSound = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        walkingSoundSource = gameObject.transform.Find("Walking Sound Player").
            GetComponent<AudioSource>();
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
            anim.SetBool("isWalking", false);

            walkingSoundSource.Stop();
            shouldPlayWalkingSound = true;
        }

        if(arrived == true && Vector3.Distance(transform.position,
            player.transform.position) > 3.0f)
        {
            PathWayMovement();
        }

        if (arrived == true && Vector3.Distance(transform.position,
           player.transform.position) < 3.0f)
        {
            anim.SetBool("isWalking", false);

            walkingSoundSource.Stop();
            shouldPlayWalkingSound = true;
        }

        GuidedMovement();
    }

    public void GuidedMovement()
    {
        if(shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                guidedWaypoints[currentWaypoint1].transform.position, Time.deltaTime
                * guidedSpeed);
            anim.SetBool("isWalking", true);

            if(shouldPlayWalkingSound)
            {
                shouldPlayWalkingSound = false;
                walkingSoundSource.Play();
            }

            Vector3 lookAt = guidedWaypoints[currentWaypoint1].transform.position
                - this.transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
                rotSpeed * Time.deltaTime);
        }
    }

    public void PathWayMovement()
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
                anim.SetBool("isEating", false);
                anim.SetBool("isPet", false);

                if (shouldPlayWalkingSound)
                {
                    shouldPlayWalkingSound = false;
                    walkingSoundSource.Play();
                }

                rotSpeed = Speed * Random.Range(1f, 1.1f);

                Vector3 lookAt = waypoints[currentWaypoint].transform.position
                    - this.transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt),
                    rotSpeed * Time.deltaTime);
            }

            if (numofWaypoints > waypointtoStop)
            {
                anim.SetBool("isWalking", false);

                walkingSoundSource.Stop();

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

    IEnumerator StartReset()
    {
        yield return new WaitForSeconds(resetTime);

        numofWaypoints = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Guided Deer Waypoint")
        {
            shouldMove = false;
            arrived = true;
        }

        if (other.gameObject.CompareTag("Hand"))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isPet", true);
            // anim.SetBool("isEating", false);

            walkingSoundSource.Stop();
            shouldPlayWalkingSound = true;
        }

        if (other.gameObject.name.Contains("Pink Fruit"))
        {
           // anim.SetBool("isEating", true);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            anim.SetBool("isPet", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            anim.SetBool("isPet", false);
        }

        if (other.gameObject.name.Contains("Pink Fruit"))
        {
           // anim.SetBool("isEating", false);
            anim.SetBool("isWalking", true);

            if (shouldPlayWalkingSound)
            {
                shouldPlayWalkingSound = false;
                walkingSoundSource.Play();
            }
        }
    }
}
