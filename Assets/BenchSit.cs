using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchSit : MonoBehaviour
{
    GameObject player;
    Vector3 benchPos;
    Vector3 tempPos;
    bool nearBench = false;
    bool playerSat = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        benchPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z -0.2f);
        tempPos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (nearBench == true)
        {
            if (Input.GetKeyDown(KeyCode.B) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) && playerSat == true)
            {
                print(tempPos.z);
                playerSat = !playerSat;
                player.transform.position = tempPos;
            }
            else if (Input.GetKeyDown(KeyCode.B) || OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) && playerSat == false)
            {
                playerSat = !playerSat;
            }
        }

        AttachPlayer();
    }

    void AttachPlayer()
    {
        if (playerSat)
        {
            player.transform.position = benchPos;
            player.transform.rotation = transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            nearBench = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            nearBench = false;
        }
    }
}
