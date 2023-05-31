using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIPlayerController : MonoBehaviour
{
    public bool onStealhMode;
    public bool amAfraid;
    public CharactersToPlay characterFeatures;
    public GameObject collectionEffect;

    public ThirdPersonMovement movement;
    public StealthMode stealthMode;
    public DistractMode distractMode;
    public Jump jump;
    public ColisionController colisionController;
    private PlayerStates oldPlayerState;

    public Action<PlayerStates> OnStatePlayerChange;

    public MeshRenderer mesh;
    public PlayerStates playerState;
    public iconPlayerStates iconPlayerStates;
    public ConfigurationsPerPlayerState[] configOnPlayerSatate = new ConfigurationsPerPlayerState[(int)PlayerStates.count];

    private void OnValidate()
    {
        var statesCount = (int)PlayerStates.count;
        if (configOnPlayerSatate.Length != statesCount)
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
        colisionController.collisionEnter += CrossedFinalLine; 
        playerState = PlayerStates.idle;
        collectionEffect.gameObject.SetActive(false);
    }
    void Update()
    {
        if (GameManager.OnlyInstance.gameStates == GameStates.GameOver) return;
        RefreshState();
    }

    private void RefreshState()
    {
        if (!jump.isGrounded && !stealthMode.stealth)
        {
            if (oldPlayerState != PlayerStates.jump)
            {
                SetState(PlayerStates.jump);
            }
        }

        else if (stealthMode.stealth && movement.dir.magnitude != 0)
        {
            if (oldPlayerState != PlayerStates.stealthMove)
            {
                SetState(PlayerStates.stealthMove);
            }
        }
        else if (stealthMode.stealth && !jump.isGrounded)
        {
            if (oldPlayerState != PlayerStates.stealthJump)
            {
                SetState(PlayerStates.stealthJump);
            }
        }
        else if (stealthMode.stealth)
        {
            if (oldPlayerState != PlayerStates.stealthIdle)
            {
                SetState(PlayerStates.stealthIdle);

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
        else if (amAfraid)
        {
            if (oldPlayerState != PlayerStates.afraid)
            {
                SetState(PlayerStates.afraid);
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

    public void CrossedFinalLine(Collider coll)
    {
        if (coll.CompareTag("FinalLine"))
        {
            GameManager.OnlyInstance.crossedFinalLine = true;
        }
    }

}
public enum PlayerStates
{
    idle = 0,
    run = 1,
    jump = 2,
    stealthMove = 3,
    distract = 4,
    stealthIdle = 5,
    stealthJump = 6,
    afraid = 7,
    count = 8
}

public enum iconPlayerStates
{
    Happy,
    Sad,
    Stealth,
    Distract,
    Afraid,
    Death
}

[Serializable]
public class ConfigurationsPerPlayerState
{
    public float speed;
    public bool onStealthMode;    
}

