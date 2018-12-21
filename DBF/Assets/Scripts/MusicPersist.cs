using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPersist : MonoBehaviour {

    /// <summary>
    /// this script is attached to the music source of the game and makes sure the music continues playing when you leave the main menu
    /// it also controls the music volume
    /// </summary>


    //the slider in the options menu that determines volume
    public Slider musicVolume;
    
    private AudioSource gameMusic;
    
    //make cure the audio persists through scenes
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        gameMusic = GetComponent<AudioSource>();
    }

    //play the music, set the volume to it's default value
    private void Start()
    {
        gameMusic.Play();
        gameMusic.volume = musicVolume.value;
    }
    //if the player adjusts the volume, update its value
    private void Update()
    {
        gameMusic.volume = musicVolume.value;
    }
}

