using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(StartGame);

    }
    void StartGame()
    {
        SceneManager.LoadScene("LevelTest_2");
    }
}
