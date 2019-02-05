using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private bool jumpA;
    private bool jumpB;
    private bool isGrounded;
    public SinglePlayerController singlePlayerController;
    private float screenWidth;
    private GameObject ballGameObject;
    private BallController ballScript;
    AudioSource jumpPlayerAudio;
    private float  fallMultiplier = 1.5f;

    // Use this for initialization
    void Start () {
        screenWidth = Screen.width;
        ballGameObject = GameObject.Find("basketball");
        ballScript = ballGameObject.GetComponent<BallController>();
        jumpPlayerAudio = GetComponent<AudioSource>();
        isGrounded = true;
    }

    void Update()
    {
        //For Bot movement
        if (singlePlayerController.teamAMode.Equals("bot"))
        {
            if (this.transform.tag.Equals("TeamA"))
            {
                StartCoroutine(BotJump(2));
            }
        }
        if (singlePlayerController.teamBMode.Equals("bot"))
        {
            if (this.transform.tag.Equals("TeamB") && isGrounded)
            {
                StartCoroutine(BotJump(2));
            }
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touchA = Input.GetTouch(i);
            if (singlePlayerController.teamAMode.Equals("human") && singlePlayerController.teamBMode.Equals("human"))
            {
                if (touchA.position.x < screenWidth / 2)
                {
                    if (touchA.phase.Equals(TouchPhase.Began) && this.transform.tag.Equals("TeamA") && isGrounded)
                    {
                        this.gameObject.GetComponent<Rigidbody2D>().velocity = CalculateJumpDistance(ballGameObject, 5);
                        isGrounded = false;
                    }
                }
                else if (touchA.position.x > screenWidth / 2)
                {
                    if (touchA.phase.Equals(TouchPhase.Began) && this.transform.tag.Equals("TeamB") && isGrounded)
                    {
                        this.gameObject.GetComponent<Rigidbody2D>().velocity = CalculateJumpDistance(ballGameObject, 5);
                        isGrounded = false;
                    }
                }
            }
            else if (singlePlayerController.teamAMode.Equals("human") && singlePlayerController.teamBMode.Equals("bot"))
            {
                if (touchA.phase.Equals(TouchPhase.Began) && this.transform.tag.Equals("TeamA") && isGrounded)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = CalculateJumpDistance(ballGameObject, 5);
                    isGrounded = false;
                }
            }
            else if (singlePlayerController.teamAMode.Equals("bot") && singlePlayerController.teamBMode.Equals("human"))
            {
                if (touchA.phase.Equals(TouchPhase.Began) && this.transform.tag.Equals("TeamB") && isGrounded)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = CalculateJumpDistance(ballGameObject, 5);
                    isGrounded = false;
                }
            }
        }            
    }   

    public Vector3 CalculateJumpDistance(GameObject anyObject, float height)
    {
        if (jumpPlayerAudio != null)
            jumpPlayerAudio.Play();
        Vector3 jumpDis = new Vector3(0, 0, 0);
        float maxHspeed = 2;
        float g = Physics.gravity.magnitude; // get the gravity value
        float vSpeed = height; // calculate the vertical speed
        float totalTime = 6 * vSpeed / g; // calculate the total time
        float maxDistance = Vector3.Distance(anyObject.transform.position, this.transform.position);
        float hSpeed = maxDistance / totalTime * 2; // calculate the horizontal speed
        if ((anyObject.transform.position.x - this.transform.position.x) < 0)
        {
            if (hSpeed < -maxHspeed)
            {
                hSpeed = -maxHspeed;
            }
            else
            {
                hSpeed = -hSpeed;
            }
        }
        else
        {
            if (hSpeed > maxHspeed)
            {
                hSpeed = maxHspeed;
            }
        }
        /*  
         *  This script will allow only all the players will jump
         *  But anyone of same team has the ball will jump
         *  opponent will jump
         */ 
        if (ballScript.attached)
        {
            if (this.gameObject.transform.parent.name.Equals(ballScript.attachTagName))     
            {
                if (this.gameObject.name.Equals(ballScript.attachParentName))
                {
                    jumpDis = new Vector3(hSpeed, vSpeed, 0);
                }
                else
                {
                    jumpDis = new Vector3(0, 0, 0);
                }
            }
            else
            {
                jumpDis = new Vector3(hSpeed, vSpeed, 0);
            }
        }
        else
        {
            jumpDis = new Vector3(hSpeed, vSpeed, 0);
        }
        return jumpDis;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("ground"))
        {
            isGrounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag.Contains("ground"))
        {
            isGrounded = false;
        }
    }
    
    IEnumerator BotJump(float time)
    {
        yield return new WaitForSeconds(time);
        if (this.isGrounded)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = CalculateJumpDistance(ballGameObject, 5);
        }
    }
}
