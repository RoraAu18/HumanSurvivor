using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIPlayerController : MonoBehaviour, iLifeController 
{
    public bool OnSneakyMode;
    public ThirdPersonMovement playerItem;
    public MeshRenderer mesh;
    public PlayerStates playerState;
    public ConfigurationsPerPlayerState[] configOnPlayerSatate = new ConfigurationsPerPlayerState[(int) PlayerStates.COUNT];
    public float impactForce = 30;


    private void OnValidate()
    {
        var statesCount = (int)PlayerStates.COUNT;
        if(configOnPlayerSatate.Length != statesCount)
        {
            Array.Resize(ref configOnPlayerSatate, statesCount);
        }
    }
    void Update()
    {
        OnSneakyMode = Input.GetKey(KeyCode.Q);

        if (OnSneakyMode)
        {

            Debug.Log("I'm in Sneaky mode");

        }

    }

    public void SetState(PlayerStates newState)
    {
        if (newState == playerState) return;
        playerItem.speed = configOnPlayerSatate[(int)newState].speed;
        mesh.sharedMaterial.color = configOnPlayerSatate[(int)newState].color;
        playerState = newState;
    }

    public void OnDamage()
    {
        
    }

    public void OnDeath()
    {
        GameManager.OnlyInstance.AddDeaths(1);
    }

   //executes every time a change happens in the inspector
}
public enum PlayerStates
{
    Normal = 0,
    Stealth = 1,
    COUNT = 3
}

[Serializable]
public class ConfigurationsPerPlayerState
{
    public float speed;
    public Color color;
}

