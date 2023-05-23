using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewLevel", menuName = "HumanSurvivor/Level")]

public class LevelsToPlayData : ScriptableObject
{
    public string sceneNameToLoad;
    public Sprite spriteLevel;
    public string nameLevel;
    public int objectsToCollect;
    public int maxTimeToCollect;
    public bool isBlocked;
}
