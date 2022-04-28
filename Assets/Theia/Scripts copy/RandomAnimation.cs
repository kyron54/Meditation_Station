using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{

    Animator anim;
    public float minWait, maxWait;
    public float rareAnimChance;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        timer = Time.time + Random.Range(minWait, maxWait);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
            PlayAnimation();
    }

    void PlayAnimation()
    {
        if (Random.Range(1, 100) < rareAnimChance)
            anim.SetTrigger("PlayRare");
        else
            anim.SetTrigger("Play");
        timer = Time.time + Random.Range(minWait, maxWait);
    }
}
