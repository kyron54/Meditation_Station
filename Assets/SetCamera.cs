using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCamera : MonoBehaviour
{
    Canvas canvas;
    GameObject main;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        main = GameObject.FindGameObjectWithTag("MainCamera");
        cam = main.GetComponent<Camera>();
        canvas.worldCamera = cam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
