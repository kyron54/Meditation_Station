using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomBehaviour : MonoBehaviour
{
    private float distance;
    public float distancetoPlayer;
    public Transform targetPos;

    public GameObject spores;

    private int numofsporesSpawned;
    public int maxSpores;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, targetPos.position);

        if (distance <= distancetoPlayer && numofsporesSpawned <=
            maxSpores)
        {

            GameObject newExp = Instantiate(spores, transform.position,
            transform.rotation);

            numofsporesSpawned++;

            Destroy(newExp, .5f);
            numofsporesSpawned--;





        }
    }
}
