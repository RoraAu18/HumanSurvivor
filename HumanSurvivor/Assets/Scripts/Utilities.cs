using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Utilities
{
    static GameObject[] rootGameObject;

    //Declarate this is an extension method with "this" 
    public static float Operate(this Operations operations, float n1, float n2)
    {
        switch (operations)
        {
            case Operations.Sum:
                return n1 + n2;
            case Operations.Multiply:
                return n1 * n2;
            case Operations.Set:
                return n1 = n2;
            default:
                return n1;
        }
    }
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
public enum Operations
{
    Sum,
    Multiply,
    Set
}
/*
 * M�todos de extesi�n, sin estar dentro de la clase, a�adir funcionalidad a la clase.
 * desde una clase externa ponerle m�s m�todos sin tener que escribirlos. :0
 * 
 * M�todo para a�adir y otro para eliminar de la lista de modificadores, implementarlo
 */
