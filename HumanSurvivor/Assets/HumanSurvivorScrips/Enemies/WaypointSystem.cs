using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    public Transform[] waypoints = new Transform[4];
    Transform currentWaypoint;
    [SerializeField]
    float moveSpeed;
    int waypointPosIndex;
    [SerializeField]
    float targetTime;
    float timer;
    [SerializeField]
    public List<IWaypointUser> waypointUsers = new List<IWaypointUser>();

    void Start()
    {
        waypointPosIndex = 0;
        currentWaypoint = waypoints[waypointPosIndex];
        transform.position = currentWaypoint.position;

        var myUsers = GetComponentsInChildren<IWaypointUser>();
        //uneccesarry? could just add them instead of checking
        for (int i = 0; i < myUsers.Length; i++)
        {
            if (!waypointUsers.Contains(myUsers[i]))
            {
                waypointUsers.Add(myUsers[i]);
            }
        }
    }



    public void RemoveUser(IWaypointUser user)
    {
        waypointUsers.Remove(user);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= targetTime)
        {
            Move();
            timer = 0;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        var distanceLeft = currentWaypoint.position - transform.position;
        Debug.Log("moving towards" + currentWaypoint);
        if (distanceLeft.magnitude < 0.001f)
        {
            //i don´t get this line of code :v
            if (waypointUsers.Count == 0)
            {
                //GetNextWayPoint();
            }
        }
        else
        {
            for (int i = 0; i < waypointUsers.Count; i++)
            {
                if (waypointUsers[i].ShouldChangeWaypoint())
                {
                    //GetNextWayPoint(waypointUsers[i].TimeToMove(), waypointUsers.IndexOf(waypointUsers[i]));
                    break;
                }
            }
        }
    }


}
