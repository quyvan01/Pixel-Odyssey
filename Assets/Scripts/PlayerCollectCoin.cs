using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Namespace for TextMeshPro
using UnityEngine.UI; // Namespace for UI


public class PlayerCollectCoin : MonoBehaviour
{
    public int score = 0; // Initial score
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro GUI element
    [SerializeField] private AudioSource collectCoinAudioSource; 
    public Button buyItemButton; // Reference to the BuyItem button
    public Animator buyButtonAnimator; // Reference to the Animator component on the BuyItemButton


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        // Check the score each frame and update the button's interactable state
        if (score >= 6)
        {
            buyItemButton.interactable = true; // Enable the BuyItem button
        }
        else
        {
            buyItemButton.interactable = false; // Disable the BuyItem button
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("Collision Detected with: " + collision.gameObject.name);
        // Check if the collided object is a coin
        if (collision.gameObject.CompareTag("Coin"))
        {
            collectCoinAudioSource.Play();
            Debug.Log("Collided with a coin!");
            collision.gameObject.GetComponent<Collider2D>().enabled = false; // Disable the collider
            Destroy(collision.gameObject); // Destroy the coin
            score++; // Increase the score
            UpdateScoreText(); // Update the score display
        }

    }

    public void UpdateScoreText()
    {
        scoreText.text = "Coins: " + score.ToString(); // Update the TextMeshPro text
        // if (score >= 3)
        // {
        //     buyItemButton.interactable = true; // Enable the BuyItem button
        // }
    }
}
