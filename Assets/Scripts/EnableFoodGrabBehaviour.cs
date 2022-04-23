using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFoodGrabBehaviour : MonoBehaviour
{
    private OVRGrabber myGrabber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyThis()
    {
        //this turns off the OVRGrabbable script

        this.GetComponent<OVRGrabbable>().enabled = false;

        //this gets the hand that's grabbing it

        myGrabber = this.GetComponent<OVRGrabbable>().m_grabbedBy;

        //use ForceRelease method in the OVRGrabber to release object

        myGrabber.ForceRelease(this.gameObject.GetComponent<OVRGrabbable>());

        //destroy object

        Destroy(this.gameObject);
    }
}
