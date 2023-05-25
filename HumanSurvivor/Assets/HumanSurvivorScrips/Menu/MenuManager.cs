using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject selectMenu;
    public GameObject inicialMenu;
    public List<CharactersToPlay> charactarers;
    [SerializeField]
    Button creditsButton;    
    [SerializeField]
    Button backToMenu;
    [SerializeField]
    GameObject creditsContainer;

     
    [SerializeField] Button playButton;

    private void Awake()
    {
        if (instance != this)
        {
            if (instance != null)
            {
                DestroyImmediate(instance.gameObject);
            }
        }
        instance = this;
    }

    void Start()
    {
        creditsContainer.SetActive(false);
        playButton.onClick.AddListener(SelectGame);
        creditsButton.onClick.AddListener(Credits);
        backToMenu.onClick.AddListener(Back);
    }
    void SelectGame()
    {
        selectMenu.SetActive(true);
        inicialMenu.SetActive(false);
         
    }
    void Credits()
    {
        creditsContainer.SetActive(true);
    }
    void Back()
    {
        creditsContainer.SetActive(false);

    }
}
