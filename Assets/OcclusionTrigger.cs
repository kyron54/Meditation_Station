using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionTrigger : MonoBehaviour
{
    OcclusionPortal myOc;
    // Start is called before the first frame update
    void Start()
    {
        myOc = GetComponent<OcclusionPortal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        myOc.open = false;
    }

    private void OnTriggerExit(Collider other)
    {
        myOc.open = true;
    }
}
