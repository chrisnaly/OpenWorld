using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
