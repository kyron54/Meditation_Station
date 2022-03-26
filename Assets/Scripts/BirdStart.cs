/**
 * This starts the birds that fly over
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdStart : MonoBehaviour
{
    // Bird
    [SerializeField] GameObject bird;

    /// <summary>
    /// Starts birds when player leaves
    /// </summary>
    /// <param name="other">Object exiting the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //bird.GetComponent<BirdFlyoverBehaviour>().startFlight = true;
        }
    }
}
