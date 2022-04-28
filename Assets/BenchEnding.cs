using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchEnding : MonoBehaviour
{
    bool nearBench = false;
    private PlayTimer pt;
    public GameObject playTimer;

    // Start is called before the first frame update
    void Start()
    {
        pt = playTimer.GetComponent<PlayTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (nearBench == true)
            {
                pt.End();
            }
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
        if (other.tag == "Player")
        {
            nearBench = false;
        }
    }
}
