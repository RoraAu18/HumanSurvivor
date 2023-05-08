using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IGameEventsUser
{
    //This will help control and check on the general changes and modify them from here
    //There are several programming <frameworks/Stablished methods> in which we can notify the Game Manager,the observation, getting the Minager to check on the processes, the benefit of that one is that we only have to modify this code if necessary. we cna also get the functions and methods to noticy the manager. The most optimal way was to create an interface as below.
    public void OnScoreChanged(int newScore);
    public void OnDeathsCountChanged(int newScore);
    public void OnCoinsCollected(int newScore);

}
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int currentScore;
    public int currentDeaths;
    public int currentCoin;

    public List<IGameEventsUser> gameEventUsers = new List<IGameEventsUser>();
   
    public static GameManager OnlyInstance
    {
        //like get set, but we just want to get, we do not want the instance to be able to set anything anywhere else
        get
        {
            return instance; 
        }
    }

    //Awakes is set even before the first frame is played, we normally set references and instances here
    private void Awake()
    {
        if(instance != this)
        {
            if(instance != null)
            {
                DestroyImmediate(instance.gameObject);
            }
        }
        instance = this;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        for (int i = 0; i < gameEventUsers.Count; i++)
        {
            gameEventUsers[i].OnScoreChanged(currentScore);
        }
    }

    public void AddDeaths(int amount)
    {
        currentDeaths += amount;
        for (int i = 0; i < gameEventUsers.Count; i++)
        {
            gameEventUsers[i].OnDeathsCountChanged(currentDeaths);
        }
    }

    public void AddCoins(int amount)
    {
        currentCoin += amount;
        for (int i = 0; i < gameEventUsers.Count; i++)
        {
            gameEventUsers[i].OnCoinsCollected(currentCoin);
        }
    }
}


