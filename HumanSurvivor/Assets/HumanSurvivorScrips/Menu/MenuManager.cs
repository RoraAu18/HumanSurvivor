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
        playButton.onClick.AddListener(SelectGame);

    }
    void SelectGame()
    {
        selectMenu.SetActive(true);
        inicialMenu.SetActive(false);
         
    }
}
