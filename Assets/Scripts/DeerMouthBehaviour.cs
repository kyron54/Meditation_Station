using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerMouthBehaviour : MonoBehaviour
{
    private Animator anim;
    public float resetTime = 3.0f;

    public AudioSource eatingSoundSource;


   
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
        if (other.gameObject.name.Contains("Pink Fruit"))
        {
            anim.SetBool("isEating", true);
            anim.SetBool("isSniffing", false);

            eatingSoundSource.Play();

            StartCoroutine(NotEatingorSniffing());

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Pink Fruit"))
        {
            anim.SetBool("isEating", false);
            //  anim.SetBool("isWalking", true);
            anim.SetBool("isSniffing", false);
           
        }
    }

    IEnumerator NotEatingorSniffing()
    {
        yield return new WaitForSeconds(resetTime);
        anim.SetBool("isEating", false);
        anim.SetBool("isSniffing", false);
    }
}
