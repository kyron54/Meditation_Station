using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnablePause : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    public PauseMenuController pause;
    public GameObject uiHelper;
    public GameObject cameraRig;
    public AudioSource riverSound;

    public bool isPaused = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        cam1.enabled = true;
        cameraRig.SetActive(false);
        //cam2.enabled = false;
    }

    void Update()
    {
        if (pause.pressPause && isPaused == false)
        {
            isPaused = true;
        }

        if (isPaused)
        {
            cameraRig.SetActive(true);

            cam1.enabled = !cam1.enabled;
            cam2.enabled = true;

            if (pause.pressPause)
            {
                isPaused = false;
            }

        }

        if(isPaused == true)
        {
            Time.timeScale = 0;
            uiHelper.SetActive(true);
            riverSound.mute = true;
        }
        else
        {
            Time.timeScale = 1;
            uiHelper.SetActive(false);
            riverSound.mute = false;
            cameraRig.SetActive(false);
        }
    }
}
