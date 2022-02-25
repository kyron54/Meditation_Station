using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatOnWater : MonoBehaviour
{
    Rigidbody rb;
    float yPos;
    public float waterLevel;
    public float buoyancy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        yPos = transform.position.y;

        if (yPos < waterLevel)
        {
            rb.AddForce(Vector3.up * buoyancy);
        }
    }
}
