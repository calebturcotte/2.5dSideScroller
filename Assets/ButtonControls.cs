using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControls : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Player.GamePaused = false;
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
       Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        Debug.Log("Change Scene to Menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Close Application");
        Time.timeScale = 1f;
    }

}
