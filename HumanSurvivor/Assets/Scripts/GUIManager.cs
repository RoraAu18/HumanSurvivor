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
    
    public void Awake()
    {
       GameManager.OnlyInstance.gameEventUsers.Add(this);
       GameManager.OnlyInstance.winLoseStateUser.Add(this);
    }
    void Start()
    {
        winLoseMenuParent.SetActive(false);
        reset.onClick.AddListener(ResetGame);
        backMenu.onClick.AddListener(BackGame);
    }

    private void ResetGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
      
    }

    private void BackGame()
    {
        SceneManager.LoadScene("MainMenu");
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
                case PlayerStates.stealth:
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


