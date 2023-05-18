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
    Vector3 currentVelocity;
    WaypointUser owner;
    [SerializeField]
    public List<IWaypointUser> waypointUsers = new List<IWaypointUser>();
    bool goBack = false;
    public void Init(WaypointUser _owner, Transform[] ownerWaypoints, Transform currentEntrance)
    {
        owner = _owner;
        Debug.Log(owner + " " + waypoints.Length);
        waypoints = ownerWaypoints;
        waypoints[0].position = owner.transform.position;
        waypoints[1].position = currentEntrance.GetComponentInChildren<HouseRoom>().transform.position;
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
        //owner.transform.position += dir * moveSpeed * Time.deltaTime;

        var smoothVec = Vector3.SmoothDamp(owner.transform.position, currentWaypoint.transform.position, ref currentVelocity, 0.8f);
        owner.transform.position = Vector3.Lerp(smoothVec, currentWaypoint.transform.position, moveSpeed);
        Quaternion lookTo = Quaternion.LookRotation(currentWaypoint.position);
        Quaternion.Slerp(owner.transform.rotation, lookTo, rotSpeed * Time.deltaTime);
        if(Vector3.Distance(currentWaypoint.position, owner.transform.position) < 1f)
        {
            for (int i = 0; i < waypointUsers.Count; i++)
            {
                if (waypointUsers[i].ShouldChangeWaypoint())
                {
                    GetNextWayPoint(waypointUsers.IndexOf(waypointUsers[i]));
                }
            }
        }
    }

    public int GetNextWayPoint(int userIdx)
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
        return waypointPosIndex;
    }

}


