using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatActBehaviour : MonoBehaviour
{
    public float Speed = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Movement()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Contains("water"))
        {
            Movement();
        }
    }
}
