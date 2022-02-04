using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }
}
