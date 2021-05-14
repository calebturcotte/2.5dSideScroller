using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    public Scene level;
    public void Resume() //resume the game
    {
        pauseMenuUI.SetActive(false); //deactivate the pause menu
        Player.GamePaused = false; //set gamePaused to FALSE (may be needed to pause non-time related elements)
        Time.timeScale = 1f; //set timescale to 1; normal speed
    }

    public void RestartLevel() //restart current level
    {
        Player.GamePaused = false;
        Scene scene = SceneManager.GetActiveScene(); //get current scene (initially)
        SceneManager.LoadSceneAsync(scene.name); //load the initial scene
        Time.timeScale = 1f; //set timescale to 1; normal speed (since we access this from the pause menu, it will be 0)
    }

    public void QuitGame() //Quite game and return to main menu
    {
        SceneManager.LoadSceneAsync("Main Menu"); //load the main menu scene
        Player.GamePaused = false; //game state is no longer paused
        Time.timeScale = 1f; //set timescale to 1; normal speed (since we access this from the pause menu, it will be 0)
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Enemy_AI_test"); //load "level 1"
        //no need to set timescale or gamePaused; they are 1 and false when loaded in
    }

}
