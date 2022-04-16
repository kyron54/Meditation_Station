using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool pauseOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Start) || Input.GetKeyDown(KeyCode.C))
        {
            pauseOn = !pauseOn;
        }

        pauseMenu.SetActive(pauseOn);
    }
}
