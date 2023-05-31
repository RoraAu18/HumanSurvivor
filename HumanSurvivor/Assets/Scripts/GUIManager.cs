using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GUIManager : MonoBehaviour, IGameEventsUser, IWinLoseStateUser
{
    public GameObject gamePlayUI;
    public ObjectsToCollectDataList objectsToCollectDataList;

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

    public GameObject pauseMenuContainer;
    public Button resumeGameButton;
    public Button backToMenuButton;
    public Button pauseButton;

    [SerializeField]
    bool isATutorial = false;
    [SerializeField]
    GameObject tutorialContainer;

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

        if (isATutorial) { tutorialContainer.gameObject.SetActive(true); }
        else { tutorialContainer.gameObject.SetActive(false); }

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
    public void WinLoseEvent(bool youWin)
    {
        gamePlayUI.SetActive(false);
               
        if (youWin)
        {
            winLoseImage.sprite=winSprite;
            if (GameManager.OnlyInstance.allItemsCollected)
            {
                firstStarPos.color = Color.white;
            }
            if (GameManager.OnlyInstance.onTime)
            {
                secondStarPos.color = Color.white;
            }
            if (GameManager.OnlyInstance.wasntDetected)
            {
                thirdStarPos.color = Color.white;
            }
        }        
        else
        {
            winLoseImage.sprite = loseSprite;
        }
        StartCoroutine(OnGameOver());
    }



    [Serializable]
    public class ObjectImagePerType
    {
        public Image myImage;
        public ObjectsType myType;
    }

}


