using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMatchMatColor : MonoBehaviour
{
    public Material mat;
    ParticleSystem ps;


    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ps.startColor = mat.color;
    }
}
