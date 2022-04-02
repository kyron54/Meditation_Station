using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOneOccluder : MonoBehaviour
{
    public OcclusionPortal myOc;
    // Start is called before the first frame update
    void Start()
    {
        // myOc = GetComponent<OcclusionPortal>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            myOc.open = !myOc.open;
        }
    }
}
