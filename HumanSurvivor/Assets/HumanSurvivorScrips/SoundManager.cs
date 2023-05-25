using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip soundAddCollectable;
    public AudioSource player;
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

    private void Start()
    {
        enemy.onEnemyStateChange += EnemySoundsChange;
    }
    public void playSoundAddCollectable()
    {
        player.clip = soundAddCollectable;
        player.Play();
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
}
