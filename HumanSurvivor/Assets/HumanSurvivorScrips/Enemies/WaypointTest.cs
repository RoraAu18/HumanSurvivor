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
public class WaypointTest<WaypointUser> where WaypointUser : Component
{
    public Transform[] waypoints;
    Transform currentWaypoint;
    [SerializeField]
    float moveSpeed;    
    [SerializeField]
    float rotSpeed;
    int waypointPosIndex;
    [SerializeField]
    float targetTime;
    float timer;
    WaypointUser owner;
    [SerializeField]
    public List<IWaypointUser> waypointUsers = new List<IWaypointUser>();
    bool goBack = false;
    public void Init(WaypointUser _owner, Transform[] ownerWaypoints)
    {
        owner = _owner;
        Debug.Log(owner + " " + waypoints.Length);
        waypoints = ownerWaypoints;
        waypoints[0].position = owner.transform.position;
        waypointPosIndex = 0;
        currentWaypoint = waypoints[waypointPosIndex];
        owner.transform.position = currentWaypoint.position;
    }
    public void AddNewUser(IWaypointUser user)
    {
        if (!waypointUsers.Contains(user))
        {
            waypointUsers.Add(user);
        }
    }
    public void RemoveUser(IWaypointUser user)
    {
        waypointUsers.Remove(user);
    }

    public void Move(WaypointUser owner)
    {
        currentWaypoint = waypoints[waypointPosIndex];
        Debug.Log(currentWaypoint);
        owner.transform.LookAt(currentWaypoint.transform);
        Vector3 dir = currentWaypoint.position - owner.transform.position;
        owner.transform.position += dir * moveSpeed * Time.deltaTime;
        Quaternion lookTo = Quaternion.LookRotation(currentWaypoint.position);
        Quaternion.Slerp(owner.transform.rotation, lookTo, rotSpeed * Time.deltaTime);
        if(dir.magnitude < 1f)
        {
            /*
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
            }*/
            for (int i = 0; i < waypointUsers.Count; i++)
            {
                if (waypointUsers[i].ShouldChangeWaypoint())
                {
                    GetNextWayPoint(waypointUsers[i].TimeToMove(), waypointUsers.IndexOf(waypointUsers[i]));
                }
            }
        }
    }

    public void GetNextWayPoint(float time, int userIdx)
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


