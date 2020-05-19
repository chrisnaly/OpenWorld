using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMENU : MonoBehaviour
{
    
    public static bool GameIsPause = false;

    public GameObject pauseMenuUI;

    void Update()
{
    if(Input.GetKeyDown(KeyCode.Escape))
    {
        if(GameIsPause)
        {
            Resume();
        }
        else
        {
            Pause();

        }
        
    }
}

public void Resume()
{
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPause = false;
}

public void Pause()
{
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPause = true;
}

public void Load()
{
    SceneManager.LoadScene("SampleScene");
}

public void LoadMenu()
{
    SceneManager.LoadScene("TitleScene");
}

public void QuitGame()
{
    Debug.Log("Quitting the game...");
    Application.Quit();
}

}
