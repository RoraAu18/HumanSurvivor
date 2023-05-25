using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class TutorialManager : MonoBehaviour, IWinLoseStateUser
{
    public List<TutorialStepsScritable> tutorialStepsInOrden;
    public List<Collider> triggerForSteps;
    public TextMeshProUGUI stepText;
    public int currentStepIndex;
    public GameObject tutoriaUIParent;

    private void Awake()
    {
        GameManager.OnlyInstance.winLoseStateUser.Add(this);

    }
    void Start()
    {
        currentStepIndex = 0;

        for (int i = 1; i < triggerForSteps.Count; i++)
        {
            triggerForSteps[i].gameObject.SetActive(false);
        }

    }

    private void Update()
    {

    }

    public void ActivateTutorialUI()
    {
        if (currentStepIndex < tutorialStepsInOrden.Count)
        {
            stepText.text = tutorialStepsInOrden[currentStepIndex].descriptionStepForUI;
            tutorialStepsInOrden[currentStepIndex - 1].isComplete = true;
            triggerForSteps[currentStepIndex].gameObject.SetActive(true);
        }

    }

    public void WinLoseEvent(bool youWin)
    {
        tutoriaUIParent.SetActive(false);
    }
}
