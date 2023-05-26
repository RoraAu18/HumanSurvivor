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
   
    public Image iconPlayer;
    public Sprite happyPlayerSprite;
    public Sprite fearPlayerSprite;
    public Sprite stealthPlayerSprite;
    public Sprite distractPlayerSprite;

    public GameObject winLoseMenuParent;
    public Image winLoseImage;
    public Sprite winSprite;
    public Sprite loseSprite;
    public TextMeshProUGUI winLoseText;
    public Image firstStarPos;
    public Image secondStarPos;
    public Image thirdStarPos;
    public Sprite startIcon;
    public GameObject pauseMenuContainer;
    public Button resumeGameButton;
    public Button backToMenuButton;
    public Button pauseButton;
    public void Awake()
    {
       GameManager.OnlyInstance.gameEventUsers.Add(this);
       GameManager.OnlyInstance.winLoseStateUser.Add(this);
    }
    void Start()
    {
        winLoseMenuParent.SetActive(false);
        reset.onClick.AddListener(ResetGame);
        backMenu.onClick.AddListener(BackGame2);
        pauseMenuContainer.SetActive(false);
        backToMenuButton.onClick.AddListener(BackGame2);
        pauseButton.onClick.AddListener(PauseGame);
        resumeGameButton.onClick.AddListener(ResumeGame);
        Time.timeScale = 1;
    }

    private void ResetGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
      
    }

    private void BackGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
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
        winLoseMenuParent.SetActive(true);

    }

    [Serializable]
    public class ObjectImagePerType
    {
        public Image myImage;
        public ObjectsType myType;
    }

}


