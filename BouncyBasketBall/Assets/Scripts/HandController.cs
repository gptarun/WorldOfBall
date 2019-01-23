using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject hand;
    private Transform handPivot;
    private GameObject ballGameObject;
    private BallMovementMouse ballScript;
    void Start()
    {
        handPivot = transform.Find("handPivot");
        ballGameObject = GameObject.Find("basketball");
        ballScript = ballGameObject.GetComponent<BallMovementMouse>();
    }

    void Update()
    {
        
    }


}


