using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerPetBehaviour : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hand"))
        {
            anim.SetBool("isPet", true);
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
    }
}
