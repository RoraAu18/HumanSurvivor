using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, IGameEventsUser
{
    public AudioClip soundAddCollectable;
    public AudioSource bkMusic;
    public AudioSource player;

    //EnemySounds
    [SerializeField]
    EnemyAIContoller enemy;
    [SerializeField]
    AudioSource surprisedSound;
    [SerializeField]
    AudioSource walkSound;
    [SerializeField]
    AudioSource runSound;
    [SerializeField]
    AudioSource confusedSound;
    [SerializeField]
    AudioSource ahaSound;

    //PlayerSounds
    [SerializeField]
    AIPlayerController playerSts;
    [SerializeField]
    AudioSource distractionCall;
    [SerializeField]
    AudioSource playerJumpSound;    
    [SerializeField]
    AudioSource endangeredSound;

    private void Start()
    {
        playerSts = GameManager.OnlyInstance.player;
        playerSts.OnStatePlayerChange += PlayerSoundsChange;
        enemy.onEnemyStateChange += EnemySoundsChange;
    }
    public void playSoundAddCollectable()
    {
        player.clip = soundAddCollectable;
        player.Play();
    }
    public void PlayerSoundsChange(PlayerStates states)
    {
        switch (states)
        {
            case PlayerStates.idle:
                bkMusic.pitch = 1;
                bkMusic.volume = 0.7f;
                break;
            case PlayerStates.jump:
                playerJumpSound.PlayOneShot(playerJumpSound.clip);
                Debug.Log("jump sound");
                bkMusic.pitch = 1;
                bkMusic.volume = 0.7f;
                break;
            case PlayerStates.stealthIdle:
                bkMusic.pitch = 0.9f;
                bkMusic.volume = 0.4f;
                break;
            case PlayerStates.stealthMove:
                bkMusic.pitch = 0.9f;
                bkMusic.volume = 0.4f;
                break;
            case PlayerStates.stealthJump:
                playerJumpSound.PlayOneShot(playerJumpSound.clip);
                Debug.Log("stealth jump sound");
                bkMusic.pitch = 0.9f;
                bkMusic.volume = 0.4f;
                break;
            case PlayerStates.afraid:
                endangeredSound.PlayOneShot(endangeredSound.clip);
                break;
            case PlayerStates.run:
                bkMusic.pitch = 1;
                bkMusic.volume = 0.7f;
                break;
        }

    }
    public void EnemySoundsChange(EnemyStates states)
    {
        switch (states)
        {
            case EnemyStates.Idle:
                break;
            case EnemyStates.Distracted:
                break;
            case EnemyStates.Walking:
                walkSound.PlayOneShot(walkSound.clip);
                break;
            case EnemyStates.Confused:
                confusedSound.PlayOneShot(confusedSound.clip);
                break;
            case EnemyStates.Surprised:
                surprisedSound.PlayOneShot(surprisedSound.clip);
                break;
            case EnemyStates.Running:
                runSound.PlayOneShot(runSound.clip);
                break;
            case EnemyStates.CatchingPlayer:
                ahaSound.PlayOneShot(ahaSound.clip);
                break;
            case EnemyStates.Desperate:
                break;
        }
    }

    public void DistractingEnemySound()
    {
        distractionCall.PlayOneShot(distractionCall.clip);
    }

    public void OnMoodChanged(PlayerStates states)
    {

    }

    public void OnObjectsCollected(ObjectsType typeObjectCollected)
    {
        player.clip = soundAddCollectable;
        player.Play();
    }
}
