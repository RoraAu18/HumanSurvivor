using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip soundAddCollectable;
    [SerializeField] AudioSource player;

    private void Start()
    {
        
    }
    public void playSoundAddCollectable()
    {
        player.clip = soundAddCollectable;
        player.Play();
    }
}
