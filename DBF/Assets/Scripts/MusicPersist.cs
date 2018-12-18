using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPersist : MonoBehaviour {

    public Slider musicVolume;
    
    private AudioSource gameMusic;
    

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        gameMusic = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameMusic.Play();
        gameMusic.volume = musicVolume.value;
    }
    private void Update()
    {
        gameMusic.volume = musicVolume.value;
    }
}

