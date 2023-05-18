using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IGameEventsUser
{
    //This will help control and check on the general changes and modify them from here
    //There are several programming <frameworks/Stablished methods> in which we can notify the Game Manager,the observation, getting the Minager to check on the processes, the benefit of that one is that we only have to modify this code if necessary. we cna also get the functions and methods to noticy the manager. The most optimal way was to create an interface as below.
    public void TimerRecord(int newScore);
    public void OnMoodChanged(PlayerStates statePlayer);
    public void OnObjectsCollected(int indexObjectCollected);

}
public class GameManager : MonoBehaviour
{
    public EnemyAIContoller enemy;
    public AIPlayerController player;
       
    private static GameManager instance;
    public int currentScore;
    public int currentDeaths;
    public int currentCoin;
    public Transform currentDistraction;
    public GameStates gameStates;
    private PlayerStates currPlayerState;
    private EnemyStates currEnemyState;
    private iconPlayerStates iconPlayerStates;

    public List<IGameEventsUser> gameEventUsers = new List<IGameEventsUser>();
   
    public static GameManager OnlyInstance
    {        
        get
        {
            return instance; 
        }
    }

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
            gameEventUsers[i].TimerRecord(currentScore);
        }
    }

    public void OnDistractMode(Transform posForDistract)
    {
        enemy.gotDistraction = true;
        currentDistraction = posForDistract;
    }

    public void PlayerChangeMood(PlayerStates statePlayer)
    {
        currPlayerState = statePlayer;

        for (int i = 0; i < gameEventUsers.Count; i++)
        {
            gameEventUsers[i].OnMoodChanged(statePlayer);
        }
    }
    public void EnemyChangeMood(EnemyStates stateEnemy)
    {
        currEnemyState = stateEnemy;
        player.amAfraid = (stateEnemy == EnemyStates.CatchingPlayer);

        for (int i = 0; i < gameEventUsers.Count; i++)
        {
           // gameEventUsers[i].OnMoodChanged(statePlayer);
        }
    }

    public void GlobalState()
    {
        if (currEnemyState==EnemyStates.CatchingPlayer)
        {

        }
    }


    public void AddItemCollected(int indexObjectCollected)
    {        
        for (int i = 0; i < gameEventUsers.Count; i++)
        {
            gameEventUsers[i].OnObjectsCollected(indexObjectCollected);
        }
    }
}
public enum iconPlayerStates
{
    Happy,
    Sad,
    Stealth,
    Distract,
    Death
}
public enum GameStates
{
    GameStart,
    GameOver,
    GameRunning
}


