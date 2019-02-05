using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class TournamentScript : MonoBehaviour {

    //This Script is going to be used in Setting up all teams which are playing in the tournaments.

    [HideInInspector] public int matchNumber = 0;
    private readonly List<string> teamList = new List<string>(new string[] { "Africa", "Argentina", "Australia", "Brazil", "China", "France", "India", "Mexico", "Philippines", "Russia", "Serbia", "Singapore", "Spain", "Thailand", "USA", "Yugoslavia" });
    private readonly List<string> teamListShort = new List<string>(new string[] { "Afr", "Arg", "Aus", "Bra", "Chi", "Fra", "Ind", "Mex", "Phi", "Rus", "Ser", "Sin", "Spa", "Tha", "Usa", "Yug" });    
    //Tournament config params
    [SerializeField] Image[] flagImages;
    private Sprite[] flags;
    private TeamScript[] team = new TeamScript[8];
    [SerializeField] Button[] teamButtons;
    [SerializeField] TextMeshProUGUI winnerName;
    public TournamentController tournamentController;
    [HideInInspector] public MatchDay[] quarterFinal = new MatchDay[4];
    [HideInInspector] public MatchDay[] semiFinal = new MatchDay[2];
    [HideInInspector] public MatchDay[] final = new MatchDay[1];
    private TeamScript teamA = null;
    private TeamScript teamB = null;
    private TeamScript tournamentWinner;
    [SerializeField] Button playButton;
    // Use this for initialization   
    void Start () {
        Debug.Log(matchNumber);
        flags = Resources.LoadAll<Sprite>("Flags");
        if (matchNumber == 0)
        {
            for (int i = 0; i < 8; i+=2)
            {
                TeamScript teamA=null;
                TeamScript teamB=null;
                String groupName = "Group" + (char)('A' + (int)(i / 2));
                Image imageTeamA = SetFlagSpriteoFTeam( groupName, "TeamA", flags[i]);
                Image imageTeamB = SetFlagSpriteoFTeam(groupName, "TeamB", flags[i+1]);
                if (i == 0)
                {
                    teamA = new TeamScript(teamList[i], teamListShort[i], "Human", imageTeamA);
                    teamB = new TeamScript(teamList[i+1], teamListShort[i+1], "Bot", imageTeamB);
                }
                else
                {
                    teamA = new TeamScript(teamList[i], teamListShort[i], "Bot", imageTeamA);
                    teamB = new TeamScript(teamList[i+1], teamListShort[i+1], "Bot", imageTeamB);
                }
                MatchDay matchDay = new MatchDay(teamA,teamB, groupName);
                quarterFinal[i / 2] = matchDay;
            }
        }
        
    }
	
	void Update () {
		
	}    
    public void StartTournament()
    {
        if (matchNumber >= 0 && matchNumber < 4)
        {
            tournamentController.initializeSharedObjects(2, 0,"quarterFinal");
            tournamentController.SetTeams(quarterFinal[matchNumber]);
        }
        if (matchNumber >= 4 && matchNumber < 6)
        {
            tournamentController.initializeSharedObjects(0, 2,"semiFinal");
            tournamentController.SetTeams(semiFinal[matchNumber-4]);
        }
        if(matchNumber == 6)
        {
            tournamentController.initializeSharedObjects(2, 5, "final");
            tournamentController.SetTeams(final[0]);
            Debug.Log("yeah Final");
        }
        matchNumber++;
        if (GameObject.Find("MenuBg") != null)
        {
            Destroy(GameObject.Find("MenuBg"));
        }
    }

    private void SetWinner()
    {
        //Need to end Tournament here
    }

    public void SetTeam(MatchDay matchDay)
    {
        for (int i = 0; i < quarterFinal.Length; i++)
        {
            if (quarterFinal[i].CurrButtonName.Equals(matchDay.CurrButtonName))
            {
                quarterFinal[i]=matchDay;
            }
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SetTeamForNextGame()
    {
        if (matchNumber >= 1 && matchNumber < 5)
        {
            setSemiFinalTeams(matchNumber);
        }
        else if (matchNumber >= 5 && matchNumber < 7)
        {
            if ((matchNumber-5) % 2 == 0)
            {
                teamA = new TeamScript(semiFinal[matchNumber - 5].MatchDayResult.Winner);
                string groupName = "Final";
                teamA.Flag = SetFlagSpriteoFTeam(groupName, "TeamA", teamA.Flag.sprite);
            }
            else
            {
                teamB = new TeamScript(semiFinal[matchNumber - 5].MatchDayResult.Winner);
                string groupName = "Final";
                teamB.Flag = SetFlagSpriteoFTeam(groupName, "TeamB", teamB.Flag.sprite);
                final[(matchNumber - 5) / 2] = new MatchDay(teamA, teamB, groupName);
            }
        }
        else if (matchNumber == 7)
        {
            tournamentWinner = new TeamScript(final[0].MatchDayResult.Winner);
            string groupName = "Winner";
            tournamentWinner.Flag = SetFlagSpriteoFTeam(groupName, "winner", tournamentWinner.Flag.sprite);
            playButton.interactable = false;
            Debug.Log("And we are done");
        }
    }

    public void DisableTeamSelectionButtons()
    {
        for (int i = 0; i < teamButtons.Length; i++)
        {
            teamButtons[i].interactable = false;
        }
    }
    public Image SetFlagSpriteoFTeam(String buttonName,String gameObjectName,Sprite sprite)
    {
        Image imageTeam = GameObject.Find(buttonName).transform.Find(gameObjectName).GetComponent<Image>();
        imageTeam.overrideSprite = sprite;
        imageTeam.sprite = sprite;
        return imageTeam;
    }
    public void setSemiFinalTeams(int i)
    {
        i = (i - 1);
        if (i % 2 == 0)
        {
            teamA = new TeamScript(quarterFinal[i].MatchDayResult.Winner);
            string groupName = "Semi" + (char)('A' + (int)(i / 2));
            teamA.Flag = SetFlagSpriteoFTeam(groupName, "TeamA", teamA.Flag.sprite);
        }
        else
        {
            teamB = new TeamScript(quarterFinal[i].MatchDayResult.Winner);
            string groupName = "Semi" + (char)('A' + (int)(i / 2));
            teamB.Flag = SetFlagSpriteoFTeam(groupName, "TeamB", teamB.Flag.sprite);
            semiFinal[i / 2] = new MatchDay(teamA, teamB, groupName);
        }
    }
}
