using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform[] wayPoints;
    public bool loopWayPoints;
    private NavMeshAgent agent;
    Animator animator;
    public Transform player;
    int wayPoint = 0;
    bool attack = false;
    public Collider col;
    public bool npsNormal;
    public bool police;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthNPC health;
    public ParticleSystem gun1;
    public ParticleSystem gun2;
    public ParticleSystem dieEffect;
    public ParticleSystem blood;
    public AudioSource policeGun;
    AudioSource scream;
    public AudioClip girl;
    public AudioClip zombie;
    public AudioClip man;
    public AudioClip cuttingClip;
    public AudioSource cutting;

    private void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartCoroutine("Patrol");
        health.SetMaxHealth(maxHealth);
        scream = GetComponent<AudioSource>();
    }

    IEnumerator Patrol()
    {
        agent.SetDestination(wayPoints[wayPoint].position);
        while (true)
        {
            if (Vector3.Distance(wayPoints[wayPoint].position, transform.position) < 2)
            {
                if (wayPoint == wayPoints.Length - 1)
                {
                    if (loopWayPoints)
                    {
                        wayPoint = 0;
                        agent.SetDestination(wayPoints[0].position);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    wayPoint++;
                    agent.SetDestination(wayPoints[wayPoint].position);
                }
            }
            yield return new WaitForSeconds(.5f);
        }
        yield return null;
    }
    private void Update()
    {
        if (agent.velocity.magnitude > 0.01f)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (attack == true && npsNormal == false && police == false)
        {
            agent.SetDestination(player.transform.position);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
            agent.speed = 3f;
            if (Vector3.Distance(player.transform.position, transform.position) < 1)
            {
                animator.SetBool("Attack2", true);
            }
            else
            {
                animator.SetBool("Attack2", false);
                animator.SetBool("Run", true);
                animator.SetBool("Walk", true);
            }
        }
        else if (attack == true && npsNormal == true && police == false)
        {
            animator.SetBool("Run", true);
            agent.speed = 3f;
        }
        else if (attack == true && npsNormal == false && police == true)
        {
            animator.SetBool("Attack", true);
            transform.LookAt(player.transform.position);
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            agent.speed = 0;
        }
        if (currentHealth <= 0)
        {
            animator.SetBool("Die", true);
            agent.speed = 0f;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attack = true;
        }
        if (other.gameObject.tag == "PlayerDamage")
        {
            TakeDamage(25);
            cutting.PlayOneShot(cuttingClip);
            blood.Play();
            if (this.gameObject.tag == "Zombie")
            {
                scream.PlayOneShot(zombie);
            }
            else if (this.gameObject.tag == "Man")
            {
                scream.PlayOneShot(man);
            }
            else if (this.gameObject.tag == "Girl")
            {
                scream.PlayOneShot(girl);
            }

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
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health.SetHealth(currentHealth);
    }
    public void GunEffect()
    {
        gun1.Play();
        gun2.Play();
        policeGun.Play();
    }
    public void DieEffect()
    {
        dieEffect.Play();
    }
    public void SpiritScore()
    {
        ScoreManager.instance.AddPoint();
    }
    public void PoliceDamage()
    {
        GameObject.Find("Player").GetComponent<Move>().TakeDamage(3);
    }

}
