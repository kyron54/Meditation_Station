using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdActBehaviour : MonoBehaviour
{
    public Transform targetPos;

    public float speed;

    private float distance;

    public float distancetoPlayer = 10;

    public Transform startPos;

    private bool arrived = false;
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
        distance = Vector3.Distance(transform.position, targetPos.position);
        if (distance <= 5.0f)
        {
            transform.position = transform.position;
        }
        if (distance <= distancetoPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, speed
                * Time.deltaTime);


        }
        if (distance >= distancetoPlayer)
        {

            transform.position = Vector3.MoveTowards(transform.position,
                startPos.position, speed * Time.deltaTime);
        }

    }
}
