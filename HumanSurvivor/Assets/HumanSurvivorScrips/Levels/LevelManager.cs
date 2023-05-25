using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<LevelsToPlayData> levelsToPlayData;
    private List<bool> levelEnabledStates = new List<bool>();
    bool isEnabled = false;

    private void Awake()
    {       
        PlayerPrefs.SetInt("Level_0", 1);
        
        

        for (int i = 0; i < levelsToPlayData.Count; i++)
        {            
            var value=PlayerPrefs.GetInt("Level_" + i.ToString());

            if (value==1)
            {
                levelsToPlayData[i].isBlocked = false;
            }
            else
            {
                levelsToPlayData[i].isBlocked = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
