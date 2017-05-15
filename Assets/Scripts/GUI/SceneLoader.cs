using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    private bool loadScene = false;
  
    public GameObject panelLoading;
    public GameObject panelMenu;
    public GameObject highScore;
    public GameObject option;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;


    public void PlayGame()
    {
        panelLoading.gameObject.SetActive(true);
        panelMenu.gameObject.SetActive(false);

        if (loadScene == false)
        {
            loadScene = true;
            loadingText.text = "Loading...";
            StartCoroutine(LoadNewScene());
        }

        if (loadScene == true)
        {

            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void HighScore()
    {
        highScore.gameObject.SetActive(true);
        panelMenu.gameObject.SetActive(false);
    }

    public void OpenOption()
    {
        option.gameObject.SetActive(true);
        panelMenu.gameObject.SetActive(false);
    }

    public void CloseOption()
    {
        panelMenu.gameObject.SetActive(true);
        option.gameObject.SetActive(false);
    }

    public void CloseHighScore()
    {
        panelMenu.gameObject.SetActive(true);
        highScore.gameObject.SetActive(false);
    }


    IEnumerator LoadNewScene() {

        yield return new WaitForSeconds(1.5f);
        AsyncOperation async = Application.LoadLevelAsync(scene);
        
        while (!async.isDone) {
            yield return null;
        }

    }

}
