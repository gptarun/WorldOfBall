using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectTeam : MonoBehaviour {

    private List<string> teamList = new List<string>(new string[] { "Africa", "Argentina", "Australia", "Brazil", "China", "France", "India", "Mexico", "Philippines", "Russia", "Serbia", "Singapore", "Spain", "Thailand", "USA", "Yugoslavia"});
    private readonly List<string> teamListShort = new List<string>(new string[] { "Afr", "Arg", "Aus", "Bra", "Chi", "Fra", "Ind", "Mex", "Phi", "Rus", "Ser", "Sin", "Spa", "Tha", "Usa", "Yug" });
    private readonly List<int> teamRating = new List<int>(new int[] { 2,3,2,3,2,3,2,1,2,3,2,2,3,2,3,3 });
    //Choosing Teams
    [SerializeField] public TextMeshProUGUI teamAChoice;
    [SerializeField] public TextMeshProUGUI teamBChoice;
    //Choosing Human or AI mode
    [SerializeField] Button changeModeA;
    private string modeA;
    [SerializeField] Button changeModeB;
    private string modeB;
    private int modeCounterA = 0;
    private int modeCounterB = 1;
    //Chaning sprite AI to Human and vice-versa
    public Sprite[] mode;
    private int indexA = 9;
    private int indexB = 9;
    //Calculating stars
    public GameObject[] teamAStar;
    public GameObject[] teamBStar;
    //Loading flags
    public Sprite[] flags;
    [SerializeField] public Button teamAFlag;
    [SerializeField] public Button teamBFlag;
    [SerializeField] TournamentScript tournament;
    //Storing Team Data into Dictionary
    //Dictionary<int, TeamScript> teamData = new Dictionary<int, TeamScript>();
    string currButtonName;
    [SerializeField] SinglePlayerController singlePlayer;

    void Start () {
        teamAChoice.text = teamList[indexA].ToString();
        teamBChoice.text = teamList[indexA].ToString();
        changeModeA.image.sprite = mode[modeCounterA];
        changeModeB.image.sprite = mode[modeCounterB];
        flags = Resources.LoadAll<Sprite>("Flags");
        modeA = "human";
        modeB = "bot";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RighButtonA()
    {
        indexA++;
        if (indexA >= teamList.Count)
        {
            indexA = 0;
        }
        teamAChoice.text = teamList[indexA].ToString();
        RankCalculation(indexA, teamAStar);
        SetFlagoFTeam(indexA, teamAFlag);
    }
    public void LeftButtonA()
    {
        indexA--;
        if (indexA <= 0)
        {
            indexA = teamList.Count - 1;
        }
        teamAChoice.text = teamList[indexA].ToString();
        RankCalculation(indexA, teamAStar);
        SetFlagoFTeam(indexA, teamAFlag);

    }
    public void RighButtonB()
    {
        indexB++;
        if (indexB >= teamList.Count)
        {
            indexB = 0;
        }
        teamBChoice.text = teamList[indexB].ToString();
        RankCalculation(indexB, teamBStar);
        SetFlagoFTeam(indexB, teamBFlag);
    }
    public void LeftButtonB()
    {
        indexB--;
        if (indexB <= 0)
        {
            indexB = teamList.Count - 1;
        }
        teamBChoice.text = teamList[indexB].ToString();
        RankCalculation(indexB, teamBStar);
        SetFlagoFTeam(indexB, teamBFlag);
    }
    public void SelectModeOnA()
    {
        modeCounterA++;
        if (modeCounterA % 2 == 0)
        {
            changeModeA.image.sprite = mode[0];
            modeA = "human";
}
        else
        {
            changeModeA.image.sprite = mode[1];
            modeA = "bot";
        }
    }
    public void SelectModeOnB()
    {
        modeCounterB++;
        if (modeCounterB % 2 == 0)
        {
            changeModeB.image.sprite = mode[0];
            modeB = "human";
        }
        else
        {
            changeModeB.image.sprite = mode[1];
            modeB = "bot";
        }
    }

    public void RankCalculation(int value, GameObject[] gameObj)
    {
        ResetRank(gameObj);
        for (int i = 0; i < teamRating[value]; i++)
        {
            gameObj[i].SetActive(true);
        }        
    }
    public void ResetRank(GameObject[] gameObj)
    {
        for (int i = 0; i < 3; i++)
        {
            gameObj[i].SetActive(false);
        }
    }
    public void SetFlagoFTeam(int flagCount,Button flagButton)
    {
        flagButton.image.sprite = flags[flagCount];
    }

    public void SetTeamForTournament()
    {
        //tournament.SetTeam(teamList[indexA].ToString(), teamList[indexB].ToString(),modeA,modeB,teamAFlag.image.sprite,teamBFlag.image.sprite, currButtonName);
        Image imageTeamA = SetFlagSpriteoFTeam(currButtonName, "TeamA", teamAFlag.image.sprite);
        Image imageTeamB = SetFlagSpriteoFTeam(currButtonName, "TeamB", teamBFlag.image.sprite);
        TeamScript teamA = new TeamScript(teamList[indexA].ToString(), teamListShort[indexA].ToString(), modeA, imageTeamA);
        TeamScript teamB = new TeamScript(teamList[indexB].ToString(), teamListShort[indexB].ToString(), modeB, imageTeamB);
        tournament.SetTeam(new MatchDay(teamA, teamB, currButtonName));
    }

    public void SetTeamForSingleMatch()
    {
        singlePlayer.SetTeams(teamList[indexA].ToString(), teamList[indexB].ToString(), modeA, modeB);
    }

    public void buttonClickedName(string buttonName)
    {
        currButtonName = buttonName;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public Image SetFlagSpriteoFTeam(string buttonName, string gameObjectName, Sprite sprite)
    {
        Image imageTeam = GameObject.Find(buttonName).transform.Find(gameObjectName).GetComponent<Image>();
        imageTeam.overrideSprite = sprite;
        imageTeam.sprite = sprite;
        return imageTeam;
    }
}
