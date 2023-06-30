using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Utilities
{
    static GameObject[] rootGameObject;

    public static void GetRooTObjects()
    {
        rootGameObject = SceneManager.GetActiveScene().GetRootGameObjects();

    }
    public static void GetComponentsOfType<T>(List<T> myTList) where T : Component
    {
        myTList.Clear();
        for (int i = 0; i < rootGameObject.Length; i++)
        {
            RecursiveGetComponetsOfType(rootGameObject[i], myTList);            
        }
    }

    private static void RecursiveGetComponetsOfType<T>(GameObject myObject, List<T> myTList) where T : Component
    {
        if (myObject.TryGetComponent(out T myT)) myTList.Add(myT);

        for (int i = 0; i < myObject.transform.childCount; i++)
        {
            var myChild = myObject.transform.GetChild(i).gameObject;
            RecursiveGetComponetsOfType(myChild, myTList);
        }

    }

}
