using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScoreController : MonoBehaviour {
    private SinglePlayerController singlePlayerController;
    private bool colHalfExit = false;
    private bool colBodyExit = false;
    private bool colOutOfBoundExit = false;
    // Use this for initialization
    void Start () {
        singlePlayerController = GameObject.Find("GameSceneObject").GetComponent<SinglePlayerController>();
    }
		
    public void OnTriggerEnter2D(Collider2D col)
    {       
        if (col.tag == "Body")
        {
            colBodyExit = false;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Score raise");
        if (col.tag == "Body")
        {
            colBodyExit = true;
        }
        if (col.tag == "Half")
        {
            colHalfExit = true;
        }
        if (col.tag.Contains("Ball") && this.gameObject.name.Contains("Basket_Team1"))
        {
            if (colHalfExit)
            {
                singlePlayerController.scoreB = singlePlayerController.scoreB + 3;
            }
            else
            {
                singlePlayerController.scoreB = singlePlayerController.scoreB + 2;
            }
        }
        if (col.tag.Contains("Ball") && this.gameObject.name.Contains("Basket_Team2"))
        {
            if (colHalfExit)
            {
                singlePlayerController.scoreA = singlePlayerController.scoreA + 3;
            }
            else
            {
                singlePlayerController.scoreA = singlePlayerController.scoreA + 2;
            }
        }
        if (col.tag == "OutOfBound")
        {
            colOutOfBoundExit = true;
        }
    }
}
