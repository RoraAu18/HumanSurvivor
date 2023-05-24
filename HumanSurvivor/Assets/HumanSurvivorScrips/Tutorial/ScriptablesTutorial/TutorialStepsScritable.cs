using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StepTutorial", menuName = "HumanSurvivor/StepTutorial")]

public class TutorialStepsScritable : ScriptableObject
{
    public int idxStep;
    public string nameStep;
    public string descriptionStepForUI;
    public bool isComplete;
  

}
