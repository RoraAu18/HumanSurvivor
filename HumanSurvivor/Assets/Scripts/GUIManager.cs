using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour, IGameEventsUser
{
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI deathsTxt;
    public TextMeshProUGUI coinsTxt;

    public void Awake()
    {
        //We must subscribe this class to the gameeventusers list
        GameManager.OnlyInstance.gameEventUsers.Add(this);
    }
    void Start()
    {
        scoreTxt.SetText("0");
        deathsTxt.SetText("0");
        coinsTxt.SetText("0");
    }
    public void OnMoodChanged(int newScore)
    {
        deathsTxt.SetText(newScore.ToString());
    }

    public void TimerRecord(int newScore)
    {
        scoreTxt.SetText(newScore.ToString());
    }

    public void OnObjectsCollected(int newScore)
    {
        coinsTxt.SetText(newScore.ToString());
    }

}
