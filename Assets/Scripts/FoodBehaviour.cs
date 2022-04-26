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

       // rb.isKinematic = true;
       // rb.constraints = RigidbodyConstraints.FreezePosition;
       // rb.constraints = RigidbodyConstraints.FreezeRotation;

        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    IEnumerator ActivateFruit()
    {
        yield return new WaitForSeconds(2);

        rb.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
           //  rb.isKinematic = false;

          //  rb.constraints = RigidbodyConstraints.None;
        }

        
      

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Hand"))
        {
           // rb.isKinematic = false;

          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         if(other.gameObject.CompareTag("Hand"))
         {
            StartCoroutine(ActivateFruit());
         }
        

        if (other.gameObject.CompareTag("Animal Mouth"))
        {

            Destroy(gameObject);

           
           // gameObject.GetComponent<EnableFoodGrabBehaviour>().DestroyThis();

            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        /*  if (other.gameObject.CompareTag("Hand")  && OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
          {
              rb.isKinematic = false;
          }

          */

        if (other.gameObject.CompareTag("Hand") && OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            rb.constraints = RigidbodyConstraints.None;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
       /* if(other.gameObject.CompareTag("Hand"))
        {
            rb.isKinematic = false;
        }

        */
    }


}
