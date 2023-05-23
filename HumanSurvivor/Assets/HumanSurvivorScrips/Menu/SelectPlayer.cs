using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SelectPlayer : MonoBehaviour
{
    private int index;
    public Transform charactarersPrefabs;
    [SerializeField] Button nextButton;
    [SerializeField] Button previousButton;
    [SerializeField] GameObject character;
    [SerializeField] TextMeshProUGUI nameCharacter;
    [SerializeField] TextMeshProUGUI descriptionCharacter;

   
    private GameObject oldCharacter;

    
    private Dictionary<GameObject, GameObject> characterInstances = new Dictionary<GameObject, GameObject>();


    private void Start()
    {
        nextButton.onClick.AddListener(NextCharacter);
        previousButton.onClick.AddListener(PreviosCharacter);
        oldCharacter = null;
        index = PlayerPrefs.GetInt("idxPlayer");
        ChangeCharacter();
    }

    private void ChangeCharacter()
    {
        DeactivateAllCharacters();
        PlayerPrefs.SetInt("idxPlayer", index);
        character = MenuManager.instance.charactarers[index].characterSelected;
        GameObject instantiatedCharacter;

        if (characterInstances.ContainsKey(character))
        {
            instantiatedCharacter = characterInstances[character];
        }
        else
        {
            instantiatedCharacter = Instantiate(character, charactarersPrefabs);
            characterInstances.Add(character, instantiatedCharacter);
        }

        instantiatedCharacter.transform.eulerAngles = (Vector3.up * 140);
        instantiatedCharacter.SetActive(true);
        SetLayerRecursively(instantiatedCharacter, LayerMask.NameToLayer("UI"));
        instantiatedCharacter.transform.localScale = (Vector3.one * 300);
        nameCharacter.text = MenuManager.instance.charactarers[index].nameCharacter;
        descriptionCharacter.text = MenuManager.instance.charactarers[index].descriptionCharacter;
        
    }

    private void DeactivateAllCharacters()
    {
        foreach (GameObject characterInstance in characterInstances.Values)
        {
            characterInstance.SetActive(false);
        }
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    private void NextCharacter()
    {
        if (index==MenuManager.instance.charactarers.Count-1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }
        ChangeCharacter();
    }

    private void PreviosCharacter()
    {
        if (index == 0)
        {
            index = MenuManager.instance.charactarers.Count - 1;
        }
        else
        {
            index -= 1;
        }
        ChangeCharacter();
    }

}
