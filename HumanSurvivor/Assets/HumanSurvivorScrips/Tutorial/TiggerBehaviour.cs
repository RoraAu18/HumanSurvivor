using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerBehaviour : MonoBehaviour
{
    private Collider myCollider;
    public TutorialManager tutorialManager;
    private ColisionController colisionController;
    void Start()
    {
        TryGetComponent<Collider>(out myCollider);
        TryGetComponent<ColisionController>(out colisionController);
        colisionController.collisionEnter += NextStep;
    }

    private void NextStep(Collider _)
    {
        if(tutorialManager.currentStepIndex == 6) { GameManager.OnlyInstance.crossedFinalLine = true; };
        tutorialManager.currentStepIndex += 1;
        if(tutorialManager.currentStepIndex == 5)
        {
            tutorialManager.triggerForSteps[6].enabled = true;
        }
        tutorialManager.ActivateTutorialUI();
        gameObject.SetActive(false);

    }

}
