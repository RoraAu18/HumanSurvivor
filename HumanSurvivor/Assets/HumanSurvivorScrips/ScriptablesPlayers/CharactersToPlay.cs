using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "NewCharacter", menuName = "HumanSurvivor/Character")]

public class CharactersToPlay : ScriptableObject
{
    public GameObject characterSelected;
    public string nameCharacter;
    public string descriptionCharacter;

    //Expressions
    public Image iconPlayer;

    public Sprite happyPlayerSprite;
    public Sprite fearPlayerSprite;
    public Sprite stealthPlayerSprite;
    public Sprite distractPlayerSprite;
    
}
