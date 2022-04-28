using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToNextScene : MonoBehaviour
{
    Animator anim;
    GameObject transCon;

    // Start is called before the first frame update
    void Start()
    {
        transCon = GameObject.FindGameObjectWithTag("Transition");
        anim = transCon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isPressedLong", true);
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(8);

        SceneManager.LoadScene(0);
    }
}
