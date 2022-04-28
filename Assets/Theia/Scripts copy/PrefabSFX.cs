using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSFX : MonoBehaviour
{
    public AudioSource localSFX;
    public void PlayPrefabSFX(AudioClip sfx)
    {
        localSFX.PlayOneShot(sfx);
    }
}
