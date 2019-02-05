using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScoreController : MonoBehaviour {
    //Code moved to the Ball controller
  /*  private SinglePlayerController singlePlayerController;
    private int basketCounter = 0;
    public GameObject scoreAnim;
    private GameObject basketball;
    public AudioSource scoreAudio;
    // Use this for initialization
    void Start () {
        singlePlayerController = GameObject.Find("GameSceneObject").GetComponent<SinglePlayerController>();
        basketball = GameObject.Find("basketball");
        scoreAudio = GetComponent<AudioSource>();
    }
		
    public void OnTriggerEnter2D(Collider2D col)
    {
        basketCounter++;
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (basketCounter == 2)
        {            
            if (col.tag.Contains("Ball") && this.gameObject.name.Contains("Basket_Team1"))
            {
                if (scoreAudio != null)
                    scoreAudio.Play();
                StartCoroutine("MakeUserScore");
                singlePlayerController.scoreB = singlePlayerController.scoreB + 3;
            }
            if (col.tag.Contains("Ball") && this.gameObject.name.Contains("Basket_Team2"))
            {
                if (scoreAudio != null)
                    scoreAudio.Play();
                StartCoroutine("MakeUserScore");
                singlePlayerController.scoreA = singlePlayerController.scoreA + 3;
            }
            basketCounter = 0;
        }
        else
        {
            basketCounter = 0;
        }
    }

    IEnumerator MakeUserScore()
    {
        basketball.GetComponent<CircleCollider2D>().enabled = false;
        singlePlayerController.ResetPositions();
        basketball.GetComponent<CircleCollider2D>().enabled = true;
        scoreAnim.SetActive(true);
        yield return new WaitForSeconds(1);
        scoreAnim.SetActive(false);     
    }*/
}
