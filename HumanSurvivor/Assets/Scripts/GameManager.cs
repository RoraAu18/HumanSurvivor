using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IGameEventsUser
{   
    public void OnMoodChanged(PlayerStates states);
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
    public bool crossedFinalLine = false;
    [SerializeField]
    GameObject finalCheckpoint;

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
        finalCheckpoint.gameObject.SetActive(false);
        crossedFinalLine = false;
        //winLoseStateUser = GetComponents<IWinLoseStateUser>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        player.amAfraid = (enemy.chasingPlayer);
        if (allItemsCollected)
        {
            if (crossedFinalLine)
            {
                OnWinLoseState(true);
                crossedFinalLine = false;
                return;
            }
        }
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
        soundManager.DistractingEnemySound();
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
        //wasntDetected = !(stateEnemy == EnemyStates.Surprised);
        if (stateEnemy == EnemyStates.Surprised)
        {
            wasntDetected = false;
        }
        if (stateEnemy== EnemyStates.CatchingPlayer)
        {
            OnWinLoseState(false);
        }

        for (int i = 0; i < gameEventUsers.Count; i++)
        {
           //gameEventUsers[i].OnMoodChanged(stateEnemy);
        }
    }     


    public void AddItemCollected(ObjectsType objectType)
    {
        currItemsCollected += 1;
        for (int i = 0; i < gameEventUsers.Count; i++)
        {
            gameEventUsers[i].OnObjectsCollected(objectType);
            StartCoroutine(CollectEffect());
        }
        if (currItemsCollected==itemsToCollect)
        {
            allItemsCollected = true;
            ActivateFinalCheckpoint();
        }
        soundManager.playSoundAddCollectable();

    }

    void ActivateFinalCheckpoint()
    {
        if (allItemsCollected)
        {
            finalCheckpoint.gameObject.SetActive(true);
        }
    }
    public void OnWinLoseState(bool youWin)
    {      
        onTime = CheckTime();
        for (int i = 0; i < winLoseStateUser.Count; i++)
        {
            winLoseStateUser[i].WinLoseEvent(youWin);
            Debug.Log(winLoseStateUser.Count);
        }
        gameStates = GameStates.GameOver;
    }
    public bool CheckTime()
    {
        return time < maxTime;
    }

    IEnumerator CollectEffect()
    {
        player.collectionEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        player.collectionEffect.gameObject.SetActive(false);

    }
}

public enum GameStates
{
    GameStart,
    GameOver,
    GameRunning
}


