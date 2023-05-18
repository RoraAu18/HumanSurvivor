using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointUser : MonoBehaviour, IWaypointUser
{
    public WaypointTest<WaypointUser> waypointTest;
    public Transform[] houserooms;
    public Transform[] myWaypoints;
    [SerializeField]
    Transform waypointSource;
    
    public Transform currentHouseRoom;
    public ColisionController colisionController;
    public bool systemActive;
    [SerializeField]
    bool shouldMove;
    [SerializeField]
    bool shouldStartOver;
    public int currentWP;
    public float timeToMove = 2;
    public bool ShouldChangeWaypoint()
    {
        return shouldMove;
    }

    public bool ShouldStartOver()
    {
        return shouldStartOver;
    }

    public float TimeToMove()
    {
        return timeToMove += Time.deltaTime;
    }

    public void Init()
    {
        waypointTest.Init(this, myWaypoints, currentHouseRoom);
        waypointTest.AddNewUser(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        systemActive = false;
        TryGetComponent(out colisionController);
        myWaypoints = waypointSource.GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (systemActive)
        {
            waypointTest.Move(this);
        }

        for (int i = 0; i < colisionController.currentFrameCollissions.Count; i++)
        {
            var currColl = colisionController.currentFrameCollissions[i];
            if (currColl.GetComponentInChildren<HouseRoom>())
            {
                currentHouseRoom = currColl.GetComponentInChildren<HouseRoom>().transform;
            }
        }
    }

}

