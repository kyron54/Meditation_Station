using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public bool pressPause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        ButtonResume();
    }

    public void Resume()
    {
        pressPause = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }   
    
    public void QuitGame()
    {
        Application.Quit();
    }

    private void ButtonResume()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start) || Input.GetKeyDown(KeyCode.C))
        {
            pressPause = true;
        }
        else
        {
            pressPause = false;
        }
    }
}
