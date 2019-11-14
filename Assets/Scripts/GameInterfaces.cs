using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInterfaces : MonoBehaviour {
    [SerializeField]
    GameObject pauseUI;
    [SerializeField]
    GameObject gameOverUI;

    public bool isPaused = false;
    public bool gameEnded = false;

    //AudioSource gameOverSound;

    void Start() {
        //CursorHandling();
        //AudioSource[] audioSources = GetComponents<AudioSource>();
        //gameOverSound = audioSources[0];
    }

    void CursorHandling() {
        // Cursor handling
        if (!isPaused || !gameEnded)
            Cursor.lockState = CursorLockMode.Locked; // Removes the mouse cursor
        else if (isPaused) {
            Screen.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void PauseTheGame() {
        if (!isPaused) {
            isPaused = true;
            ShowPauseInterface();
        }
    }

    public void EndTheGame() {
        if (!gameEnded)
        {
            gameEnded = true;
            ShowGameOverInterface();
        }
    }

    private void ShowPauseInterface() {
        pauseUI.SetActive(true);
        Time.timeScale = 0.0F; // Freezing the game
    }

    private void ShowGameOverInterface() {
        gameOverUI.SetActive(true);
        Time.timeScale = 0.0F; // Freezing the game
        //gameOverSound.Play();
    }

    public void ContinuePlaying() {
	    Time.timeScale = 1.0F;
	    pauseUI.SetActive(false); // Unpause
        isPaused = false;
    }

    public void LoadNewScene(string name) {
        SceneManager.LoadScene(name);
        Time.timeScale = 1.0F;
    }
}
