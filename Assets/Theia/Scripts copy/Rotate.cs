using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool touchControls;
    public Vector3 rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (touchControls)
        {
            if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
                transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotSpeed.y, 0));
        }
        else
            transform.Rotate(rotSpeed * Time.deltaTime);
    }

    
}
