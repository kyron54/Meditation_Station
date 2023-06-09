using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Animator anim;
    private AudioSource start;
    public Button startButton;
    public Button credits;

    // Start is called before the first frame update
    void Start()
    {
        start = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(PlayStart());
        }
    }

    public void switchCredits()
    {
        SceneManager.LoadScene(4);
    }

    public void MeditationStart()
    {
        StartCoroutine(PlayStart());
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator PlayStart()
    {
        anim.SetBool("Fade", true);
        start.Play();
        startButton.interactable = false;
        credits.interactable = false;

        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(1);
    }
}
