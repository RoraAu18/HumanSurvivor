using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ObjectsToCollect", menuName = "HumanSurvivor/ObjectToCollect")]
public class ObjectsToCollectDataList : ScriptableObject
{
    public List<ObjectToCollectData> objectToCollectData;

    public ObjectToCollectData GetObjectDataByType(ObjectsType objectType)
    {
        for (int i = 0; i < objectToCollectData.Count; i++)
        {
            if (objectToCollectData[i].objectsType== objectType)
            {
                return objectToCollectData[i];
            }
        }
        return null;
    }

}

[Serializable]
public class ObjectToCollectData
{
    public ObjectsType objectsType;
    public Sprite myImage;

}

public enum ObjectsType
{
    Coffee=0,
    Laptop=1,
    ToiletPaper=2,
}