using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCamera : MonoBehaviour
{
    public List<Transform> players;
    public float damping = 1;
    public float speed = 1;
    public float zSpeed = 1;
    public float minDistance = 5;
    public float maxDistance = 15;
    public float sumOnZCam;
    public Camera cam;
    private Vector3 _midPlayerDisplayPos;
    private float _MovForwardCam;

    private void Start()
    {
        players.Add(GameManager.OnlyInstance.player.transform);
    }
    void Update()
    {
        _midPlayerDisplayPos = GetMidPointPlayersOnDisplay();
        var dir = new Vector3(_midPlayerDisplayPos.x, 0, _midPlayerDisplayPos.y) - new Vector3(0.5f, 0, 0.5f);
        transform.position += dir * Time.deltaTime * speed;

        _MovForwardCam = GetForwardMovementDirection();

        var desireNewValue = sumOnZCam + _MovForwardCam * Time.deltaTime * zSpeed;

        if (desireNewValue < minDistance || desireNewValue > maxDistance)
        {
            _MovForwardCam = 0;
        }

        sumOnZCam += _MovForwardCam * Time.deltaTime * zSpeed;
        sumOnZCam = Mathf.Clamp(sumOnZCam, minDistance, maxDistance);

        transform.position += transform.forward * _MovForwardCam * Time.deltaTime * zSpeed;
    }

    public Vector3 GetMidPointPlayersOnDisplay()
    {
        Vector3 midpoint = Vector3.zero;
        foreach (Transform player in players)
        {
            Vector3 screenPos = cam.WorldToViewportPoint(player.position);
            midpoint += screenPos;
        }
        midpoint /= players.Count;

        return midpoint;
    }

    public float GetForwardMovementDirection()
    {
        float maxDistance = 0;
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j < players.Count; j++)
            {
                if (i == j) continue;
                var player1 = cam.WorldToViewportPoint(players[i].position);
                var player2 = cam.WorldToViewportPoint(players[j].position);
                var dis = (player1 - player2);
                dis.z = 0;
                maxDistance = Mathf.Max(dis.magnitude, maxDistance);
            }
        }
        var delta = 0.75f - maxDistance;
        return delta;
    }
}