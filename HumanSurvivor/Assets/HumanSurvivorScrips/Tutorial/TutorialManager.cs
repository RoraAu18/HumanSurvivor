using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class TutorialManager : MonoBehaviour
{
    public List<TutorialStepsScritable> tutorialStepsInOrden;
    public List<Collider> triggerForSteps;
    public TextMeshProUGUI stepText;
    public int currentStepIndex;
    public GameObject tutoriaUIParent;
    

    void Start()
    {
        currentStepIndex = -1;    
        
        for (int i = 1; i < triggerForSteps.Count; i++)
        {
            triggerForSteps[i].gameObject.SetActive(false);
        }
       
    }


    void Update()
    {
        //if (tutorialStepsInOrden[currentStepIndex].isComplete)
        //{           
        //    tutorialStepsInOrden[currentStepIndex].isComplete = true;

        //    ProgressToNextStep();
        //}

        //if (tutorialStepsInOrden[tutorialStepsInOrden.Count - 1].isComplete)
        //{
        //    tutoriaUIParent.SetActive(false);
        //}

    }

    private void ProgressToNextStep()
    {
        
        if (currentStepIndex < tutorialStepsInOrden.Count - 1)
        {            
            currentStepIndex++;
            ActivateTutorialUI();
        }
        else
        {            
            //EndTutorial();
        }
    }

    public void ActivateTutorialUI()
    {
        if (currentStepIndex > tutorialStepsInOrden.Count)
        {
            tutoriaUIParent.SetActive(false);
        }
        stepText.text = tutorialStepsInOrden[currentStepIndex].descriptionStepForUI;
        tutorialStepsInOrden[currentStepIndex - 1].isComplete = true;
        triggerForSteps[currentStepIndex+1].gameObject.SetActive(true);
    }

}
