using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatScript : MonoBehaviour
{
    public float speed = 1.19f;
    public float maxPosition;
    Vector3 pointA;
    Vector3 pointB;

    void Start()
    {
        pointA = transform.position;
        pointB = new Vector3(transform.position.x, transform.position.y + maxPosition, transform.position.z);
    }

    void Update()
    {
        //PingPong between 0 and 1
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(pointA, pointB, time);
    }
}
