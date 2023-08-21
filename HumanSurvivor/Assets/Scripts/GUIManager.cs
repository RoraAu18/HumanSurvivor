using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GUIManager : MonoBehaviour, IGameEventsUser, IWinLoseStateUser, IGameDataStorer
{
    public GameObject gamePlayUI;
    public ObjectsToCollectDataList objectsToCollectDataList;
    GameData gameUIdata;

    [SerializeField] Button reset;
    [SerializeField] Button backMenu;
    public List<ObjectImagePerType> objectsToCollect;
   
    [HideInInspector]
    public Image iconPlayer;
    [HideInInspector]
    public Sprite happyPlayerSprite;
    [HideInInspector]
    public Sprite fearPlayerSprite;
    [HideInInspector]
    public Sprite stealthPlayerSprite;
    [HideInInspector]
    public Sprite distractPlayerSprite;

    public GameObject winLoseMenuParent;
    [HideInInspector]
    public Image winLoseImage;
    [HideInInspector]
    public Sprite winSprite;
    [HideInInspector]
    public Sprite loseSprite;
    public TextMeshProUGUI winLoseText;
    [HideInInspector]
    public Image firstStarPos;
    [HideInInspector]
    public Image secondStarPos;
    [HideInInspector]
    public Image thirdStarPos;
    [HideInInspector]
    public Sprite startIcon;
    [SerializeField]
    BestMatchsGameData bestMatchData;

    public GameObject pauseMenuContainer;
    public Button resumeGameButton;
    public Button backToMenuButton;
    public Button pauseButton;

    [SerializeField]
    bool isATutorial = false;
    [SerializeField]
    GameObject tutorialContainer;
    public DataManager dataMan => GameManager.OnlyInstance.dataManager;

    public GameData id => bestMatchData;

    public void Awake()
    {
       GameManager.OnlyInstance.gameEventUsers.Add(this);
       GameManager.OnlyInstance.winLoseStateUser.Add(this);


    }
    void Start()
    {
        iconPlayer.sprite = GameManager.OnlyInstance.player.characterFeatures.iconPlayer.sprite;
        happyPlayerSprite = GameManager.OnlyInstance.player.characterFeatures.happyPlayerSprite;
        fearPlayerSprite = GameManager.OnlyInstance.player.characterFeatures.fearPlayerSprite;
        stealthPlayerSprite = GameManager.OnlyInstance.player.characterFeatures.stealthPlayerSprite;
        distractPlayerSprite = GameManager.OnlyInstance.player.characterFeatures.distractPlayerSprite;

        winLoseMenuParent.SetActive(false);
        reset.onClick.AddListener(ResetGame);
        backMenu.onClick.AddListener(BackGame2);
        pauseMenuContainer.SetActive(false);
        backToMenuButton.onClick.AddListener(BackGame2);
        pauseButton.onClick.AddListener(PauseGame);
        resumeGameButton.onClick.AddListener(ResumeGame);

        dataMan.AddUser(this, id.id);
        if (isATutorial) { tutorialContainer.gameObject.SetActive(true); }
        else { tutorialContainer.gameObject.SetActive(false); }
        //restart teh starts of the level
        Time.timeScale = 1;
    }

    private void ResetGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
      
    }
       
    private void BackGame2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu 1");
    }

    void PauseGame()
    {
        pauseMenuContainer.SetActive(true);
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnMoodChanged(PlayerStates newState)
    {
        if (GameManager.OnlyInstance.player.amAfraid)
        {
            iconPlayer.sprite = fearPlayerSprite;
        }
        else
        {
            switch (newState)
            {
                case PlayerStates.distract:
                    iconPlayer.sprite = distractPlayerSprite;
                    break;
                case PlayerStates.stealthMove:
                    iconPlayer.sprite = stealthPlayerSprite;
                    break;
                case PlayerStates.stealthIdle:
                    iconPlayer.sprite = stealthPlayerSprite;
                    break;
                default:
                    iconPlayer.sprite = happyPlayerSprite;
                    break;
            }
        }
    }

    public void OnObjectsCollected(ObjectsType typeObjectCollected)
    {
        for (int i = 0; i < objectsToCollect.Count; i++)
        {
            if (objectsToCollect[i].myType == typeObjectCollected)
            {
                objectsToCollect[i].myImage.sprite = objectsToCollectDataList.GetObjectDataByType(typeObjectCollected).myImage;
                return;
            }
        }

       
    }

    IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(1f);
        winLoseMenuParent.SetActive(true);
    }


    //Best scores system should be added to the Game manager instead. 
    public void WinLoseEvent(bool youWin)
    {
        gamePlayUI.SetActive(false);
        var currentData = new BestMatchsGameData();
        if (youWin)
        {
            winLoseImage.sprite=winSprite;
            if (GameManager.OnlyInstance.allItemsCollected)
            {
                firstStarPos.color = Color.white;
                currentData.allItemsCollected = true;
            }
            if (GameManager.OnlyInstance.onTime)
            {
                secondStarPos.color = Color.white;
                currentData.onTime = true;

            }
            if (GameManager.OnlyInstance.wasntDetected)
            {
                thirdStarPos.color = Color.white;
                currentData.wasNotDetected = true;
            }
        }        
        else
        {
            winLoseImage.sprite = loseSprite;
        }

        if (GameManager.OnlyInstance.GetBestScore(bestMatchData, currentData, out BestMatchsGameData newBest))
        {
            bestMatchData = newBest;
            dataMan.SaveData(bestMatchData);
        }
        StartCoroutine(OnGameOver());
    }

    public GameData StoreData()
    {
        return bestMatchData;
    }

    public void RestoreData(GameData restoreData)
    {
        if(restoreData is BestMatchsGameData bestData)
        {
            bestMatchData = bestData;
        }
    }


    [Serializable]
    public class ObjectImagePerType
    {
        public Image myImage;
        public ObjectsType myType;
    }

}


