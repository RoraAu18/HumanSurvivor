using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataRef", menuName = "HumanSurvivor/DataSystem/DataRefs")]

public class DataRef : ScriptableObject
{
    public string uniqueId;
    public bool inUse = false;
}
