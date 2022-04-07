using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBarrier : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float smoothFade;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeMin;
    [SerializeField] private float fadeMax;

    bool fadingIn = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && fadingIn)
        {
            StartCoroutine(Fade());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            fadingIn = false;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        if (fadingIn)
        {
            for (float i = fadeMin; i <= fadeMax; i += smoothFade)
            {
                if (fadingIn)
                {
                    // "_offset" is, for whatever reason, the name of the GradientRise property
                    GetComponent<Renderer>().material.SetFloat("_offset", i);
                    yield return new WaitForSeconds(fadeSpeed);
                }
            }
            fadingIn = false;
        }
        else
        {
            for (float i = gameObject.GetComponent<Renderer>().material.GetFloat("_offset"); i > fadeMin; i -= smoothFade)
            {
                gameObject.GetComponent<Renderer>().material.SetFloat("_offset", i);
                yield return new WaitForSeconds(fadeSpeed);
            }
            GetComponent<Renderer>().material.SetFloat("_offset", -50);
            fadingIn = true;
        }
    }
}
