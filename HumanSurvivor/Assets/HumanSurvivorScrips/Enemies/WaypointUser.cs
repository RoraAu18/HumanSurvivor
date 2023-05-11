using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointUser : MonoBehaviour, IWaypointUser
{
    [SerializeField]
    WaypointTest waypointSystem;
    [SerializeField]
    bool shouldMove;
    [SerializeField]
    bool shouldStartOver;
    public float timeToMove;
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


    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out waypointSystem);
        waypointSystem.AddNewUser(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
