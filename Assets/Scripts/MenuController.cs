using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {
    public string currentScene;
    public Canvas currentCanvas, itemCanvas;
    public void QuitApplication()
    {
        Application.Quit();
    }

    public void RestartingGame()
    {
        SceneManager.LoadScene(currentScene);
        ReanudatingGame();
    }
    public void ResumeGame()
    {
        currentCanvas.enabled = false;
        itemCanvas.enabled = true;
        ReanudatingGame();
    }

    void ReanudatingGame()
    {
        Time.timeScale = 1;
    }
}
