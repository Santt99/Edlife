using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayQuitButtons : MonoBehaviour {
    public GameObject load;
    public GameObject mainMenu;
    public Slider loadBar;
    public Text percent;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneToLoad)
    {
        mainMenu.SetActive(false);
        load.SetActive(true);
        StartCoroutine(ChargeScene(sceneToLoad));
    }

    IEnumerator ChargeScene(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(.5f);
        AsyncOperation asinc = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asinc.isDone)
        {
            Debug.Log(asinc.progress);
            loadBar.value = asinc.progress * 100;
            
            percent.text = (Mathf.Floor(asinc.progress * 100) + 10).ToString() + "%";
            yield return null;
        }
       

    }
}
