using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChanger : MonoBehaviour
{
    Renderer mat;
    private GameObject player;
    Color colorA = new Color(0, 0, 255, 100);
    Color colorB = new Color(0, 0, 255, 0);

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>();
        player = GameObject.Find("OVRPlayerController");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;

        float distance = Mathf.Abs(player.transform.position.z) - transform.position.z;
        mat.material.color = Color.Lerp(colorA, colorB, distance/5);
        Debug.Log("Distance " + distance);
    }
}
