using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointUser : MonoBehaviour, IWaypointUser
{
    public WaypointTest<WaypointUser> waypointTest;
    public Transform[] houserooms;
    public Transform[] myWaypoints = new Transform[6];
    public Transform currentHouseRoom;
    public bool systemActive;
    [SerializeField]
    bool shouldMove;
    [SerializeField]
    bool shouldStartOver;
    [SerializeField]
    float timer;
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
        waypointTest.Init(this, myWaypoints);
        waypointTest.AddNewUser(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        systemActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (systemActive)
        {
            waypointTest.Move(this);
            
            timer += Time.deltaTime;
            /*
            if (timer >= timeToMove)
            {
                waypointTest.GetNextWayPoint(timeToMove, 1);
                timer = 0;
            }*/
        }
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        var currentObjColl = collision.gameObject;
        switch (currentHouseRoom)
        {
            case CompareTag("OrangeRoom"):
                currentHouseRoom = collision.transform;
                break;          
            case collision.gameObject.CompareTag("RedRoom"):
                currentHouseRoom = collision.transform;
                break;          
            case collision.gameObject.CompareTag("GreenRoom"):
                currentHouseRoom = collision.transform;
                break;          
            case collision.gameObject.CompareTag("WhiteRoom"):
                currentHouseRoom = collision.transform;

                break;
        }
    
    }*/
}
