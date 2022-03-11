using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OVRInput.Update();
    }

    // Update is called once per frame
    void Update()
    {
        switchScenes();
    }

    void switchScenes()
    {

    }

    public void MeditationStart()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
}
