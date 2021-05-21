using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // pause menu pannel
    public GameObject pauseMenu;

    // the main audio mixer
    public AudioMixer audioMixer;

    // start the game
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // show the UI at the game start
    public void ShowUI()
    {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }

    // pause the game
    public void PauseGame()
    {
        // show the pasu menu pannel
        pauseMenu.SetActive(true);
        // pause the game 
        Time.timeScale = 0f;
    }

    // resume the game
    public void ResumeGame()
    {
        // hide the pasu menu pannel
        pauseMenu.SetActive(false);
        // resume the game
        Time.timeScale = 1f;
    }

    // set the audio volume value
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }
}