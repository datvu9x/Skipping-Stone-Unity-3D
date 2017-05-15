using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	public Transform mainMenu, optionsMenu, highScore;
    public Text player;

    private void Start()
    {
        player.text = Player.UserName; ;
    }
    public void LoadScene(string name){
		Application.LoadLevel(name);
	}

    public void HighScore(string name)
    {
        Application.LoadLevel(name);
    }
    public void QuitGame(){
		Application.Quit();
	}

    public void HighScore(bool clicked)
    {
        if (clicked == true)
        {
            highScore.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            highScore.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
	public void OptionsMenu(bool clicked){
		if (clicked == true){
			optionsMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(false);
		} else {
			optionsMenu.gameObject.SetActive(clicked);
			mainMenu.gameObject.SetActive(true);
		}
	}

}
