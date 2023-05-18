using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIPlayerController : MonoBehaviour 
{
    public bool onStealhMode;
    public bool amAfraid;
    
   
    public ThirdPersonMovement movement;
    public StealthMode stealthMode;
    public DistractMode distractMode;
    public Jump jump;
    private PlayerStates oldPlayerState;

    public Action<PlayerStates> OnStatePlayerChange;

    public MeshRenderer mesh;
    public PlayerStates playerState;
    public ConfigurationsPerPlayerState[] configOnPlayerSatate = new ConfigurationsPerPlayerState[(int) PlayerStates.count];
   
    private void OnValidate()
    {
        var statesCount = (int)PlayerStates.count;
        if(configOnPlayerSatate.Length != statesCount)
        {
            Array.Resize(ref configOnPlayerSatate, statesCount);
        }
    }

    void Start()
    {
        TryGetComponent<Jump>(out Jump jump);
        TryGetComponent<ThirdPersonMovement>(out ThirdPersonMovement movement);
        TryGetComponent<StealthMode>(out StealthMode stealthMode);
        TryGetComponent<DistractMode>(out DistractMode distractMode);
        playerState = PlayerStates.idle;
    }
    void Update()
    {
        RefreshState();
    }

    private void RefreshState()
    {
        if (jump.isGrounded == false)
        {
            if (oldPlayerState != PlayerStates.jump)
            {
                SetState(PlayerStates.jump);                
            }
        }

        else if (stealthMode.stealth && movement.dir.magnitude != 0)
        {
            if (oldPlayerState != PlayerStates.stealth)
            {
                SetState(PlayerStates.stealth);
            }
        }
        else if (distractMode.distract)
        {
            if (oldPlayerState != PlayerStates.distract)
            {
                SetState(PlayerStates.distract);
            }
        }

        else if (movement.dir.magnitude != 0)
        {
            if (oldPlayerState != PlayerStates.run)
            {
                SetState(PlayerStates.run);
            }
        }

        else
        {
            if (oldPlayerState != PlayerStates.idle)
            {
                SetState(PlayerStates.idle);
            }
        }

        oldPlayerState = playerState;
    }


    public void SetState(PlayerStates newState)
    {
        movement.speed = configOnPlayerSatate[(int)newState].speed;
        onStealhMode = configOnPlayerSatate[(int)newState].onStealthMode;
        playerState = newState;
        OnStatePlayerChange?.Invoke(newState);
        GameManager.OnlyInstance.PlayerChangeMood(newState);
    }

}
public enum PlayerStates
{
    idle = 0,
    run = 1,
    jump = 2,
    stealth = 3,
    distract = 4,
    count = 5
}

[Serializable]
public class ConfigurationsPerPlayerState
{
    public float speed;
    public bool onStealthMode;    
}

