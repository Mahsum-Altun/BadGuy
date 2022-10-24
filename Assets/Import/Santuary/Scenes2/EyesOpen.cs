using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EyesOpen : MonoBehaviour
{
    public GameObject eyes1;
    public GameObject eyes2;
    public AudioClip audioClip;
    public AudioSource audioSource;
    Animator animator;
    Spirit spirit;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Spirit.instance.currentHealth == 20)
        {
            animator.SetBool("Cam", true);
        }
    }
    public void Eyes()
    {
        audioSource.PlayOneShot(audioClip);
        eyes1.SetActive(true);
        eyes2.SetActive(true);

        StartCoroutine("MainMenu");
    }
    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
