using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameEventsUser
{   
    public void OnMoodChanged(PlayerStates statePlayer);
    public void OnObjectsCollected(ObjectsType typeObjectCollected); 
}

public interface IWinLoseStateUser
{
    public void WinLoseEvent(bool youWin);
}

public class GameManager : MonoBehaviour
{
    public List<AIPlayerController> players;
    public Transform placeToBornPlayer;

    public EnemyAIContoller enemy;
    public AIPlayerController player;
    public SoundManager soundManager;
       
    private static GameManager instance;

    public int itemsToCollect;
    public int currItemsCollected;
    public float maxTime;
    private float time;
    public bool wasntDetected=true;
    public bool onTime = true;
    public bool allItemsCollected = false;
    

    public Transform currentDistraction;
    public GameStates gameStates;
    private PlayerStates currPlayerState;
    private EnemyStates currEnemyState;
    public iconPlayerStates iconPlayerStates;

    public List<IGameEventsUser> gameEventUsers = new List<IGameEventsUser>();
    public List<IWinLoseStateUser> winLoseStateUser = new List<IWinLoseStateUser>();
   
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

    private void Start()
    {
        int idxPlayer = PlayerPrefs.GetInt("idxPlayer",0);
        player = Instantiate(players[idxPlayer], placeToBornPlayer);
        player.transform.rotation = placeToBornPlayer.transform.rotation;
        player.TryGetComponent<AudioSource>(out AudioSource playerAudio);
        soundManager.player = playerAudio;
        
    }

    private void Update()
    {
        time += Time.deltaTime;
        
    }

    public void AddScore(int amount)
    {
        //currentScore += amount;
        for (int i = 0; i < gameEventUsers.Count; i++)
        {
           // gameEventUsers[i].TimerRecord(currentScore);
        }
    }

    public void OnDistractMode()
    {
        enemy.gotDistraction = true;
        
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
        player.amAfraid = (enemy.chasingPlayer);
        wasntDetected = !(stateEnemy == EnemyStates.CatchingPlayer);
        if (stateEnemy== EnemyStates.CatchingPlayer)
        {
            OnWinLoseState(false);
        }

        for (int i = 0; i < gameEventUsers.Count; i++)
        {
           // gameEventUsers[i].OnMoodChanged(statePlayer);
        }
    }     


    public void AddItemCollected(ObjectsType objectType)
    {
        currItemsCollected += 1;
        for (int i = 0; i < gameEventUsers.Count; i++)
        {
            gameEventUsers[i].OnObjectsCollected(objectType);
        }
        if (currItemsCollected==itemsToCollect)
        {
            allItemsCollected = true;
            OnWinLoseState(true);
        }
        soundManager.playSoundAddCollectable();

    }

    public void OnWinLoseState(bool youWin)
    {      
        onTime = CheckTime();
        for (int i = 0; i < winLoseStateUser.Count; i++)
        {
            winLoseStateUser[i].WinLoseEvent(youWin);
        }
        gameStates = GameStates.GameOver;
    }

    public bool CheckTime()
    {
        Debug.Log(time + " " + maxTime);
        if (time < maxTime)
        {
            return true;
        }
        else
        {
            return false;
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


