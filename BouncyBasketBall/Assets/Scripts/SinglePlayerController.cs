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
    public GameObject ExtraTimeAnim;
    float delatTime;
    public GameObject[] player = new GameObject[6];
    Vector3[] playerPosition = new Vector3[6];
    public Transform ballPosition;
    Vector3 ballInitPos;
    GameObject basketball;
    public AudioSource lastTick;
    private bool isTick;
    public AudioSource gameBell;
    public bool matchEnded;
    /// <summary>
    ///These varaibles will be used for player creation dynamically a
    /// </summary>
    [HideInInspector] public Sprite[] faces;
    [HideInInspector] public Sprite[] jersey;
    // Use this for initialization

    void Start () {
        basketball = GameObject.Find("basketball");
        pauseButtonPressed = false;
        totalQuaterCounter = OptionMenuScript.quaterCounter;
        quater.text = "Q" + quaterCounter;
        qTimer = OptionMenuScript.quaterDuration;
        quaterTimer.text = "" + qTimer;
        scoreA = 0;
        scoreB = 0;
        teamAScore.text = scoreA.ToString();
        teamBScore.text = scoreB.ToString();
        faces = Resources.LoadAll<Sprite>("Face");
        jersey = Resources.LoadAll<Sprite>("Team");        
        int indexA = Array.FindIndex(jersey, cloth => cloth.name == teamAName.text.ToString().ToUpper());
        int indexB = Array.FindIndex(jersey, cloth => cloth.name == teamBName.text.ToString().ToUpper());
        for (int i = 0; i < OptionMenuScript.teamSizeCounter*2; i++)
        {
            playerPosition[i] = player[i].transform.position;
            player[i].SetActive(true);
            if (i % 2 == 0) {
                player[i].GetComponent<SpriteRenderer>().sprite = jersey[indexA];
                int tempFace = UnityEngine.Random.Range(0, 11);
                if( tempFace == 1) { tempFace++; }
                player[i].gameObject.transform.Find("Face").GetComponent<SpriteRenderer>().sprite = faces[tempFace];
            }
            else
            {
                player[i].GetComponent<SpriteRenderer>().sprite = jersey[indexB];
                int tempFace = UnityEngine.Random.Range(0, 11);
                if (tempFace == 1) { tempFace++; }
                player[i].gameObject.transform.Find("Face").GetComponent<SpriteRenderer>().sprite = faces[tempFace];
            }                        
        }
        ballInitPos = ballPosition.position;
        StartCoroutine("LoseTime");
        delatTime = qTimer;
        Time.timeScale = 1;
        isTick = true;
        matchEnded = false;
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
                isTick = true;
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
            if (qTimer < 10 && qTimer >= 0 && lastTick != null && isTick )
            {
                isTick = false;
                lastTick.Play();
            }
            lastTick.UnPause();
        }
        else
        {
            lastTick.Pause();            
        }
    }
    //This is the basic coroutine to reset everytime when you score, foul and timer expires. Also acknowledged the timer to stop
    IEnumerator MakeUserReady()
    {
        ResetPositions();
        jumpAnim.SetActive(true);
        yield return new WaitForSeconds(2);
        jumpAnim.SetActive(false);
    }

    public void ResetPositions()
    {
        StopCoroutine("LoseTime");
        basketball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        basketball.GetComponent<Rigidbody2D>().angularVelocity = 0;
        for (int i = 0; i < player.Length; i++)
        {
            player[i].transform.position = playerPosition[i];
        }
        ballPosition.transform.position = ballInitPos;
        StartCoroutine("LoseTime");
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
        teamAName.text = teamANameSelected.Substring(0, 3);
        teamBName.text = teamBNameSelected.Substring(0, 3);
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
            StartCoroutine(Draw());
            qTimer = OptionMenuScript.quaterDuration;
        }
    }
    public void MatchEnded()
    {
        if (gameBell != null)
        {
            gameBell.Play();
        }
        pauseButton.interactable = false;
        isTick = true;        
        basketball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        StopAllCoroutines();
        canvasObject.SetActive(true);
        matchEnded = true;
    }

    IEnumerator Draw()
    {
        ResetPositions();        
        ExtraTimeAnim.SetActive(true);
        yield return new WaitForSeconds(1);
        ExtraTimeAnim.SetActive(false);
    }
}
