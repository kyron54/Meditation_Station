using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayTimer : MonoBehaviour
{
    private float timeValue;
    public float seconds;
    public Text timeText;

    GameObject transitionController;
    private TransitionController transCon;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //seconds = 90;
        timeValue = seconds;

        //transitionController = GameObject.FindGameObjectWithTag("Transition Controller");
        //transCon = transitionController.GetComponent<TransitionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);

        if (timeValue <= 0)
        {
            End();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("Time Remaining in Experience: {0:00}:{1:00}", minutes, seconds);

        if(timeValue <= 60)
        {
            timeText.color = new Color(100, 100, 100, 255);
        }
        else
        {
            timeText.color = new Color(100, 100, 100, 0);
        }
    }

    void ActivateEndTransition()
    {
        anim.SetBool("isPressed", true);
    }

    public void End()
    {
        ActivateEndTransition();
        StartCoroutine(EndTransition(3));
    }

    IEnumerator EndTransition(float waitTime)
    {
        print("does this shit work?");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(3);
    }
}
