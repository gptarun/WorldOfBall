using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private readonly float jumpPower = 60f;
    private Rigidbody2D rigidBody2D;
    private GameObject ballGameObject;
    private BallMovementMouse ballScript;
    private Vector3 jumpCoordinates;
    [HideInInspector] public bool teamAJump = false;
    [HideInInspector] public bool teamBJump = false;
    private float screenWidth;
    public SinglePlayerController singlePlayerController;
    private bool isGrounded;
    private readonly float jumpConstant = 2f;
    [HideInInspector] public bool rotateHand;
    private float z;
    private float rotationSpeed;
    private bool rotateA;
    private bool rotateB;
    private bool antiRotate;

    //private HingeJoint2D hingeJoint;

    private void Start()
    {
        screenWidth = Screen.width;
        rigidBody2D = GetComponent<Rigidbody2D>();
        z = 0.0f;
        rotationSpeed = 400.0f;
        antiRotate = false;
        initializeObjects();
    }
    private void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].position.x < screenWidth / 2 && isGrounded)
            {
                if (singlePlayerController.teamAMode.Equals("human"))
                {
                    teamAJump = true;
                    GetJumpA();
                    rotateA = true;
                }
                else if (singlePlayerController.teamBMode.Equals("human") && isGrounded)
                {
                    teamBJump = true;
                    GetJumpB();
                    rotateB = true;
                }

            }
            else if (Input.touches[i].position.x > screenWidth / 2 && isGrounded)
            {
                if (singlePlayerController.teamBMode.Equals("human"))
                {
                    teamBJump = true;
                    GetJumpB();
                    rotateB = true;
                }
                else if (singlePlayerController.teamAMode.Equals("human") && isGrounded)
                {
                    GetJumpA();
                    rotateA = true;
                    teamAJump = true;
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
        if (ballGameObject == null)
        {
            initializeObjects();
        }
    }

    private void RotateHandA()
    {
        if (this.gameObject.tag.Equals("TeamA"))
        {
            if (!antiRotate)
            {
                z -= Time.deltaTime * rotationSpeed;
            }
            if (z < -180.0f)
            {
                antiRotate = true;
            }
            if (antiRotate)
            {
                z += Time.deltaTime * rotationSpeed;
            }
            this.gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, z);
            if (z >= 0)
            {
                rotateA = false;
                z = 0;
                antiRotate = false;
            }
        }
    }

    private void RotateHandB()
    {
        if (this.gameObject.tag.Equals("TeamB"))
        {
            if (!antiRotate)
            {
                z -= Time.deltaTime * rotationSpeed;
            }
            if (z < -180.0f)
            {
                antiRotate = true;
            }
            if (antiRotate)
            {
                z += Time.deltaTime * rotationSpeed;
            }
            this.gameObject.transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, z);
            if (z >= 5)
            {
                rotateB = false;
                z = 0;
                antiRotate = false;
            }
        }
    }

    public void GetJumpA()
    {
        if (this.gameObject.tag.Equals("TeamA"))
        {
            jumpCoordinates = ballGameObject.transform.position - transform.position;
            jumpCoordinates.y = jumpConstant;
            jumpCoordinates.x = jumpCoordinates.x / 2;
            rigidBody2D.AddForce(jumpCoordinates * jumpPower);
            isGrounded = false;

        }
    }
    public void GetJumpB()
    {
        if (this.gameObject.tag.Equals("TeamB"))
        {
            jumpCoordinates = ballGameObject.transform.position - transform.position;
            jumpCoordinates.y = jumpConstant;
            jumpCoordinates.x = jumpCoordinates.x / 2;
            rigidBody2D.AddForce(jumpCoordinates * jumpPower);
            isGrounded = false;

        }
    }
    void initializeObjects()
    {
        ballGameObject = GameObject.Find("basketball");
        ballScript = ballGameObject.GetComponent<BallMovementMouse>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.transform.name.Contains("Body"))
        {
            isGrounded = true;
        }
    }
}