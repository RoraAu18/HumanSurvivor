using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelImageBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] LevelsToPlayData levelsToPlayData;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image backgroundForText;   
    [SerializeField] Image blockImage;
    [SerializeField] Button playLevelButton;
    private Image myBackgroudImage;
    private bool isMouseOver = false;

   
    void Start()
    {
        TryGetComponent<Image>(out myBackgroudImage);
        nameText.text = levelsToPlayData.nameLevel;
        myBackgroudImage.sprite = levelsToPlayData.spriteLevel;
        nameText.gameObject.SetActive(false);
        playLevelButton.gameObject.SetActive(false);

       


        if (levelsToPlayData.isBlocked == true)
        {
            blockImage.gameObject.SetActive(true);
            backgroundForText.gameObject.SetActive(true);
        }
        else
        {
            blockImage.gameObject.SetActive(false);
            backgroundForText.gameObject.SetActive(false);
        }

        playLevelButton.onClick.AddListener(StartLevel);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActiveInfoLevel(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActiveInfoLevel(false);
    }

    private void ActiveInfoLevel(bool state)
    {
        if (levelsToPlayData.isBlocked==false)
        {
            backgroundForText.gameObject.SetActive(state);
            nameText.gameObject.SetActive(state);
            playLevelButton.gameObject.SetActive(state);
        }
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(levelsToPlayData.sceneNameToLoad);
    }
}

