using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OptionMenuScript : MonoBehaviour {
    //All option menu UI
    //number of quaters in game
    [SerializeField] TextMeshProUGUI noQuater;
    //Difficulty level of Game Easy or Hard
    [SerializeField] TextMeshProUGUI difficulty;
    List<string> diffLevel = new List<string>(new string[] {"EASY","HARD" });
    //Make sound effects ON/OFF
    [SerializeField] Toggle sound;
    //Set team size - 1, 2 or 3
    [SerializeField] TextMeshProUGUI teamSize;    
    //Set quater time 30, 60, 90
    [SerializeField] TextMeshProUGUI quaterTime;
    //Switch side after Number of quaters/2
    [SerializeField] Toggle switchSide;
    //Make game music ON/OFF
    [SerializeField] Toggle music;
    //All static variables
    public static int quaterCounter = 4;
    public static int teamSizeCounter = 3;
    public static string difficultyLevel = "EASY";
    public static bool isSound = true;
    public static bool isMusic = true;
    public static int quaterDuration = 90;
    // Use this for initialization
    void Start () {
        noQuater.text = quaterCounter.ToString();
        difficulty.text = diffLevel[0];
        teamSize.text = teamSizeCounter.ToString();
        quaterTime.text = quaterDuration.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onToggleSound()
    {
        FindObjectOfType<AudioSource>().mute = !sound.isOn;        
    }

    public void ChangeQuaterNumber()
    {
        quaterCounter--;
        if(quaterCounter == 0)
        {
            quaterCounter = 4;
        }
        noQuater.text = quaterCounter.ToString();
    }

    public void ChangeDifficulty()
    {
        if (difficulty.text.Equals("EASY")){
            difficulty.text = diffLevel[0];
            difficultyLevel = diffLevel[0];
        }
        else
        {
            difficulty.text = diffLevel[1];
            difficultyLevel = diffLevel[1];
        }
    }

    public void ChangeTeamSize()
    {
        teamSizeCounter--;
        if (teamSizeCounter == 0)
        {
            teamSizeCounter = 3;
        }
        teamSize.text = teamSizeCounter.ToString();
    }

    public void ChangeQuaterTime()
    {
        quaterDuration = quaterDuration + 30;
        if(quaterDuration >= 120)
        {
            quaterDuration = 30;
        }
        quaterTime.text = quaterDuration.ToString();
    }
}
