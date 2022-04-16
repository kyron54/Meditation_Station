using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public bool pressPause = false;
    PauseMenuManager pauseManager;

    // Start is called before the first frame update
    void Start()
    {
        pauseManager = GetComponent<PauseMenuManager>();
    }

    private void Update()
    {

    }

    public void Resume()
    {
        pauseManager.pauseOn = !pauseManager.pauseOn;
        //pressPause = !pressPause;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }   
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
