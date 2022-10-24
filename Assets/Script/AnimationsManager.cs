using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    Animator animator;
    public float coolDownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0f;
    float maxComboDelay = 1f;
    AudioSource audioSource;
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public AudioClip die;
    bool dieBool = false;
    public Collider col;
    public float timeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animator.SetBool("hit2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            animator.SetBool("hit3", false);
            noOfClicks = 0;
        }
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnClick();
            }
        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == true && dieBool == false)
        {
            audioSource.PlayOneShot(die);
            dieBool = true;
        }
    }
    void OnClick()
    {
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks >= 1)
        {
            animator.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
            animator.SetBool("hit2", true);
        }
        if (noOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animator.SetBool("hit2", false);
            animator.SetBool("hit3", true);
        }
    }
    public void AttackOpen()
    {
        col.enabled = true;
    }
    public void AttackClosed()
    {
        col.enabled = false;
    }
    public void Hit1()
    {
        audioSource.PlayOneShot(hit1);
    }
    public void Hit2()
    {
        audioSource.PlayOneShot(hit2);
    }
    public void Hit3()
    {
        audioSource.PlayOneShot(hit3);
    }
    public void Shake()
    {
        CameraShake.Instance.ShakeCamera(3f, 5f);
    }
}
