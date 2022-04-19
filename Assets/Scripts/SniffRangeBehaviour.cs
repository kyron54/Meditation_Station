using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffRangeBehaviour : MonoBehaviour
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
        if(other.gameObject.name.Contains("Pink Fruit"))
        {
            anim.SetBool("isSniffing", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Pink Fruit"))
        {
            anim.SetBool("isSniffing", false);
        }
    }
}
