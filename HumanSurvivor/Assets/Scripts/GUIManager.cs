using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour, IGameEventsUser, IWinLoseStateUser
{
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI deathsTxt;
    public TextMeshProUGUI coinsTxt;
    public List<Image> objectsToCollect;
    public List<Sprite> iconsObjectsCollected;
    public Image iconPlayer;
    public Sprite happyPlayerSprite;
    public Sprite fearPlayerSprite;
    public Sprite stealthPlayerSprite;
    public Sprite distractPlayerSprite;

    public GameObject winLoseMenuParent;
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


    public void OnObjectsCollected(int indexObjectCollected)
    {      
          objectsToCollect[indexObjectCollected].sprite = iconsObjectsCollected[indexObjectCollected];
    }

    public void WinLoseEvent(bool youWin)
    {
        if (GameManager.OnlyInstance.allItemsCollected)
        {
            firstStarPos.sprite = startIcon;
        }
        if (GameManager.OnlyInstance.onTime)
        {
            secondStarPos.sprite = startIcon;
        }
        if (GameManager.OnlyInstance.wasntDetected)
        {
            thirdStarPos.sprite = startIcon;
        }
        if (youWin)
        {
            winLoseText.text = "VICTORY";
        }
        else
        {
            winLoseText.text = "DEFEAT";

        }
        winLoseMenuParent.SetActive(true);

    }

}
