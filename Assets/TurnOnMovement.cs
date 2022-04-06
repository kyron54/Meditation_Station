using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnMovement : MonoBehaviour
{
    [Tooltip("Press 'M' on your keyboard in order to enable movement! Turning it on and off again is Trash!")]
    public BasicMovement bm;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            bm.enabled = !bm.enabled;
            Cursor.visible = !Cursor.visible;
        }
    }
}
