/**
 * This script controls the birds that fly in the air 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlyoverBehaviour : MonoBehaviour
{
    // Makes sure shrinkage isn't rapid
    bool shrinkStarted = false;

    // Speed of the birds
    [SerializeField] int speed = 10;

    // These vars set the location the birds teleport to
    [SerializeField] GameObject destObject;
    [SerializeField] Vector3 teleDest;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        teleDest = destObject.transform.position;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Controls movement
        Movement();

        // Triggers shrink
        if (!shrinkStarted && transform.position.x > 300 || transform.position.z > 300 ||
            transform.position.x < -100 || transform.position.z > -300)
        {
            // Prevents coroutine from starting multiple times
            shrinkStarted = true;

            StartCoroutine(Shrink());
        }
    }

    /// <summary>
    /// Moves the bird forward
    /// </summary>
    void Movement()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    /// <summary>
    /// Shrinks the bird
    /// </summary>
    /// <returns>waits before shrinking</returns>
    IEnumerator Shrink()
    {
        while(transform.localScale.magnitude > 0)
        {
            // Shrinks the bird
            transform.localScale -= new Vector3(.01f, .01f, .01f);

            // Delays
            yield return new WaitForSeconds(.1f);
        }

        // Triggers teleport
        Teleport();
    }

    /// <summary>
    /// Resets bird position for subsequent flyovers
    /// </summary>
    void Teleport()
    {
        transform.position = teleDest;
    }

}
