using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour, IWinLoseStateUser
{
    [SerializeField] LevelsToPlayData thisLevel;
    public void Awake()
    {
        GameManager.OnlyInstance.winLoseStateUser.Add(this);

    }
    public void WinLoseEvent(bool youWin)
    {
        var nextIdxLevel = thisLevel.idxLevel + 1;
        if (youWin)
        {
            PlayerPrefs.SetInt("Level_" + nextIdxLevel.ToString(), 1);
        }
       
    }
}
