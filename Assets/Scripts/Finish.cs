using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject finish1UI; // Reference to the Finish1UI
    public GameObject finish2UI; // Reference to the Finish2UI

    private AudioSource finishSound;
    private bool levelCompleted = false;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;

            // Check the current level and activate the appropriate UI
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                StartCoroutine(CompleteLevelWithUI(finish1UI));
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                StartCoroutine(CompleteLevelWithUI(finish2UI));
            }
        }
    }

    private IEnumerator CompleteLevelWithUI(GameObject finishUI)
    {
        finishUI.SetActive(true); // Activate the UI
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        finishUI.SetActive(false); // Optionally deactivate the UI
        CompleteLevel(); // Proceed to the next level
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
