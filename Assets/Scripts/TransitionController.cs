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
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayTransition();
    }

    //Purely for testing purposes.
    void PlayTransition()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("isTriggered");
        }
    }
}
