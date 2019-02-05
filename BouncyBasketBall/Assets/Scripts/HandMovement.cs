using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour {
    private bool rotateA;
    private bool rotateB;
    private bool antiRotateA;
    private bool antiRotateB;
    [HideInInspector] public bool botAntiRotateA;
    [HideInInspector] public bool botAntiRotateB;
    public SinglePlayerController singlePlayerController;
    private float screenWidth;
    private GameObject ballGameObject;
    private BallController ballScript;
    private float rotationSpeed;
    private float angleA;
    private float angleB;

    // Use this for initialization
    void Start () {
        screenWidth = Screen.width;
        angleA = 0.0f;
        angleB = 0.0f;
        antiRotateA = false;
        antiRotateB = false;
        botAntiRotateA = false;
        botAntiRotateB = false;
        rotateA = false;
        rotateB = false;
        rotationSpeed = 500.0f;
        ballGameObject = GameObject.Find("basketball");
        ballScript = ballGameObject.GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update ()
    {
        //For Bot movement
        if (singlePlayerController.teamAMode.Equals("bot"))
        {
            if (!this.rotateA && !this.antiRotateA && !this.botAntiRotateA)
            {
                StartCoroutine(RotateBotA(2));
            }
            if (botAntiRotateA)
            {
                this.antiRotateA = true;
            }
        }
        if (singlePlayerController.teamBMode.Equals("bot"))
        {
            if (!this.rotateB && !this.antiRotateB && !this.botAntiRotateB)
            {
                StartCoroutine(RotateBotB(2));
            }
            if (botAntiRotateB)
            {
                this.antiRotateB = true;
            }
        }

        //For human movement
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touchA = Input.GetTouch(i);
            if (singlePlayerController.teamAMode.Equals("human") && singlePlayerController.teamBMode.Equals("human"))
            {
                if (touchA.position.x < screenWidth / 2 )
                {
                    if (touchA.phase.Equals(TouchPhase.Began))
                    {
                        antiRotateA = false;
                        rotateA = true;
                    }
                    if (touchA.phase.Equals(TouchPhase.Ended))
                    {
                        antiRotateA = true;
                    }
                }
                else if (touchA.position.x > screenWidth / 2)
                {
                    if (touchA.phase.Equals(TouchPhase.Began))
                    {
                        antiRotateB = false;
                        rotateB = true;
                    }
                    if (touchA.phase.Equals(TouchPhase.Ended))
                    {
                        antiRotateB = true;
                    }
                }
            }
            else if (singlePlayerController.teamAMode.Equals("human"))
            {
                if (touchA.phase.Equals(TouchPhase.Began))
                {
                    antiRotateA = false;
                    rotateA = true;
                }
                if (touchA.phase.Equals(TouchPhase.Ended))
                {
                    antiRotateA = true;
                }
            }
            else if (singlePlayerController.teamBMode.Equals("human"))
            {
                if (touchA.phase.Equals(TouchPhase.Began))
                {
                    antiRotateB = false;
                    rotateB = true;
                }
                if (touchA.phase.Equals(TouchPhase.Ended))
                {
                    antiRotateB = true;
                }
            }
        }
        if (rotateA)
        {
            RotateHandA();
        }
        if (rotateB)
        {
            RotateHandB();
        }
        if (ballScript.attached && this.gameObject.name.Equals(ballScript.attachName))
        {
            ballGameObject.transform.position = this.transform.Find("handPivot").position ;
        }
    }

    private void RotateHandA()
    {
        if (transform.parent.transform.parent.name.Equals("TeamA"))
        {
            if (!antiRotateA && angleA < 180.0f)
            {
                angleA += Time.deltaTime * rotationSpeed;
                if (angleA > 180f)
                {
                    angleA = 180f;
                    botAntiRotateA = true;
                }
            }
            if (antiRotateA)
            {
                angleA -= Time.deltaTime * rotationSpeed;
                if (angleA <= 0)
                {
                    angleA = 0;
                    botAntiRotateA = false;
                }
            }
            transform.localRotation = Quaternion.Euler(0, 0, angleA);
            if (angleA <= 0)
            {
                rotateA = false;
                antiRotateA = false;
            }
        }
    }
    private void RotateHandB()
    {
        if (transform.parent.transform.parent.name.Equals("TeamB"))
        {
            if (!antiRotateB && angleB > -180.0f)
            {
                angleB -= Time.deltaTime * rotationSpeed;
                if (angleB < -180f)
                {
                    angleB = -180f;
                    botAntiRotateB = true;
                }
            }
            if (antiRotateB)
            {
                angleB += Time.deltaTime * rotationSpeed;
                if (angleB > 0)
                {
                    angleB = 0;
                    botAntiRotateB = false;
                }
            }
            transform.localRotation = Quaternion.Euler(0, 0, angleB);            
            if (angleB >= 0)
            {
                rotateB = false;
                antiRotateB = false;
            }
        }
    }

    IEnumerator RotateBotA(float time)
    {
        yield return new WaitForSeconds(time);
        this.antiRotateA = false;
        this.rotateA = true;
    }

    IEnumerator RotateBotB( float time)
    {
        yield return new WaitForSeconds(time);
        this.antiRotateB = false;
        this.rotateB = true;
    }
}
