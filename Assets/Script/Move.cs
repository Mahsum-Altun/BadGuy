using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    Animator animator;
    CharacterController controller;
    public float speed;
    public int maxHealth = 100;
    public int currentHealth;
    public Health health;
    public float turnSmoothTime;
    float tunrSmoothVelocity;
    public Transform cam;
    float ySpeed;

    private void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        health.SetMaxHealth(maxHealth);
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref tunrSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.y = ySpeed;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if (currentHealth <= 0)
        {
            animator.SetBool("Death", true);
            speed = 0f;
            StartCoroutine("Die");
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ScoreManager.instance.Relog();
            StartCoroutine("Die");
        }
        if (controller.isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health.SetHealth(currentHealth);
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
