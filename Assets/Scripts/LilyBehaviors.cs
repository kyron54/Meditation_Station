using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyBehaviors : MonoBehaviour
{
    Transform transform;

    private Vector3 origin;

    private int rotationDecider;

    public float moveSpeed;
    public int rotationSpeed;

    private bool reachedDestination = true;

    private void Awake()
    {
        transform = gameObject.GetComponent<Transform>();

        origin = transform.position;

        rotationDecider = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(rotationDecider);

        if(reachedDestination)
        {
            reachedDestination = false;
            StartCoroutine(RandomMove());
        }
    }

    IEnumerator RandomMove()
    {
        Vector3 randPoint = Random.insideUnitCircle;
        Vector3 randMove = new Vector3(origin.x + randPoint.x, origin.y,
            origin.z + randPoint.z);

        float moveStep = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(origin, randMove, moveStep);
        transform.position = Vector3.Lerp(transform.position, randMove,
            moveSpeed);

        reachedDestination = true;

        yield return null;
    }

    void Rotate(int decider)
    {
        if(rotationDecider == 0)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime,
                Space.Self);
        }
        else
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime,
                Space.Self);
        }
    }
}
