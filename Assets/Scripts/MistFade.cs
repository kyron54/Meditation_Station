using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistFade : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mist;

    [SerializeField] private float smoothFade;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeMin;

    bool fadingIn = true;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == player)
        {
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        if (fadingIn)
        {
            // For some reason, "Vector1_014feeecad424ce4ab7b8624bba25862" is the name of the
            // alpha property of the mist sphere's shader
            for (float i = mist.gameObject.GetComponent<Renderer>().material.GetFloat("Vector1_014feeecad424ce4ab7b8624bba25862");
                i >= fadeMin; i -= smoothFade)
            {
                if (fadingIn)
                {
                    // "_offset" is, for whatever reason, the name of the GradientRise property
                    mist.gameObject.GetComponent<Renderer>().material.SetFloat("Vector1_014feeecad424ce4ab7b8624bba25862", i);
                    yield return new WaitForSeconds(fadeSpeed);
                }
            }
            GetComponent<Collider>().gameObject.SetActive(false);
        }
    }
}
