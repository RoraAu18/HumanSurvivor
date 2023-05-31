using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerBehaviour : MonoBehaviour
{
    private Collider myCollider;
    public TutorialManager tutorialManager;
    private ColisionController colisionController;
    [SerializeField]
    TutorialStepsScritable step;
    void Start()
    {
        TryGetComponent<Collider>(out myCollider);
        TryGetComponent<ColisionController>(out colisionController);
        colisionController.collisionEnter += NextStep;
    }

    private void NextStep(Collider _)
    {
        if (tutorialManager.currentStepIndex == 7) { GameManager.OnlyInstance.crossedFinalLine = true; };
        tutorialManager.currentStepIndex += 1;
        if(tutorialManager.currentStepIndex == 6) { tutorialManager.finalItemQuest.gameObject.SetActive(true); }
        tutorialManager.ActivateTutorialUI();
        gameObject.SetActive(false);
    }

    bool Tasks(int step)
    {
        switch (step)
        {
            case 1:
                {
                    return true;
                }
            case 2:

                    return true;
                
            case 3:
                {
                    return true;
                }
            case 4:

                {
                    return true;
                }
            case 5:

                {
                    return true;
                }
            case 6:

                {
                    return true;
                }
            case 7:

                {
                    return true;
                }
        }
        return false;

    }
}
