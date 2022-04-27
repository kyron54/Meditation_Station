using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    // This is the Image being used for transitioning
    public Animator anim;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isPressed", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //anim.SetTrigger("isTriggered");
            PlayTransition();
        }
    }

    //Purely for testing purposes.
    public void PlayTransition()
    {
        anim.SetBool("isPressed", true);
    }
}
