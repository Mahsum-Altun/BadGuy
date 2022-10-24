using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    AudioSource npcDamage;
    public AudioSource metal;
    public GameObject bird;
    public GameObject ocean;
    bool music = false;
    private void Start()
    {
        npcDamage = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPCDamage")
        {
            transform.parent.GetComponent<Move>().TakeDamage(2);
            npcDamage.Play();
            bird.SetActive(false);
            ocean.SetActive(false);
            if (music == false)
            {
                music = true;
                metal.Play();
            }

        }
    }
}
