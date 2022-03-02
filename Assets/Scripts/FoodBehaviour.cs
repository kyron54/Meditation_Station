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
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
            rb.isKinematic = false;
           
        }

        
        if(collision.gameObject.CompareTag("Animal Mouth"))
        {
            Destroy(gameObject);
        }

        
    }


}
