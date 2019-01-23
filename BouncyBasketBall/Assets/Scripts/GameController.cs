using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    private Dictionary<int, TeamScript> TeamData = new Dictionary<int, TeamScript>();
    [SerializeField] Text quater;
    private int totalCounter;
    private int quaterCounter = 1;
    [SerializeField] Text quaterTimer;
    private int qTimer;
    [SerializeField] Text teamAScore;
    private int scoreA;
    [SerializeField] Text teamBScore;
    private int scoreB;
    [SerializeField] Text teamAName;
    [SerializeField] Text teamBName;
    [SerializeField] GameObject canvasObject;
    float timeLeft;
    bool gameOver = false;

    // Use this for initialization
    void Start () {
        
        int teamCounter = 0;
        foreach (var item in TournamentScript.currentTeams)
        {
            TeamData.Add(teamCounter, item.Value);
            teamCounter++;
        }
        teamAName.text = TeamData[0].ShortName;
        teamBName.text = TeamData[1].ShortName;
        totalCounter = 1;   //Need to get it from stting
        quater.text = "Q"+quaterCounter;
        qTimer = 2;
        timeLeft = qTimer;
        quaterTimer.text = "0"+qTimer; //Need to get it from Settings menu
        scoreA = 5;
        scoreB = 8;
        teamAScore.text = scoreA.ToString();
        teamBScore.text = scoreB.ToString();
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (qTimer > -1)
        {
            quaterTimer.text = qTimer.ToString();
        }
        if ((quaterCounter == totalCounter) && (timeLeft < 0))
        {
            if (!gameOver)
            {
                gameOver = true;
                gameWinner();
                
            }
            
        }
    }

    public void gameWinner()
    {        
            if(scoreA > scoreB)
            {
                TournamentScript.semiFinal.Add(TournamentScript.semiCounter, TournamentScript.currentTeams[TournamentScript.teamCount-2]);
                Debug.Log("Team A wins");
                MatchEnded();   
            }
            else if(scoreA < scoreB)
            {
                TournamentScript.semiFinal.Add(TournamentScript.semiCounter, TournamentScript.currentTeams[TournamentScript.teamCount-1]);
                Debug.Log("Team B wins");
                MatchEnded();
            }
            else
            {
                Debug.Log("Draw");
            }     
    }
    IEnumerator LoseTime()
    {       
        while (true)
        {
            yield return new WaitForSeconds(1);
            qTimer--;
        }
    }

    public void MatchEnded()
    {
        if (TournamentScript.isTournament)
        {
            foreach (var item in TournamentScript.semiFinal)
            {
                Debug.Log(item.Value.TeamName);

            }
            TournamentScript.matchNumber++;          
            TournamentScript.semiCounter++;
        }
        canvasObject.SetActive(true);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void LoadTournament()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
