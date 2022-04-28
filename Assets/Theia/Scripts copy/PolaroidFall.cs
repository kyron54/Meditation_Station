using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidFall : MonoBehaviour
{
    public float rotSpeed, upSpeed;
    public float fallSpeed, fallAccel, minSize;

    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = Random.Range(-5, 5);
        upSpeed = Random.Range(-5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > minSize)
        {
            transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
            transform.Translate(Vector3.right * rotSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
            transform.localScale = transform.localScale - (Vector3.one * Time.deltaTime * fallSpeed);
            
            fallSpeed = Mathf.Clamp(fallSpeed - (fallAccel * Time.deltaTime), 1, 100);
        }
        else
            transform.localScale = Vector3.one * minSize;
    }

    public void Dragged()
    {
        transform.position = Input.mousePosition;
    }
}
