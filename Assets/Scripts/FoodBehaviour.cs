using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBehaviour : MonoBehaviour
{
    private Rigidbody rb;

   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
      //  rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
           //  rb.isKinematic = false;

          //  rb.constraints = RigidbodyConstraints.None;
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
           // rb.isKinematic = false;

          //  rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand"))
        {
            rb.isKinematic = false;
        }
    }


}
