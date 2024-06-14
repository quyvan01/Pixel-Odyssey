using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int health;
    public int damage;
    private float timeBtwDamage = 3f;
    public GameObject winUI; // Reference to the Win UI panel
    [SerializeField] private AudioSource WinSound;

    public Slider healthBar;
    private Animator anim;
    public bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 25)
        {
            anim.SetTrigger("stageTwo");
        }

        if (health <= 0 && !isDead)
        {
            HandleDeath();
        }

        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isDead)
        {
            if (timeBtwDamage <= 0)
            {
                other.GetComponent<PlayerHealth>().TakeDamage(25f);
            }
        }
    }

    private void HandleDeath()
    {
        anim.SetTrigger("death");
        isDead = true; // Ensure the death logic runs only once
        StartCoroutine(ShowWinUIAfterDelay(3f)); // Start the coroutine with a 3-second delay
        
    }

    // Coroutine for delaying the display of WinUI
    private IEnumerator ShowWinUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        winUI.SetActive(true); // Activate the Win UI after the delay
        WinSound.Play();
    }
}
