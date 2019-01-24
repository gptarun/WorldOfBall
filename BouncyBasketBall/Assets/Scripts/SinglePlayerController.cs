using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SinglePlayerController : MonoBehaviour {
    [HideInInspector] public string teamAMode;
    [HideInInspector] public string teamBMode;
    private string teamAFullName;
    private string teamBFullName;
    [SerializeField] Text quater;
    private int totalQuaterCounter;
    private int quaterCounter = 1;
    [SerializeField] Text quaterTimer;
    private int qTimer;
    [SerializeField] Text teamAScore;
    public int scoreA;
    [SerializeField] Text teamBScore;
    public int scoreB;
    [SerializeField] Text teamAName;
    [SerializeField] Text teamBName;
    [SerializeField] GameObject canvasObject;
    [SerializeField] TextMeshProUGUI winnerName;
    [SerializeField] Button pauseButton;
    private static bool pauseButtonPressed = false;
    bool gameOver = false;  
    public GameObject jumpAnim;
    public GameObject FoulAnim;
    float delatTime;
    public GameObject[] player = new GameObject[6];
    Vector3[] playerPosition = new Vector3[6];
    public Transform ballPosition;
    Vector3 ballInitPos;
    // Use this for initialization
    void Start () {
        pauseButtonPressed = false;
        totalQuaterCounter = OptionMenuScript.quaterCounter;
        quater.text = "Q" + quaterCounter;
        qTimer = OptionMenuScript.quaterDuration;
        quaterTimer.text = "" + qTimer;
        scoreA = 0;
        scoreB = 0;
        teamAScore.text = scoreA.ToString();
        teamBScore.text = scoreB.ToString();
        for (int i = 0; i < OptionMenuScript.teamSizeCounter*2; i++)
        { 
            player[i].SetActive(true);
            playerPosition[i] = player[i].transform.position;
        }
        ballPosition.transform.position = ballInitPos;
        StartCoroutine("LoseTime");
        delatTime = qTimer;
        Time.timeScale = 1;
        StartCoroutine(MakeUserReady());
    }
	
	// Update is called once per frame
	void Update () {
        if (!pauseButtonPressed)
        {
            teamAScore.text = scoreA.ToString();
            teamBScore.text = scoreB.ToString();
            delatTime -= Time.deltaTime;
            if (qTimer > -1)
            {
                GameQuaterCounter(qTimer);

            }
            else if(qTimer < 1 && quaterCounter != totalQuaterCounter)
            {
                qTimer = OptionMenuScript.quaterDuration;
                quaterCounter++;
                quater.text = "Q" + quaterCounter;
                StartCoroutine(MakeUserReady());
            }
            else
            {
                if (!gameOver)
                {
                    gameOver = true;
                    GameWinner();
                }
            }
        }
    }
    
    IEnumerator MakeUserReady()
    {
        StopCoroutine("LoseTime");
        ResetPositions();
        jumpAnim.SetActive(true);
        yield return new WaitForSeconds(2);
        jumpAnim.SetActive(false);
        StartCoroutine("LoseTime");
    }

    public void ResetPositions()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i].transform.position = playerPosition[i];
        }
        ballPosition.transform.position = ballInitPos;        
    }

    public void GameQuaterCounter(int qTimer)
    {
        quaterTimer.text = qTimer.ToString();                        
    }

    public void SinglePlayerTeams()
    {
        Debug.Log("Game Begins");        
    }

    public void SetTeams(string teamANameSelected, string teamBNameSelected, string modeA, string modeB)
    {
        teamAName.text = teamANameSelected.Substring(0, 4);
        teamBName.text = teamBNameSelected.Substring(0, 4);
        teamAMode = modeA;
        teamBMode = modeB;
        teamAFullName = teamANameSelected;
        teamBFullName = teamBNameSelected;
        if(GameObject.Find("MenuBg") != null)
        {
            Destroy(GameObject.Find("MenuBg"));
        }
    }
   
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
   
    public void PauseGame()
    {
        pauseButtonPressed = true;
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        pauseButtonPressed = false;
        Time.timeScale = 1f;        
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            qTimer--;
        }
    }

    public void GameWinner()
    {
        if (scoreA > scoreB)
        {
            Debug.Log("Team A wins");
            winnerName.text = teamAFullName + " Wins!";
            MatchEnded();
        }
        else if (scoreA < scoreB)
        {
            Debug.Log("Team B wins");
            winnerName.text = teamBFullName + " Wins!";
            MatchEnded();
        }
        else
        {
            Debug.Log("Draw");
            gameOver = false;
            qTimer = OptionMenuScript.quaterDuration;
        }
    }
    public void MatchEnded()
    {
        pauseButton.interactable = false;
        StopAllCoroutines();
        canvasObject.SetActive(true);        
    }
}
