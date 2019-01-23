using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	public void PlaySingleGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PlayTournamentGame()
    {
        SceneManager.LoadScene("TournamentScene");
    }
    public void quit()
    {
        Application.Quit();
    }
}
