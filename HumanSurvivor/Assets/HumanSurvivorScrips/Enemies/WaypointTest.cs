using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IWaypointUser
{
    public bool ShouldChangeWaypoint();
    public float TimeToMove();
    public bool ShouldStartOver();
}
[Serializable]
public class WaypointTest : MonoBehaviour
{
    public Transform[] waypoints ;
    Transform currentWaypoint;
    [SerializeField]
    float moveSpeed;    
    [SerializeField]
    float rotSpeed;
    int waypointPosIndex;
    [SerializeField]
    float targetTime;
    float timer;
    [SerializeField]
    public List<IWaypointUser> waypointUsers = new List<IWaypointUser>();
    bool goBack = false;


    void Start()
    {
        waypointPosIndex = 0;
        currentWaypoint = waypoints[waypointPosIndex];
        transform.position = currentWaypoint.position;
    }
    public void AddNewUser(IWaypointUser user)
    {
        if (!waypointUsers.Contains(user))
        {
            waypointUsers.Add(user);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        currentWaypoint = waypoints[waypointPosIndex];
        transform.LookAt(currentWaypoint.transform);
        Vector3 dir = currentWaypoint.position - transform.position;
        transform.position += dir * moveSpeed * Time.deltaTime;
        Quaternion lookTo = Quaternion.LookRotation(currentWaypoint.position);
        Quaternion.Slerp(transform.rotation, lookTo, rotSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < 1)
        {
            if (waypointUsers.Count <= 0)
            {
                GetNextWayPoint(targetTime, 0);
            }
            else
            {
                for (int i = 0; i < waypointUsers.Count; i++)
                {
                    if (waypointUsers[i].ShouldChangeWaypoint())
                    {
                        GetNextWayPoint(waypointUsers[i].TimeToMove(), waypointUsers.IndexOf(waypointUsers[i]));
                    }
                }
            }
        }


    }

    void GetNextWayPoint(float time, int userIdx)
    {
        if (waypointUsers[userIdx].ShouldStartOver())
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
        else if (!waypointUsers[userIdx].ShouldStartOver())
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
            else if (goBack && waypointPosIndex -1 < 0)
            {

                waypointPosIndex = 0;
                goBack = false;
            }
        }
        currentWaypoint = waypoints[waypointPosIndex];
    }

}


