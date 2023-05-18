using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointStm : MonoBehaviour
{
    [SerializeField]
    Transform waypointSource;
    public Transform[] waypoints;
    [SerializeField]
    bool shouldStartOver;
    bool goBack = false;
    public int waypointPosIndex;
    public Transform currentWaypoint;
    [SerializeField]
    float timer;
    [SerializeField]
    float spanForNextWp;
     
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waypointSource.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spanForNextWp)
        {
            GetNextWayPoint();
            timer = 0;
        }
    }
    public void GetNextWayPoint()
    {
        if (shouldStartOver)
        {
            if (waypointPosIndex + 1 < waypoints.Length)
            {
                waypointPosIndex += 1;
            }
            else
            {
                waypointPosIndex = 0;
            }
        }
        else if (!shouldStartOver)
        {
            if (waypointPosIndex + 1 < waypoints.Length && !goBack)
            {
                waypointPosIndex += 1;
            }
            else if (waypointPosIndex + 1 >= waypoints.Length && !goBack)
            {
                waypointPosIndex -= 1;

                goBack = true;
            }
            else if (goBack && waypointPosIndex - 1 >= 0)
            {

                waypointPosIndex -= 1;
            }
            else if (goBack && waypointPosIndex - 1 < 0)
            {

                waypointPosIndex = 0;
                goBack = false;
            }
        }
        
        currentWaypoint = waypoints[waypointPosIndex];
        
    }
}
