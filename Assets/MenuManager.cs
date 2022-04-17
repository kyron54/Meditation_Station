using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator anim;
    private AudioSource start;

    // Start is called before the first frame update
    void Start()
    {
        start = GetComponent<AudioSource>();
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

        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(1);
    }
}
