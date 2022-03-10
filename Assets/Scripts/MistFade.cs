using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistFade : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mistExit;
    [SerializeField] private GameObject mistSphere;

    private float playerX;
    private float playerZ;
    private float mistX;
    private float mistZ;

    private void Awake()
    {
        mistX = mistExit.transform.position.z;
        mistZ = mistExit.transform.position.z;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == player.name)
        {
            playerX = player.transform.position.x;
            playerZ = player.transform.position.z;

            float distance = Mathf.Sqrt(Mathf.Pow((playerX - mistX), 2) + Mathf.Pow((playerZ - mistZ), 2));

            Color mistColor = mistSphere.GetComponent<MeshRenderer>().material.color;
            mistColor.a = (distance / 10f);
        }
    }
}
