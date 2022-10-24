using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip step;

    void Step()
    {
        audioSource.PlayOneShot(step);
    }
}
