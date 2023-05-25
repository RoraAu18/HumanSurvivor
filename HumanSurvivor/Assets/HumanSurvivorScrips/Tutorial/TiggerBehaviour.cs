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
        tutorialManager.currentStepIndex += 1;
        tutorialManager.ActivateTutorialUI();
        gameObject.SetActive(false);

    }

}
