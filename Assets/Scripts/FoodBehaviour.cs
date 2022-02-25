using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    private Rigidbody rb;

    public float force = 500;

    private bool isGrabbed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
            rb.useGravity = true;
            isGrabbed = true;
        }

        if(collision.gameObject.CompareTag("Animal Mouth"))
        {
            Destroy(gameObject);
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
            rb.AddForce(Vector3.down * force);
            isGrabbed = false;
            
        }
    }
}
