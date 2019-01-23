using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TournamentScript : MonoBehaviour {
    public static bool isTournament = true;
    public static int matchNumber = 0;
    private readonly List<string> teamList = new List<string>(new string[] { "Africa", "Argentina", "Australia", "Brazil", "China", "France", "India", "Mexico", "Philippines", "Russia", "Serbia", "Singapore", "Spain", "Thailand", "United States", "Yugoslavia" });
    private readonly List<string> teamListShort = new List<string>(new string[] { "Afr", "Arg", "Aus", "Bra", "Chi", "Fra", "Ind", "Mex", "Phi", "Rus", "Ser", "Sin", "Spa", "Tha", "Usa", "Yug" });    
    //Tournament config params
    [SerializeField] private Image[] flagImages;
    private Dictionary<int,TeamScript> TeamData = new Dictionary<int, TeamScript>();
    public static Dictionary<int, TeamScript> currentTeams = new Dictionary<int, TeamScript>();
    public static Dictionary<int, TeamScript> semiFinal = new Dictionary<int, TeamScript>();
    public static int semiCounter = 1;
    public static Dictionary<int, TeamScript> final = new Dictionary<int, TeamScript>();
    public static int finalCounter = 1;
    private TeamScript[] team = new TeamScript[8];
    public static int teamCount = 0;
    // Use this for initialization
    private void Awake()
    {
        int gameInstanceCount = FindObjectsOfType<TournamentScript>().Length;
        if(gameInstanceCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start () {
        Debug.Log(matchNumber);
        if (matchNumber == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i == 0)
                {
                    team[i] = new TeamScript(teamList[i], teamListShort[i], "Human", flagImages[i]);
                    TeamData.Add(i, team[i]);
                }
                else
                {
                    team[i] = new TeamScript(teamList[i], teamListShort[i], "Bot", flagImages[i]);
                    TeamData.Add(i, team[i]);
                }
            }
            matchNumber++;
        }
        //Initializing the flags of Semi Final game
        if(semiFinal.Count != 0)
        {
            foreach (var item in semiFinal)
            {
                if (item.Key == 1)
                    GameObject.Find("SemiA").transform.Find("TeamA").GetComponent<Image>().overrideSprite = item.Value.Flag.sprite;
                if (item.Key == 2)
                    GameObject.Find("SemiA").transform.Find("TeamB").GetComponent<Image>().overrideSprite = item.Value.Flag.sprite;
                if (item.Key == 3)
                    GameObject.Find("SemiB").transform.Find("TeamA").GetComponent<Image>().overrideSprite = item.Value.Flag.sprite;
                if (item.Key == 4)
                    GameObject.Find("SemiB").transform.Find("TeamB").GetComponent<Image>().overrideSprite = item.Value.Flag.sprite;
            }            
            
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}    
    public void StartTournament()
    {
        if (matchNumber > 0 && matchNumber <= 4)
        {
            currentTeams.Clear();
            currentTeams.Add(teamCount, TeamData[teamCount]);
            teamCount++;
            currentTeams.Add(teamCount, TeamData[teamCount]);
            teamCount++;           
            PlayMatch();
        }
        if(matchNumber >= 5 && matchNumber <= 6)
        {
            Debug.Log(teamCount);
            currentTeams.Clear();
            currentTeams.Add(teamCount, semiFinal[teamCount]);
            teamCount++;
            currentTeams.Add(teamCount, semiFinal[teamCount]);
            teamCount++;
            PlayMatch();
        }
        if(matchNumber == 7)
        {

        }
        SetWinner();
    }

    private void PlayMatch()
    {
        Debug.Log("In game");
    }

    private void SetWinner()
    {
        //Need to end Tournament here
    }

    public void SetTeam(string teamAName, string teamBName, string modeA, string modeB, Sprite flagA, Sprite flagB, string currButtonName)
    {       
        switch (currButtonName){
            case "GroupA":            
                SetTeamGeneric(teamAName,teamBName,modeA,modeB,flagA,flagB,currButtonName,0,1);   
                break;
            case "GroupB":
                SetTeamGeneric(teamAName, teamBName, modeA, modeB, flagA, flagB, currButtonName, 2, 3);
                break;
            case "GroupC":
                SetTeamGeneric(teamAName, teamBName, modeA, modeB, flagA, flagB, currButtonName, 4, 5);
                break;
            case "GroupD":
                SetTeamGeneric(teamAName, teamBName, modeA, modeB, flagA, flagB, currButtonName, 6, 7);
                break;
            default:
                Debug.Log("Invalid Object");
                break;
        }
        
    }

    private void SetTeamGeneric(string teamAName, string teamBName, string modeA, string modeB, Sprite flagA, Sprite flagB, string currButtonName, int index1, int index2)
    {
        team[index1].Flag = GameObject.Find(currButtonName).transform.Find("TeamA").GetComponent<Image>();
        team[index1].Flag.overrideSprite = flagA;
        team[index1].TeamName = teamAName;
        team[index1].ShortName = teamAName.Substring(0, 4);
        team[index1].Mode = modeA;

        team[index2].Flag = GameObject.Find(currButtonName).transform.Find("TeamB").GetComponent<Image>();
        team[index2].Flag.overrideSprite = flagB;
        team[index2].TeamName = teamBName;
        team[index2].ShortName = teamBName.Substring(0, 4);
        team[index2].Mode = modeB;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
