using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spirit : MonoBehaviour
{
    public static Spirit instance;
    AudioSource audioSource;
    public AudioClip spiritClip;
    public ParticleSystem spiritEffect;
    public int maxHealth = 20;
    public int currentHealth;
    public BossManager health;

     private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = PlayerPrefs.GetInt("currentHealth", currentHealth);
        health.SetHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (ScoreManager.instance.score > 0 && currentHealth < 21)
            {
                ScoreManager.instance.InterestPoint();
                audioSource.PlayOneShot(spiritClip);
                spiritEffect.Play();
                TakeDamage(1);
            }

        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(1);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth += damage;
        PlayerPrefs.SetInt("currentHealth", currentHealth);
        health.SetHealth(currentHealth);
    }
}
